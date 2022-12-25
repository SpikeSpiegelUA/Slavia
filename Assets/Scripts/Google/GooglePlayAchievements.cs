using UnityEngine;
using GooglePlayGames;

public static class GooglePlayAchievements
{
    public static void UnlockRegular(string achievemntID)
    {
        Social.ReportProgress(achievemntID, 100f, null);
    }
}
