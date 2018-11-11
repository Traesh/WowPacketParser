using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class HotfixHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var type = packet.ReadUInt32E<DB2Hash>("TableHash");
            var entry = packet.ReadInt32("RecordID");
            var timeStamp = packet.ReadUInt32();
            packet.AddValue("Timestamp", Utilities.GetDateTimeFromUnixTime(timeStamp));
            var allow = packet.ReadBit("Allow");

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            if (entry < 0 || !allow)
            {
                packet.WriteLine("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
            }
            else
            {
                switch (type)
                {
                    case DB2Hash.BroadcastText:
                        {
                            var bct = new BroadcastText()
                            {
                                ID = (uint)entry,
                                Text = db2File.ReadCString("Text"),
                                Text1 = db2File.ReadCString("Text1"),
                            };

                            db2File.ReadUInt32("ID");
                            bct.LanguageID = db2File.ReadByte("LanguageID");
                            bct.ConditionID = db2File.ReadUInt32("ConditionID");
                            bct.EmotesID = db2File.ReadUInt16("EmotesID");
                            bct.Flags = db2File.ReadByte("Flags");
                            bct.ConditionID = db2File.ReadUInt32("ChatBubbleDurationMs");

                            bct.SoundEntriesID = new uint?[2];
                            for (int i = 0; i < 2; ++i)
                                bct.SoundEntriesID[i] = db2File.ReadUInt32("SoundEntriesID", i);

                            bct.EmoteID = new ushort?[3];
                            for (int i = 0; i < 3; ++i)
                                bct.EmoteID[i] = db2File.ReadUInt16("EmoteID", i);

                            bct.EmoteDelay = new ushort?[3];
                            for (int i = 0; i < 3; ++i)
                                bct.EmoteDelay[i] = db2File.ReadUInt16("EmoteDelay", i);

                            Storage.BroadcastTexts.Add(bct, packet.TimeSpan);

                            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
                            {
                                BroadcastTextLocale lbct = new BroadcastTextLocale
                                {
                                    ID = bct.ID,
                                    TextLang = bct.Text,
                                    Text1Lang = bct.Text1
                                };
                                Storage.BroadcastTextLocales.Add(lbct, packet.TimeSpan);
                            }
                            break;
                        }
                    default:
                        HotfixStoreMgr.AddRecord(type, entry, db2File);
                        break;
                }

                db2File.ClosePacket(false);
            }
        }
    }
}
