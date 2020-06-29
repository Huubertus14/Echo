using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PersistenceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject persistencePrefab;
    private void Awake()
    {
       GameObject per = Instantiate(persistencePrefab, transform.position, Quaternion.identity);
        per.name = "Persistence";
    }

    private IEnumerator Start()
    {
        SignInGooglePlay();
        yield return 0;

        while (!LoadPlayerData())
        {
            yield return 0;
        }

        //Go to next scene
    }

    private void SignInGooglePlay()
    {
        PlayGamesClientConfiguration gpConfig = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(gpConfig);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) =>
        {
            if (result != SignInStatus.Success)
            {
                Debug.LogWarning("Failed to GP sign in: " + result);
            }
        });
    }

    private bool LoadPlayerData()
    {
        return true;
    }

}

