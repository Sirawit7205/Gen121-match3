using System.Collections;
using UnityEngine;

public class objectController : MonoBehaviour
{
    public gameControl gamecontrol;
    public GameObject[,] buttonList = new GameObject[9, 9];
    public int maxH, maxW;
    public bool allowInteraction = true;

    public void moveAdjacent(int h, int w, int dir, bool checkValid)
    {
        gameControl gCon = gamecontrol.GetComponent<gameControl>();
        int removeCount = 0;

        if (dir == 0)                            //left
        {
            if (h > 0)
            {
                buttonList[h, w].GetComponent<interaction>().SlideObject(0, 1);
                buttonList[h, w - 1].GetComponent<interaction>().SlideObject(1, 1);
                swapObj(h, w, h, w - 1);
                removeCount = gCon.checkValidMoves();
                if (checkValid && removeCount == 0)
                    StartCoroutine(swapOnFailedMove(h, w, 0));
            }
        }
        else if (dir == 1)                       //right
        {
            if (h < maxW - 1)
            {
                buttonList[h, w].GetComponent<interaction>().SlideObject(1, 1);
                buttonList[h, w + 1].GetComponent<interaction>().SlideObject(0, 1);
                swapObj(h, w, h, w + 1);
                removeCount = gCon.checkValidMoves();
                if (checkValid && removeCount == 0)
                    StartCoroutine(swapOnFailedMove(h, w, 1));
            }
        }
        else if (dir == 2)                       //up
        {
            if (w > 0)
            {
                buttonList[h, w].GetComponent<interaction>().SlideObject(2, 1);
                buttonList[h - 1, w].GetComponent<interaction>().SlideObject(3, 1);
                swapObj(h, w, h - 1, w);
                removeCount = gCon.checkValidMoves();
                if (checkValid && removeCount == 0)
                    StartCoroutine(swapOnFailedMove(h, w, 2));
            }
        }
        else if (dir == 3)                       //down
        {
            if (w < maxH - 1)
            {
                buttonList[h, w].GetComponent<interaction>().SlideObject(3, 1);
                buttonList[h + 1, w].GetComponent<interaction>().SlideObject(2, 1);
                swapObj(h, w, h + 1, w);
                removeCount = gCon.checkValidMoves();
                if (checkValid && removeCount == 0)
                    StartCoroutine(swapOnFailedMove(h, w, 3));
            }
        }
        //Debug.Log("On main event, " + removeCount + " objects were removed.");
        if (checkValid && removeCount != 0)
            StartCoroutine(gCon.slideDownAndFillBoard());
    }

    public void slideDown(int col, int src, int dest)
    {
        buttonList[src, col].GetComponent<interaction>().SlideObject(3, dest - src);
        swapObj(src, col, dest, col);
    }

    private void swapObj(int x1, int y1, int x2, int y2)
    {
        GameObject temp;

        temp = buttonList[x1, y1];
        buttonList[x1, y1] = buttonList[x2, y2];
        buttonList[x2, y2] = temp;
    }

    IEnumerator swapOnFailedMove(int x, int y, int dir)
    {
        while (!allowInteraction) yield return new WaitForSeconds(0.01f);
        moveAdjacent(x, y, dir, false);
    }
}
