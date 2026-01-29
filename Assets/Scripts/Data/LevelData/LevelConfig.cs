using System;

[Serializable]
public class LevelConfig
{
    public int level;
    public int requireKillCount;
}

public struct LevelData
{
    public readonly int level;
    public readonly int requireKillCount;

    public LevelData(int level, int requireKillCount)
    {
        this.level = level;
        this.requireKillCount = requireKillCount;
    }
}