using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Parquet;
using Parquet.Data;

public class DmxRecordData
{
    private double duration;
    private List<DmxRecordPacket> data;

    public double Duration => duration;
    public IEnumerable<DmxRecordPacket> Data => data;

    public DmxRecordData(double duration, List<DmxRecordPacket> data)
    {
        this.duration = duration;
        this.data = data;
    }

    public static DmxRecordData ReadFromFilePath(string path)
    {
        try
        {
            var list = new List<DmxRecordPacket>();

            double finalPaketTime = 0;

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var reader = new BinaryReader(stream);

                // loop to the end
                var baseStream = reader.BaseStream;
                while (baseStream.Position != baseStream.Length)
                {
                    var sequence = (int)reader.ReadUInt32();
                    var time = reader.ReadDouble();

                    finalPaketTime = time;

                    var numUniverses = (int)reader.ReadUInt32();

                    var data = new List<UniverseData>();

                    for (var i = 0; i < numUniverses; i++)
                    {
                        var universe = (int)reader.ReadUInt32();
                        data.Add(new UniverseData { universe = universe, data = reader.ReadBytes(512).ToArray() });
                    }

                    list.Add(new DmxRecordPacket
                    {
                        sequence = sequence,
                        time = time,
                        numUniverses = numUniverses,
                        data = data
                    });
                }
            }

            return new DmxRecordData(finalPaketTime, list);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed importing {path}. {e.Message}");
            return null;
        }
    }

    public static async Task<DmxRecordData> ReadFromParquetFile(string path)
    {
        try
        {
            var list = new List<DmxRecordPacket>();
            double finalPaketTime = 0;
            using (Stream fileStream = System.IO.File.OpenRead(path))
            {
                using (ParquetReader parquetReader = await ParquetReader.CreateAsync(fileStream).ConfigureAwait(false))
                {
                    // get file schema (available straight after opening parquet reader)
                    // however, get only data fields as only they contain data values
                    DataField[] dataFields = parquetReader.Schema.GetDataFields();

                    // enumerate through row groups in this file
                    for (int i = 0; i < parquetReader.RowGroupCount; i++)
                    {
                        // create row group reader
                        using (ParquetRowGroupReader groupReader = parquetReader.OpenRowGroupReader(i))
                        {
                            // read all columns inside each row group (you have an option to read only
                            // required columns if you need to.
                            var columns = new DataColumn[dataFields.Length];
                            for (int c = 0; c < dataFields.Length; c++)
                            {
                                columns[c] = await groupReader.ReadColumnAsync(dataFields[c]).ConfigureAwait(false);
                            }

                            var sequenceColumn = columns[0];
                            var timeColumn = columns[1];
                            var numUniverseColumn = columns[2];
                            var dataColumn = columns[3];

                            // .Data member contains a typed array of column data you can cast to the type of the column
                            uint[] sequences = (uint[])sequenceColumn.Data;
                            double[] times = (double[])timeColumn.Data;
                            uint[] numUniverse = (uint[])numUniverseColumn.Data;
                            var data = (byte?[])dataColumn.Data;
                            var dataList = data.Where(x => x != null).Select(x => (byte)x).ToList();

                            int offset = 0;
                            for (int j = 0; j < groupReader.RowCount; j++)
                            {
                                var universeData = new List<UniverseData>();
                                for (int k = 0; k < numUniverse[j]; k++)
                                {
                                    universeData.Add(new UniverseData { universe = k, data = dataList.GetRange(offset, 512).ToArray() });
                                    offset += 512;
                                }
                                list.Add(new DmxRecordPacket
                                {
                                    sequence = (int)sequences[j],
                                    time = times[j],
                                    numUniverses = (int)numUniverse[j],
                                    data = universeData,
                                });
                            }
                            finalPaketTime = times.Last();
                        }
                    }
                }
            }
            return new DmxRecordData(finalPaketTime, list);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed importing {path}. {e.Message}");
            return null;
        }
    }
}


[Serializable]
public class DmxRecordPacket
{
    public int sequence;
    public double time; // millisec
    public int numUniverses;
    public List<UniverseData> data;
}

[Serializable]
public class UniverseData
{
    public int universe;
    public byte[] data;
}
