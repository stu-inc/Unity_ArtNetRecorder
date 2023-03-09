using System;
using System.Collections.Generic;
using com.kodai100.ArtNetApp.Entities;

namespace com.kodai100.ArtNetApp.Models
{
    
    public static class MockupData
    {

        public static IEnumerable<FixtureManufacturerEntity> FixtureManufacturerMockData =>
            new List<FixtureManufacturerEntity>
            {
                new() { Guid = Guid.NewGuid(), ManufacturerName = "Robe"},
                new() { Guid = Guid.NewGuid(), ManufacturerName = "Clay Paky"},
                new() { Guid = Guid.NewGuid(), ManufacturerName = "GLP"},
                new() { Guid = Guid.NewGuid(), ManufacturerName = "Martin Professional"},
            };
        
        public static IEnumerable<FixturePresetEntity> FixturePresetMockData =>
            new List<FixturePresetEntity>
            {
                new()
                {
                    Guid = Guid.Parse("f29e1d30-19bf-4322-90f0-2e0448816177"),
                    Manufacturer = "Robe",
                    FixtureName = "MegaPointe",
                    Mode = "Mode1 [39ch]",
                    Channels = new[]
                    {
                        new Channel{ChannelIndex = 0, ChannelName = "Pan"},
                        new Channel{ChannelIndex = 1, ChannelName = "Pan fine"},
                        new Channel{ChannelIndex = 2, ChannelName = "Tilt"},
                        new Channel{ChannelIndex = 3, ChannelName = "Tilt fine"},
                        new Channel{ChannelIndex = 4, ChannelName = "Pan/Tilt speed, Pan/Tilt time"},
                        new Channel{ChannelIndex = 5, ChannelName = "Power/Special functions"},
                        new Channel{ChannelIndex = 6, ChannelName = "Cyan"},
                        new Channel{ChannelIndex = 7, ChannelName = "Magenta"},
                        new Channel{ChannelIndex = 8, ChannelName = "Yellow"},
                        new Channel{ChannelIndex = 9, ChannelName = "Color wheel"},
                        new Channel{ChannelIndex = 10, ChannelName = "Color wheel fine"},
                        new Channel{ChannelIndex = 11, ChannelName = "Virtual color wheel"},
                        new Channel{ChannelIndex = 12, ChannelName = "Effect Speed"},
                        new Channel{ChannelIndex = 13, ChannelName = "CMY & Color wheel time"},
                        new Channel{ChannelIndex = 14, ChannelName = "Zoom & Focus & Frost & Prism time"},
                        new Channel{ChannelIndex = 15, ChannelName = "Effect wheel positioning"},
                        new Channel{ChannelIndex = 16, ChannelName = "Effect wheel rotation"},
                        new Channel{ChannelIndex = 17, ChannelName = "Effect wheel animations"},
                        new Channel{ChannelIndex = 18, ChannelName = "Static gobo wheel"},
                        new Channel{ChannelIndex = 19, ChannelName = "Rotating gobo wheel"},
                        new Channel{ChannelIndex = 20, ChannelName = "Rot. gobo  indexing and rotation"},
                        new Channel{ChannelIndex = 21, ChannelName = "Rot. gobo indexing/rotation - fine"},
                        new Channel{ChannelIndex = 22, ChannelName = "Prism wheel 1"},
                        new Channel{ChannelIndex = 23, ChannelName = "Prism wheel 1 indexing/rotation"},
                        new Channel{ChannelIndex = 24, ChannelName = "Prism wheel 2"},
                        new Channel{ChannelIndex = 25, ChannelName = "Prism wheel 2 indexing/rotation"},
                        new Channel{ChannelIndex = 26, ChannelName = "Pattern selection"},
                        new Channel{ChannelIndex = 27, ChannelName = "Pattern rotation and indexing"},
                        new Channel{ChannelIndex = 28, ChannelName = "Beam shaper selection"},
                        new Channel{ChannelIndex = 29, ChannelName = "Beam shaper rotation and indexing"},
                        new Channel{ChannelIndex = 30, ChannelName = "Frost"},
                        new Channel{ChannelIndex = 31, ChannelName = "Zoom"},
                        new Channel{ChannelIndex = 32, ChannelName = "Zoom - fine"},
                        new Channel{ChannelIndex = 33, ChannelName = "Focus"},
                        new Channel{ChannelIndex = 34, ChannelName = "Focus Fine"},
                        new Channel{ChannelIndex = 35, ChannelName = "Hot-Spot control"},
                        new Channel{ChannelIndex = 36, ChannelName = "Shutter/ strobe"},
                        new Channel{ChannelIndex = 37, ChannelName = "Dimmer intensity"},
                        new Channel{ChannelIndex = 38, ChannelName = "Dimmer intensity - fine"},
                    }
                },
                new()
                {
                    Guid = Guid.Parse("f581d163-dfe8-4c09-ab78-22f92d38e15c"),
                    Manufacturer = "GLP",
                    FixtureName = "Impression X4",
                    Mode = "Mode20 [20ch]",
                    Channels = new[]
                    {
                        new Channel{ChannelIndex = 0, ChannelName = "Pan"},
                        new Channel{ChannelIndex = 1, ChannelName = "Pan fine"},
                        new Channel{ChannelIndex = 2, ChannelName = "Tilt"},
                        new Channel{ChannelIndex = 3, ChannelName = "Tilt fine"},
                        new Channel{ChannelIndex = 4, ChannelName = "Color (fixed)"},
                        new Channel{ChannelIndex = 5, ChannelName = "Red"},
                        new Channel{ChannelIndex = 6, ChannelName = "Green"},
                        new Channel{ChannelIndex = 7, ChannelName = "Blue"},
                        new Channel{ChannelIndex = 8, ChannelName = "White"},
                        new Channel{ChannelIndex = 9, ChannelName = "Shutter"},
                        new Channel{ChannelIndex = 10, ChannelName = "Dimmer"},
                        new Channel{ChannelIndex = 11, ChannelName = "CTO"},
                        new Channel{ChannelIndex = 12, ChannelName = "Special"},
                        new Channel{ChannelIndex = 13, ChannelName = "Movement"},
                        new Channel{ChannelIndex = 14, ChannelName = "Speed Pan/Tilt"},
                        new Channel{ChannelIndex = 15, ChannelName = "Zoom"},
                        new Channel{ChannelIndex = 16, ChannelName = "Pattern"},
                        new Channel{ChannelIndex = 17, ChannelName = "Pattern Byte1"},
                        new Channel{ChannelIndex = 18, ChannelName = "Pattern Byte2"},
                        new Channel{ChannelIndex = 19, ChannelName = "Pattern Byte3"}
                    }
                },
                new()
                {
                    Guid = Guid.Parse("5e9f5d57-befb-401c-9daf-289ecb5d62fe"),
                    Manufacturer = "GLP",
                    FixtureName = "Impression X5",
                    Mode = "Mode1 [24ch]",
                    Channels = new[]
                    {
                        new Channel{ChannelIndex = 0, ChannelName = "Pan"},
                        new Channel{ChannelIndex = 1, ChannelName = "Pan fine"},
                        new Channel{ChannelIndex = 2, ChannelName = "Tilt"},
                        new Channel{ChannelIndex = 3, ChannelName = "Tilt fine"},
                        new Channel{ChannelIndex = 4, ChannelName = "Intensity"},
                        new Channel{ChannelIndex = 5, ChannelName = "Intensity fine"},
                        new Channel{ChannelIndex = 6, ChannelName = "Shutter"},
                        new Channel{ChannelIndex = 7, ChannelName = "Zoom"},
                        new Channel{ChannelIndex = 8, ChannelName = "Control / Settings"},
                        new Channel{ChannelIndex = 9, ChannelName = "Accessory 1"},
                        new Channel{ChannelIndex = 10, ChannelName = "Accessory 2"},
                        new Channel{ChannelIndex = 11, ChannelName = "Red"},
                        new Channel{ChannelIndex = 12, ChannelName = "Red fine"},
                        new Channel{ChannelIndex = 13, ChannelName = "Green"},
                        new Channel{ChannelIndex = 14, ChannelName = "Green fine"},
                        new Channel{ChannelIndex = 15, ChannelName = "Blue"},
                        new Channel{ChannelIndex = 16, ChannelName = "Blue fine"},
                        new Channel{ChannelIndex = 17, ChannelName = "--- not used"},
                        new Channel{ChannelIndex = 18, ChannelName = "--- not used"},
                        new Channel{ChannelIndex = 19, ChannelName = "Color wheel"},
                        new Channel{ChannelIndex = 20, ChannelName = "CTC"},
                        new Channel{ChannelIndex = 21, ChannelName = "CQC"},
                        new Channel{ChannelIndex = 22, ChannelName = "M/G shift"},
                        new Channel{ChannelIndex = 23, ChannelName = "Tungsten simulation"}
                    }
                },
                new()
                {
                    Guid = Guid.Parse("473ebaa1-d0be-4014-ab9e-a83a25b2373b"),
                    Manufacturer = "Clay Paky",
                    FixtureName = "Sharpy",
                    Mode = "Standard [16ch]",
                    Channels = new[]
                    {
                        new Channel{ChannelIndex = 0, ChannelName = "Color wheel"},
                        new Channel{ChannelIndex = 1, ChannelName = "Stop / Strobe"},
                        new Channel{ChannelIndex = 2, ChannelName = "Dimmer"},
                        new Channel{ChannelIndex = 3, ChannelName = "Static gobo change"},
                        new Channel{ChannelIndex = 4, ChannelName = "Prism insertion"},
                        new Channel{ChannelIndex = 5, ChannelName = "Prism rotation"},
                        new Channel{ChannelIndex = 6, ChannelName = "Effects movement"},
                        new Channel{ChannelIndex = 7, ChannelName = "Frost"},
                        new Channel{ChannelIndex = 8, ChannelName = "Focus"},
                        new Channel{ChannelIndex = 9, ChannelName = "Pan"},
                        new Channel{ChannelIndex = 10, ChannelName = "Pan fine"},
                        new Channel{ChannelIndex = 11, ChannelName = "Tilt"},
                        new Channel{ChannelIndex = 12, ChannelName = "Tilt fine"},
                        new Channel{ChannelIndex = 13, ChannelName = "Function"},
                        new Channel{ChannelIndex = 14, ChannelName = "Reset"},
                        new Channel{ChannelIndex = 15, ChannelName = "Lamp Control"},
                    }
                },
                new()
                {
                    Guid = Guid.Parse("f82e4a5e-5fc5-44f0-8145-bec978c6d0cc"),
                    Manufacturer = "Clay Paky",
                    FixtureName = " Mythos",
                    Mode = "Standard [30ch]",
                    Channels = new[]
                    {
                        new Channel{ChannelIndex = 0, ChannelName = "Cyan"},
                        new Channel{ChannelIndex = 1, ChannelName = "Magenta"},
                        new Channel{ChannelIndex = 2, ChannelName = "Yellow"},
                        new Channel{ChannelIndex = 3, ChannelName = "Color1"},
                        new Channel{ChannelIndex = 4, ChannelName = "Color2"},
                        new Channel{ChannelIndex = 5, ChannelName = "Color3"},
                        new Channel{ChannelIndex = 6, ChannelName = "Stopper / Strobe"},
                        new Channel{ChannelIndex = 7, ChannelName = "Dimmer"},
                        new Channel{ChannelIndex = 8, ChannelName = "Dimmer fine"},
                        new Channel{ChannelIndex = 9, ChannelName = "Static gobo change"},
                        new Channel{ChannelIndex = 10, ChannelName = "Animation disk insertion"},
                        new Channel{ChannelIndex = 11, ChannelName = "Animation disk rotation"},
                        new Channel{ChannelIndex = 12, ChannelName = "Rotating gobo select"},
                        new Channel{ChannelIndex = 13, ChannelName = "Gobo rotation"},
                        new Channel{ChannelIndex = 14, ChannelName = "Fine gobo rotation"},
                        new Channel{ChannelIndex = 15, ChannelName = "Prisms insertion"},
                        new Channel{ChannelIndex = 16, ChannelName = "Prisms rotation"},
                        new Channel{ChannelIndex = 17, ChannelName = "Frost"},
                        new Channel{ChannelIndex = 18, ChannelName = "Zoom"},
                        new Channel{ChannelIndex = 19, ChannelName = "Focus"},
                        new Channel{ChannelIndex = 20, ChannelName = "Focus fine"},
                        new Channel{ChannelIndex = 21, ChannelName = "Beam mode"},
                        new Channel{ChannelIndex = 22, ChannelName = "Pan"},
                        new Channel{ChannelIndex = 23, ChannelName = "Pan fine"},
                        new Channel{ChannelIndex = 24, ChannelName = "Tilt"},
                        new Channel{ChannelIndex = 25, ChannelName = "Tilt fine"},
                        new Channel{ChannelIndex = 26, ChannelName = "Function"},
                        new Channel{ChannelIndex = 27, ChannelName = "Reset"},
                        new Channel{ChannelIndex = 28, ChannelName = "Lamp control"},
                        new Channel{ChannelIndex = 29, ChannelName = "Macro effects"},
                    }
                }
            };

