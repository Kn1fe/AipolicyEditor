namespace AipolicyEditor.AIPolicy
{
    public enum ChatChannels2 : uint
    {
        Normal = 0,
        System = 1,
        NormalWithoutName = 2,
        Gm = 3,
        CenterScreen = 4
    }

    public enum TalkTextAppendDataMask : uint
    {
        None = 0,
        Name = 1,
        LocalVar0 = 2,
        LocalVar1 = 4,
        LocalVar2 = 8
    }

    public enum FactionPVPPointType : uint
    {
        MineCar = 0,
        MineBase = 1,
        MineCarArrived = 2
    }

    public enum OperatorType : uint
    {
        Add = 0,
        Sub = 1,
        Mul = 2,
        Div = 3,
        Mod = 4
    }

    public enum VarType : uint
    {
        GlobalVarID = 0,
        LocalVarID = 1,
        Const = 2,
        Random = 3
    }

    public enum MonsterPatrolSpeedType : uint
    {
        Begin = 1,
        Slow = 1,
        Fast = 2,
        End = 2
    }

    public enum MonsterPatrolType : uint
    {
        StopAtEnd = 0,
        Return = 1,
        Loop = 2
    }

    public enum SummoneeDisppearType : uint
    {
        Never = 0,
        FollowSummoner = 1,
        FollowSummonTarget = 2,
        FollowSummonerOrSummonTarget = 3
    }
}
