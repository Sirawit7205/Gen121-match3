using UnityEngine;
using UnityEngine.UI;

public class scoreController : MonoBehaviour {

    public Text scoreText;
    private int comboMultiplier = 1, score = 0;

    private void Start()
    {
        updateText();       //display init text
    }

    public void addPoints(int points, bool isCombo)
    {
        if (isCombo)
            comboMultiplier++;          //add a combo
        else
            comboMultiplier = 1;        //initial match, reset combo

        score += points * comboMultiplier;      //add score

        updateText();                   //update new score
    }

    public void resetPoints()
    {
        score = 0;                      //reset points to init
        comboMultiplier = 1;

        updateText();                   //update init text
    }

    private void updateText()
    {
        //score text
        scoreText.text = "Score:" + "\r\n " + score + "\r\nCombos:\r\n " + comboMultiplier;
    }
}
