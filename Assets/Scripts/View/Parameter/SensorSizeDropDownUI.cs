using System.Collections.Generic;
using UnityEngine;

namespace inc.stu.SyncArena
{
    public class SensorSizeDropDownUI : DropDownField<Vector2>
    {
        protected override string None => "Custom";

        // URL: https://www.j-sky-film.net/images/sensor-size.pdf
        protected override List<DropDownElement<Vector2>> DropdownElementList =>  new() 
        {
            new("35mm FullSize", new Vector2(36, 24)),
            new("APS-C", new Vector2(24, 16)),
            new("35mm Film", new Vector2(22, 16)),
            new("Super 35mm", new Vector2(24, 14)),
            new ("Four Thirds", new Vector2(17.3f, 13)),
            new ("Super 16mm", new Vector2(12.5f, 7.4f)),
            new ("16mm Film", new Vector2(10.3f, 7.5f)),
            new ("2/3 inch", new Vector2(8.8f, 6.6f)),
            new ("1/2 inch", new Vector2(6.4f, 4.8f)),
            new ("1/3 inch", new Vector2(4.8f, 3.6f)),
            
        };
        
        public void SelectOptionFromVector2(Vector2 sensorSize)
        {
            for (var i = 0; i < DropdownElementList.Count; i++)
            {
                if (DropdownElementList[i].Value == sensorSize)
                {
                    _dropdown.SetValueWithoutNotify(i);
                    return;
                }
            }
            
            SelectNone();
        }
        
    }
}
