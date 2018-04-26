using System;
using UnityEngine;
using UnityEngine.UI;

public class scoreController : MonoBehaviour {

    public Text scoreText;
    private int comboMultiplier = 1, score = 0;

    private void Start()
    {
        updateText();
    }

    public void addPoints(int points, bool isCombo)
    {
        if (isCombo)
            comboMultiplier++;
        else
            comboMultiplier = 1;

        score += points * comboMultiplier;

        updateText();
    }

    public void resetPoints()
    {
        score = 0;
        comboMultiplier = 1;

        updateText();
    }

    private void updateText()
    {
        scoreText.text = "Score:" + "\r\n " + score + "\r\nCombos:\r\n " + comboMultiplier;
    }
}
