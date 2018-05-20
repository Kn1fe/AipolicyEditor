using AipolicyEditor.AIPolicy.Operations.CustomEditors;
using System.IO;

namespace AipolicyEditor.AIPolicy
{
    public static class TargetStream
    {
        public static TargetParam Read(BinaryReader br)
        {
            EnumTarget et = (EnumTarget)br.ReadUInt32();
            uint occupation = 0;
            if (et == EnumTarget.occupation_list)
                occupation = br.ReadUInt32();
            return new TargetParam() { Target = et, Occupations = occupation };
        }

        public static void Save(BinaryWriter bw, TargetParam param)
        {
            bw.Write((uint)param.Target);
            if (param.Target == EnumTarget.occupation_list)
                bw.Write(param.Occupations);
        }
    }

    public enum EnumTarget : uint
    {
        hate_first = 0x0,
        hate_second = 0x1,
        hate_others = 0x2,
        most_hp = 0x3,
        most_mp = 0x4,
        least_hp = 0x5,
        occupation_list = 0x6,
        self = 0x7,
        monster_killer = 0x8,
        monster_birthplace_faction = 0x9,
        hate_random_one = 0xA,
        hate_nearest = 0xB,
        hate_farthest = 0xC,
        hate_first_redirected = 0xD,
    };
}
