using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUtils
{
    public static Color GetColorFromArray(float[] values, float alpha = 1)
    {
        return new Color(values[0], values[1], values[2], alpha);
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
        return earnedGold / 100;
    }

    public static int CalculateLevel(int _xp)
    {
        _xp /= 10;
        int _level = Mathf.FloorToInt(0.1f * Mathf.Sqrt(_xp));
        return _level + 1;
    }


    public static int CalculateNextLevelXP(int _xp)
    {
        int _neededXP = 0;
        int _lvl = 1;
        int _neededLvl = CalculateLevel(_xp) + 1;
        while (_lvl < _neededLvl)
        {
            _neededXP++;
            _lvl = CalculateLevel(_neededXP);
        }

        
        return _neededXP; //needed XP
    }

    public static int GetXpForLevel(int _level)
    {
        int _xp = 0;

        int _tryLvl = CalculateLevel(_xp);

        while (_tryLvl < _level)
        {
            _xp++;
            _tryLvl = CalculateLevel(_xp);
        }

        return _xp;
    }
}
