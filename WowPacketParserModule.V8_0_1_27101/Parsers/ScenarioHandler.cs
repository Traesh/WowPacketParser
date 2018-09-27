using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ScenarioHandler
    {
        [Parser(Opcode.SMSG_SCENARIO_POIS)]
        public static void HandleScenarioPOIs(Packet packet)
        {
            var scenarioPOIDataCount = packet.ReadUInt32("ScenarioPOIDataCount");
            for (var i = 0; i < scenarioPOIDataCount; i++)
            {
                packet.ReadInt32("CriteriaTreeID");

                var scenarioBlobDataCount = packet.ReadUInt32("ScenarioBlobDataCount");
                for (int j = 0; j < scenarioBlobDataCount; j++)
                {
                    packet.ReadInt32("BlobID", i, j);
                    packet.ReadInt32<MapId>("MapID", i, j);
                    packet.ReadInt32("UiMapID", i, j);
                    packet.ReadInt32("Priority", i, j);
                    packet.ReadInt32("Flags", i, j);
                    packet.ReadInt32("WorldEffectID", i, j);
                    packet.ReadInt32("PlayerConditionID", i, j);

                    var scenarioPOIPointDataCount = packet.ReadUInt32("ScenarioPOIPointDataCount", i, j);
                    for (int k = 0; k < scenarioPOIPointDataCount; k++)
                    {
                        packet.ReadInt32("X", i, j, k);
                        packet.ReadInt32("Y", i, j, k);
                    }
                }
            }
        }
    }
}
