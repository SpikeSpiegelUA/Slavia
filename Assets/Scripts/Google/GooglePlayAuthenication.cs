using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class GooglePlayAuthenication : MonoBehaviour
{
    public static PlayGamesPlatform playGamesPlatform;
    // Start is called before the first frame update
    void Start()
    {
        Authenication();
    }
    public void Authenication()
    {
        if (playGamesPlatform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            playGamesPlatform = PlayGamesPlatform.Activate();
        }
        Social.Active.localUser.Authenticate(success => {
        });
    }
    public void OpenAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }

}
