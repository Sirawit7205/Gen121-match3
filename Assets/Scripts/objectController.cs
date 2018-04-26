using System.Collections;
using UnityEngine;

public class objectController : MonoBehaviour
{
    public gameController gamecontrol;
    public scoreController scorecontrol;
    public timeController timecontrol;
    public GameObject[,] buttonList = new GameObject[9, 9];
    public int maxH, maxW;
    public bool allowInteraction = true;

    public void moveAdjacent(int h, int w, int dir, bool checkValid)
    {
        gameController gCon = gamecontrol.GetComponent<gameController>();
        int removeCount = 0;

        if (timecontrol.currentTime == 0)        //if timeout, do nothing
            return;

        if (dir == 0)                            //left
        {
            if (w > 0)
            {
                buttonList[h, w].GetComponent<interaction>().SlideObject(0, 1);         //swap L-R
                buttonList[h, w - 1].GetComponent<interaction>().SlideObject(1, 1);
                swapObj(h, w, h, w - 1);
                removeCount = gCon.checkValidMoves();               //check if this is a valid move
                if (checkValid && removeCount == 0)                 //invalid
                    StartCoroutine(swapOnFailedMove(h, w, 0));      //swap back
            }
        }
        else if (dir == 1)                       //right
        {
            if (w < maxW - 1)
            {
                buttonList[h, w].GetComponent<interaction>().SlideObject(1, 1);         //swap R-L
                buttonList[h, w + 1].GetComponent<interaction>().SlideObject(0, 1);
                swapObj(h, w, h, w + 1);
                removeCount = gCon.checkValidMoves();
                if (checkValid && removeCount == 0)
                    StartCoroutine(swapOnFailedMove(h, w, 1));
            }
        }
        else if (dir == 2)                       //up
        {
            if (h > 0)
            {
                buttonList[h, w].GetComponent<interaction>().SlideObject(2, 1);         //swap U-D
                buttonList[h - 1, w].GetComponent<interaction>().SlideObject(3, 1);
                swapObj(h, w, h - 1, w);
                removeCount = gCon.checkValidMoves();
                if (checkValid && removeCount == 0)
                    StartCoroutine(swapOnFailedMove(h, w, 2));
            }
        }
        else if (dir == 3)                       //down
        {
            if (h < maxH - 1)
            {
                buttonList[h, w].GetComponent<interaction>().SlideObject(3, 1);         //swap D-U
                buttonList[h + 1, w].GetComponent<interaction>().SlideObject(2, 1);
                swapObj(h, w, h + 1, w);
                removeCount = gCon.checkValidMoves();
                if (checkValid && removeCount == 0)
                    StartCoroutine(swapOnFailedMove(h, w, 3));
            }
        }
        //Debug.Log("On main event, " + removeCount + " objects were removed.");

        //if this is a valid move
        if (checkValid && removeCount != 0)
        {
            StartCoroutine(gCon.slideDownAndFillBoard());       //fill the board again
            scorecontrol.addPoints(removeCount, false);         //add scores, no combo
        }
    }

    public void slideDown(int col, int src, int dest)
    {
        buttonList[src, col].GetComponent<interaction>().SlideObject(3, dest - src);    //slide the object down
        swapObj(src, col, dest, col);                           //swap null and existing object
    }

    private void swapObj(int x1, int y1, int x2, int y2)
    {
        GameObject temp;

        temp = buttonList[x1, y1];                  //swapping
        buttonList[x1, y1] = buttonList[x2, y2];
        buttonList[x2, y2] = temp;
    }

    IEnumerator swapOnFailedMove(int x, int y, int dir)
    {
        while (!allowInteraction) yield return new WaitForSeconds(0.01f);   //wait until others animation completed
        moveAdjacent(x, y, dir, false);             //swap back
    }
}
