using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameProgressData
{
    public bool introDone = false;
    public List<string> completedEvents = new List<string>();
}

public static class GameProgress
{
    private static string SavePath => Application.persistentDataPath + "/gameProgress.json";

    public static GameProgressData Load()
    {
        if (!System.IO.File.Exists(SavePath)) return new GameProgressData();
        return JsonUtility.FromJson<GameProgressData>(System.IO.File.ReadAllText(SavePath)) ?? new GameProgressData();
    }

    public static void Save(GameProgressData data)
    {
        System.IO.File.WriteAllText(SavePath, JsonUtility.ToJson(data));
    }

    public static bool IsIntroDone() => Load().introDone;

    public static void MarkIntroDone()
    {
        var data = Load();
        data.introDone = true;
        Save(data);
    }

    public static bool IsEventDone(string key) => Load().completedEvents.Contains(key);

    public static void MarkEventDone(string key)
    {
        var data = Load();
        if (!data.completedEvents.Contains(key))
        {
            data.completedEvents.Add(key);
            Save(data);
        }
    }
}