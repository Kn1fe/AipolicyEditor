using AipolicyEditor.AIPolicy.Operations.CustomEditors;
using Syncfusion.Windows.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using WPFLocalizeExtension.Extensions;

namespace AipolicyEditor.AIPolicy.Operations
{
    public class O_TALK_TEXT : IOperation, ICloneable
    {
        [Browsable(false)]
        public int FromVersion => 16;
        [Browsable(false)]
        public int OperID => 2;
        [Browsable(false)]
        public string Name => MainWindow.Provider.GetLocalizedString("o2");

        //Trigger param
        [LocalizedCategory("OperationParam")]
        [LocalizedDisplayName("ChatChannel")]
        public ChatChannels2 ChatChannel { get; set; }
        [LocalizedCategory("OperationParam")]
        [LocalizedDisplayName("Text")]
        public string Text { get; set; }
        [LocalizedCategory("OperationParam")]
        [LocalizedDisplayName("AppendDataMask")]
        public TalkTextAppendDataMask AppendDataMask { get; set; }
        // Target param
        [LocalizedCategory("TargetParam")]
        [LocalizedDisplayName("Target")]
        public TargetParam Target { get; set; }

        public O_TALK_TEXT()
        {
            ChatChannel = ChatChannels2.Normal;
            Text = "";
            AppendDataMask = TalkTextAppendDataMask.None;
            Target = new TargetParam();
        }

        public void Read(BinaryReader br)
        {
            int size = br.ReadInt32();
            Text = br.ReadBytes(size).ToUnicode();
            if (Text.StartsWith("$"))
            {
                string c = Text.Substring(0, 2);
                switch (c)
                {
                    case "$S":
                        ChatChannel = ChatChannels2.System;
                        break;
                    case "$A":
                        ChatChannel = ChatChannels2.NormalWithoutName;
                        break;
                    case "$I":
                        ChatChannel = ChatChannels2.Gm;
                        break;
                    case "$X":
                        ChatChannel = ChatChannels2.CenterScreen;
                        break;
                    default:
                        ChatChannel = ChatChannels2.Normal;
                        break;
                }
            }
            else
            {
                ChatChannel = ChatChannels2.Normal;
            }
            if (ChatChannel != ChatChannels2.Normal)
            {
                Text = Text.Remove(0, 2);
            }
            AppendDataMask = (TalkTextAppendDataMask)br.ReadUInt32();
            Target = TargetStream.Read(br);
        }

        public void Write(BinaryWriter bw)
        {
            string channel = "";
            switch (ChatChannel)
            {
                case ChatChannels2.System:
                    channel = "$S";
                    break;
                case ChatChannels2.NormalWithoutName:
                    channel = "$A";
                    break;
                case ChatChannels2.Gm:
                    channel = "$I";
                    break;
                case ChatChannels2.CenterScreen:
                    channel = "$X";
                    break;
            }
            byte[] data = Encoding.Unicode.GetBytes(channel + Text + '\0');
            bw.Write(data.Length);
            bw.Write(data);
            bw.Write((uint)AppendDataMask);
            TargetStream.Save(bw, Target);
        }

        public bool Search(string str)
        {
            if (Text.Contains(str) || AppendDataMask.ToString().Contains(str))
                return true;
            else
                return false;
        }

        public object Clone()
        {
            return new O_TALK_TEXT() { Text = Text, AppendDataMask = AppendDataMask, Target = Target.Clone() as TargetParam  };
        }
    }
}
