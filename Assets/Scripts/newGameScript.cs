using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGameScript : MonoBehaviour {

    public boardSpawner boardspawner;
    public scoreController scorecontrol;
    public objectController objectcontrol;
    public timeController timecontrol;

    public void newGame()
    {
        destroyAllObjects();                    //destroy the whole board
        scorecontrol.resetPoints();             //reset score
        timecontrol.resetTimer();               //reset time
        StartCoroutine(regenerateBoard());      //regenerate the whole board
    }

    private void destroyAllObjects()
    {
        for (int i = 0; i < objectcontrol.maxH; i++)
            for (int j = 0; j < objectcontrol.maxW; j++)
                Destroy(objectcontrol.buttonList[i, j]);    //destroy everything
    }

    IEnumerator regenerateBoard()
    {
        yield return null;          //wait for next frame
        StartCoroutine(boardspawner.generateBoard(false));  //generate board
    }
}
