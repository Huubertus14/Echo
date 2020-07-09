using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PersistenceSpawner : MonoBehaviour
{
    

    [SerializeField] private GameObject persistencePrefab;
    private GameObject persistence;

    private bool triedToAuthenticate = false;

    private void Awake()
    {
        persistence = Instantiate(persistencePrefab, transform.position, Quaternion.identity);
        persistence.name = "Persistence";
    }

    private IEnumerator Start()
    {
        //Ask for read permission

        yield return 0;

        SignInGooglePlay();
        yield return new WaitUntil(()=> triedToAuthenticate == true);

        while (!LoadPlayerData(SaveData.LoadData()))
        {
            yield return 0;
        }

        //Go to next scene/Remove loading panel
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
            else
            {
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
            }
            triedToAuthenticate = true;
        });
    }

    /// <summary>
    /// Sets all values to the right thing in the game
    /// </summary>
    /// <param name="savedata"></param>
    /// <returns></returns>
    private bool LoadPlayerData(PlayerData savedata)
    {
        GameManager man = persistence.GetComponent<GameManager>();
        if (man != null)
        {
            man.LoadGame(savedata);
            return true;
        }
        else
        {
            Debug.LogError("Could not load data");
            return false;
        }
    }

}