        public static IEnumerable<FixturePlacementEntity> FixturePlacementMockData =>
            new List<FixturePlacementEntity>
            {
                new() { Guid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f"), OrderIndex = 0, Name = "My Data", Universe = 1, ChannelOffset = 0, PresetReferenceGuid = Guid.Parse("f29e1d30-19bf-4322-90f0-2e0448816177")},
            };

        public static IEnumerable<DmxChannelEntity> DmxChannelMockData =>
            new List<DmxChannelEntity>
            {
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, Universe = 1, ChannelIndex = 0, ChannelName = "Pan", ChannelValue = 255, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, Universe = 1, ChannelIndex = 1, ChannelName = "Pan Fine", ChannelValue = 10, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, Universe = 1, ChannelIndex = 2, ChannelName = "Tilt", ChannelValue = 100, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, Universe = 1, ChannelIndex = 3, ChannelName = "Tilt Fine", ChannelValue = 0, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, Universe = 1, ChannelIndex = 4, ChannelName = "Dimmer", ChannelValue = 160, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, Universe = 1, ChannelIndex = 5, ChannelName = "Color(R)", ChannelValue = 25, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, Universe = 1, ChannelIndex = 6, ChannelName = "Color(G)", ChannelValue = 240, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, Universe = 1, ChannelIndex = 7, ChannelName = "Color(B)", ChannelValue = 200, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
            };

    }

}
