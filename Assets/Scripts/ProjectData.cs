using System.Collections.Generic;

public static class ProjectData
{
    public static int Id { get; set; }
    public static string Designer { get; set; }
    public static string Token { get; set; }
    public static List<Project> Projects { get; set; } = new List<Project>();
    public static string CurrentHabitacionJson { get; set; }
    public static string CurrentObjetoJson { get; set; }
}

public class Project
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string NotasPersonales { get; set; }
    public UnityProject UnityProyect { get; set; }
}

public class UnityProject
{
    public int? Id { get; set; }
    public string Objeto { get; set; }
    public string Habitacion { get; set; }
}