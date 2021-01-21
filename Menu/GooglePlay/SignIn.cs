using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class SignIn : MonoBehaviour
{
    public static PlayGamesPlatform platform;
    // Start is called before the first frame update
    void Start() {
    
    Debug.Log(platform);
     if(platform == null)
     {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        
        platform = PlayGamesPlatform.Activate();
        Debug.Log("Activated");
        
        
     }
     Social.Active.localUser.Authenticate(sucess =>
     {
         if(sucess)
         {
             Debug.Log("Logged in sucessfully");
         }

        else
        {
            Debug.Log("Failed to log in");
        }

     });

    }

}
