using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V8_0_1_27101
{
    public static class Opcodes_8_0_1
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            switch (direction)
            {
                case Direction.ClientToServer:
                    return ClientOpcodes;
                case Direction.ServerToClient:
                    return ServerOpcodes;
                default:
            return MiscOpcodes;
        }
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_TOGGLE_WARMODE, 0x32AC},
            {Opcode.CMSG_QUERY_CREATURE, 0x3270},
            {Opcode.CMSG_QUERY_GAME_OBJECT, 0x3271},
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new BiDictionary<Opcode, int>
        {
            {Opcode.SMSG_ATTACK_START, 0x266F},
            {Opcode.SMSG_ATTACK_STOP, 0x2670},
            {Opcode.SMSG_CRITERIA_UPDATE, 0x2720},
            {Opcode.SMSG_LIGHTNING_STORM_CHANGE, 0x26D5},
            {Opcode.SMSG_LIGHTNING_STORM_START, 0x26D6},
            {Opcode.SMSG_UPDATE_OBJECT, 0x2817},
            {Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x2702},
            {Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x2703},
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>();
    }
}
