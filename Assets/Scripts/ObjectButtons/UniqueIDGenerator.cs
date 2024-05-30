using System;

public static class UniqueIDGenerator
{
    private static int counter = 0;

    public static string GenerateID()
    {
        counter++;
        return "Object_" + counter.ToString();
    }
}

