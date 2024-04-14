public static class WorldState
{
    private static int level = 0;
    private static int lives = 3;
    private static int points = 0;

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
}