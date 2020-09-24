using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuXPSlider : MonoBehaviour
{

    private Slider xpSlider;
    private float goalValue;


    private void Awake()
    {
        xpSlider = GetComponentInChildren<Slider>();
    }

    public void InitSlider()
    {
        //Set max value of slider to next lvl xp
        xpSlider.maxValue = GameUtils.CalculateNextLevelXP(GameManager.SP.playerData.playerXP); 
        Debug.Log("nxt LVL needed: " + GameUtils.CalculateNextLevelXP(GameManager.SP.playerData.playerXP));
        xpSlider.minValue = GameUtils.GetXpForLevel(GameUtils.CalculateLevel(GameManager.SP.playerData.playerXP));

        //Set value of lsider to right xp
        xpSlider.value = GameManager.SP.playerData.playerXP;
        goalValue = xpSlider.value;
    }

    

    public void LevelUp()
    {
        //Set new slider value
    }

    public void AddXP(int _xpIncrease)
    {
        goalValue += _xpIncrease;
        StartCoroutine(IncreaseSlider());
    }

    private IEnumerator IncreaseSlider()
    {
        //Increase at 10 xp per frame
        while (xpSlider.value < goalValue)
        {
            xpSlider.value += 10;
            if (xpSlider.value > goalValue)
            {
                xpSlider.value = goalValue;
            }

            yield return 0;
        }

        yield return 0;
    }
}
