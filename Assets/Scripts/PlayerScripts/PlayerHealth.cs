using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Slider sl;

    float maxValue;
    float currentValue;
    float goalValue;

    private void Start()
    {
        sl = GetComponentInChildren<Slider>();

        //Set slider invisible
    }

    public void SetInitValues(float maxPlayerHealth)
    {
        maxValue = maxPlayerHealth;
        sl.maxValue = maxValue;
        sl.value = maxValue;
        UpdatePlayerHealthBar(maxValue);
    }

    public void UpdatePlayerHealthBar(float _newHealth)
    {
        goalValue = _newHealth;

        //Show slider
    }

    private void FixedUpdate()
    {
        currentValue = Mathf.Lerp(currentValue, goalValue, Time.deltaTime * 5);
    }


}
