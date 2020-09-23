using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtils : MonoBehaviour
{
    public static Color GetColorFromArray(float[] values, float alpha =1)
    {
        return new Color(values[0],values[1],values[2],alpha);
    }

    public static bool IsGameScene()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene"))
        {
            return true;
        }
        return false;
    }

    public static int CalculateEarnedGold(int _score, int _xp)
    {
        int earnedGold = 0;
        earnedGold += GameConstants.BASE_MATCH_GOLD;
        earnedGold += _score;
        earnedGold *= _xp;
        return earnedGold/100;
    }
}
