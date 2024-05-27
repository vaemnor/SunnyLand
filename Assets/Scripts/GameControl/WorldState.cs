public static class WorldState
{
    private static int level = 0;
    private static int lives = 0;
    private static int points = 0;

    private static int levelAtLevelStart = 0;
    private static int livesAtLevelStart = 0;
    private static int pointsAtLevelStart = 0;

    private static float backgroundMusicVolume = 0.2f;

    public static int Level
    {
        get { return level; }
        set { level = value; }
    }

    public static int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    public static int Points
    {
        get { return points; }
        set { points = value; }
    }

    public static int LevelAtLevelStart
    {
        get { return levelAtLevelStart; }
        set { levelAtLevelStart = value; }
    }

    public static int LivesAtLevelStart
    {
        get { return livesAtLevelStart; }
        set { livesAtLevelStart = value; }
    }

    public static int PointsAtLevelStart
    {
        get { return pointsAtLevelStart; }
        set { pointsAtLevelStart = value; }
    }

    public static float BackgroundMusicVolume
    {
        get { return backgroundMusicVolume; }
        set { backgroundMusicVolume = value; }
    }
}