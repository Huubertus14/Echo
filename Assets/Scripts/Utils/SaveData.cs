using System.IO;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using Photon.Pun;
using GooglePlayGames.BasicApi.Multiplayer;

public class SaveData : MonoBehaviour
{

    private static string saveName = "gamesave";
    private static bool saving = false;

    private static PlayerData loadedData;

    public static PlayerData LoadData()
    {
        Debug.Log("path: "+Application.persistentDataPath);
        saving = false;
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            //  Debug.Log("Try to load from GP");
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            savedGameClient.OpenWithAutomaticConflictResolution(saveName, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        }
        if (loadedData == null)
        {
            return LoadLocal();
        }
        else
        {
            if (String.IsNullOrEmpty(loadedData.playerName))
            {
                PhotonNetwork.NickName = PlayGamesPlatform.Instance.GetUserId();
            }
        }
        return loadedData;
    }

    private static string SaveGameName => Application.persistentDataPath + "/gamesave.save";


    private static PlayerData LoadLocal()
    {
        // Debug.Log("Load local");

        if (File.Exists(SaveGameName))
        {
            try
            {
                FileStream file = new FileStream(SaveGameName, FileMode.Open);
                if (file.Length > 0)
                {
                    // Debug.Log("File exist");
                    BinaryFormatter bf = new BinaryFormatter();
                    PlayerData save = (PlayerData)bf.Deserialize(file);
                    file.Close();
                    return save;
                }
                else
                {
                    return CreateNewSaveFile();
                }
            }
            catch (System.Exception e)
            {
                
                Debug.LogError("Failed to load data " + e);
                throw;
            }
        }
        else
        {
            return CreateNewSaveFile();
        }
    }

    private static PlayerData CreateNewSaveFile()
    {
        // Debug.Log("File does not exist, creating a new one");
        PlayerData save = new PlayerData();
        save.ResetData();
        return save; //First boot ever
    }

    private static void SaveLocal(PlayerData save)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(SaveGameName, FileMode.Create, FileAccess.Write);
        bf.Serialize(file, save);
        file.Close();
    }

    public static void Save(PlayerData save)
    {
        if (save == null)
        {
            //Debug.LogWarning("Save is empty");
            return;
        }
        loadedData = save;
        SaveLocal(save);//Always save a local game
        saving = true;
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            //Save on Googe play
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            savedGameClient.OpenWithAutomaticConflictResolution(saveName, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        }
        else
        {
            //Debug.Log("Not authenticated GP");
        }
    }

    public static void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //Debug.Log("SaveOpen");
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            if (saving) //Save the game
            {
                // Debug.Log("SaveGP");
                SaveGooglePlay(game, ObjectSerializationExtension.SerializeToByteArray(loadedData), TimeSpan.FromMinutes(0));
            }
            else //load the game
            {
                // Debug.Log("LoadGP");
                LoadFromGooglePlay(game);
            }
        }
        else
        {
            // handle error
            //Debug.LogWarning("Could not open save game");
            //Always save local, so ony loaded needs to be done here
            if (!saving)
            {
                LoadLocal();
            }
        }
    }

    private static void SaveGooglePlay(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        //Debug.Log("SavingGP");
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    private static void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        // Debug.Log("DoneSavingGP: " + status);
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
        }
    }

    private static void LoadFromGooglePlay(ISavedGameMetadata game)
    {
        // Debug.Log("LoadfromGP");
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private static void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        //Debug.Log("DoneLoading GP: " +status);
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
            loadedData = ObjectSerializationExtension.Deserialize<PlayerData>(data);
        }
        else
        {
            // handle error
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int playerXP;

    //Overal Game data
    public int gold;
    public int wins;
    public int loses;
    public int damageDone;
    public int totalKills;
    public int totalDeaths;
    public float timePlayed;

    //Sub values
    public int subTypeSelected;
    public int baseSubXP;

    private float playerR;
    private float playerG;
    private float playerB;


    public void SetPlayerColor(float r, float g, float b)
    {
        r = Mathf.Clamp(r, 0, 1.0f);
        g = Mathf.Clamp(g, 0, 1.0f);
        b = Mathf.Clamp(b, 0, 1.0f);

        playerR = r;
        playerB = b;
        playerG = g;
    }

    /*  public void SetPlayerColor(Color playerCol)
      {
          r = Mathf.Clamp(r, 0, 1.0f);
          g = Mathf.Clamp(g, 0, 1.0f);
          b = Mathf.Clamp(b, 0, 1.0f);

          playerR = r;
          playerB = b;
          playerG = g;
      }*/

    public float[] GetColor()
    {
        return new float[3] { playerR, playerG, playerB };
    }

    public void ResetData()
    {
        playerName = "Commander";
        playerXP = 0;
        gold = 0;
        wins = 0;
        loses = 0;
        damageDone = 0;
        totalKills = 0;
        totalDeaths = 0;
        timePlayed = 0;
        subTypeSelected = 1;
        SetPlayerColor(0,0,1.0f);
    }

    public override string ToString()
    {
        base.ToString();
        return playerName + ", " + playerXP + ", " + gold;
    }
}

//Extension class to provide serialize / deserialize methods to object.
//src: http://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
//NOTE: You need add [Serializable] attribute in your class to enable serialization
public static class ObjectSerializationExtension
{
    public static byte[] SerializeToByteArray(this object obj)
    {
        if (obj == null)
        {
            return null;
        }
        var bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    public static T Deserialize<T>(this byte[] byteArray) where T : class
    {
        if (byteArray == null)
        {
            return null;
        }
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            memStream.Write(byteArray, 0, byteArray.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = (T)binForm.Deserialize(memStream);
            return obj;
        }
    }
}