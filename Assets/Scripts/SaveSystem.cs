using UnityEngine;

public static class SaveSystem{
    private static string gamename = "popGame";
    private static string popName = "haspopped";
    public static void Save(BalloonSO so){
        PlayerPrefs.SetString(gamename, $"{so.firstStart}:{so.totalFallTime}:{so.startTime}:{so.popped}");
    }

    public static string Load(){
        return PlayerPrefs.GetString(gamename,"");
    }

    public static void SavePopped(){
        PlayerPrefs.SetInt(popName, 13);
    }

    public static bool HasPopped(){
        return PlayerPrefs.GetInt(popName, 0) == 13;
    }
}