using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private Text countDownTxt;

    private Timer countDownTimer;

    private Color normalColor;

    private void Start()
    {
        countDownTxt = GetComponent<Text>();
        normalColor = countDownTxt.color;
        countDownTimer = GameManager._instance.CountDown;
    }

    private void Update()
    {
        countDownTxt.text = (int)countDownTimer.CurrentTimer / 60 + ":" + (int)countDownTimer.CurrentTimer % 60;
        if (countDownTimer.CurrentTimer < 10) countDownTxt.color = Color.red;
        else countDownTxt.color = normalColor;
    }
}
