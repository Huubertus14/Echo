using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KillFeedback : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    //public void CreateFeedback
}
