using System;
using System.Collections.Generic;
using com.kodai100.ArtNetApp.Entities;

namespace com.kodai100.ArtNetApp.Models
{
    
    public static class MockupData
    {
        
        public static IEnumerable<FixturePresetEntity> FixturePresetMockData =>
            new List<FixturePresetEntity>
            {
                new() { Guid = Guid.Parse("f29e1d30-19bf-4322-90f0-2e0448816177"), Name = "Robe MegaPointe [20ch]" },
                new() { Guid = Guid.Parse("f581d163-dfe8-4c09-ab78-22f92d38e15c"), Name = "GLP Impression X4 [14ch]" },
                new() { Guid = Guid.Parse("5e9f5d57-befb-401c-9daf-289ecb5d62fe"), Name = "GLP Impression X5 [14ch]" },
                new() { Guid = Guid.Parse("473ebaa1-d0be-4014-ab9e-a83a25b2373b"), Name = "ClayPaky Sharpy [12ch]" },
                new() { Guid = Guid.Parse("f82e4a5e-5fc5-44f0-8145-bec978c6d0cc"), Name = "ClayPaky Mythos [12ch]" }
            };

        public static IEnumerable<FixturePlacementEntity> FixturePlacementMockData =>
            new List<FixturePlacementEntity>
            {
                new() { Guid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f"), OrderIndex = 0, Name = "Robe MegaPointe [20ch]", Universe = 1, ChannelOffset = 0, PresetReferenceGuid = Guid.Parse("f29e1d30-19bf-4322-90f0-2e0448816177")},
                new() { Guid = Guid.Parse("34fa4f94-c892-4ab0-bd6b-99e0e316cfb9"), OrderIndex = 1, Name = "GLP Impression X4 [14ch]", Universe = 1, ChannelOffset = 20, PresetReferenceGuid = Guid.Parse("f581d163-dfe8-4c09-ab78-22f92d38e15c")},
                new() { Guid = Guid.Parse("a6245496-d273-4433-9171-91b6bb638914"), OrderIndex = 2, Name = "GLP Impression X5 [14ch]", Universe = 1, ChannelOffset = 34, PresetReferenceGuid = Guid.Parse("5e9f5d57-befb-401c-9daf-289ecb5d62fe") },
                new() { Guid = Guid.Parse("464ae561-558e-4b0d-966f-bab7fd897db6"), OrderIndex = 3, Name = "ClayPaky Sharpy [12ch]", Universe = 1, ChannelOffset = 48, PresetReferenceGuid = Guid.Parse("473ebaa1-d0be-4014-ab9e-a83a25b2373b") },
                new() { Guid = Guid.Parse("a59489b6-5cc3-4c4f-ba86-c54e1663dd7a"), OrderIndex = 4, Name = "ClayPaky Mythos [12ch]", Universe = 2, ChannelOffset = 0, PresetReferenceGuid = Guid.Parse("f82e4a5e-5fc5-44f0-8145-bec978c6d0cc") },
                new() { Guid = Guid.Parse("17eb91fb-be41-4aed-9087-c691fda0b9bb"), OrderIndex = 5, Name = "My Device", Universe = 2, ChannelOffset = 0, PresetReferenceGuid = Guid.Empty }
            };

        public static IEnumerable<DmxChannelEntity> DmxChannelMockData =>
            new List<DmxChannelEntity>
            {
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, ChannelIndex = 0, ChannelName = "Pan", ChannelValue = 255, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, ChannelIndex = 1, ChannelName = "Pan Fine", ChannelValue = 10, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, ChannelIndex = 2, ChannelName = "Tilt", ChannelValue = 100, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, ChannelIndex = 3, ChannelName = "Tilt Fine", ChannelValue = 0, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, ChannelIndex = 4, ChannelName = "Dimmer", ChannelValue = 160, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, ChannelIndex = 5, ChannelName = "Color(R)", ChannelValue = 25, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, ChannelIndex = 6, ChannelName = "Color(G)", ChannelValue = 240, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
                new(){ Guid = Guid.NewGuid(), OrderIndex = 0, ChannelIndex = 7, ChannelName = "Color(B)", ChannelValue = 200, InstancedFixtureReferenceGuid = Guid.Parse("7d1ea26d-ee01-4cee-9068-9816795d356f")},
            };

    }

}
