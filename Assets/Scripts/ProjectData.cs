using System.Collections.Generic;

public static class ProjectData
{
    public static string Designer { get; set; }
    public static List<Project> Projects { get; set; }
    public static string CurrentProjectJsonUrl { get; set; }
}

public class Project
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string NotasPersonales { get; set; }
    public string UnityProjectJsonUrl { get; set; }
}
