using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl : MonoBehaviour {
    public GameObject boardControl;
    public int maxH, maxW;
    private bool[,] removed = new bool[9, 9];

    public int checkValidMoves()
    {
        int removeCount = 0;

        for (int i = 0; i < maxH; i++)
        {
            for (int j = 0; j < maxW; j++)
            {
                removeCount += checkAndRemoveMatch(i, j);
            }
        }
        return removeCount;
    }

    private int checkAndRemoveMatch(int i, int j)
    {
        GameObject[,] btns = boardControl.GetComponent<objectController>().buttonList;
        int up = 0, down = 0, left = 0, right = 0, removeCount = 0;

        if (!removed[i, j])
        {
            while (i - up - 1 >= 0 && btns[i, j].GetComponent<shape>().texture == btns[i - up - 1, j].GetComponent<shape>().texture)
                up++;

            while (i + down + 1 < maxH && btns[i, j].GetComponent<shape>().texture == btns[i + down + 1, j].GetComponent<shape>().texture)
                down++;

            while (j - left - 1 >= 0 && btns[i, j].GetComponent<shape>().texture == btns[i, j - left - 1].GetComponent<shape>().texture)
                left++;

            while (j + right + 1 < maxW && btns[i, j].GetComponent<shape>().texture == btns[i, j + right + 1].GetComponent<shape>().texture)
                right++;
        }

        //Debug.Log("Matches colors in UDLR: " + up + " " + down + " " + left + " " + right);

        if (up + down + 1 >= 3)
        {
            for (int k = i - 1; k >= i - up; k--)
            {
                Destroy(btns[k, j], 0.2f);
                removed[k, j] = true;
            }

            Destroy(btns[i, j], 0.2f);
            removed[i, j] = true;

            for (int k = i + 1; k <= i + down; k++)
            {
                Destroy(btns[k, j], 0.2f);
                removed[k, j] = true;
            }

            removeCount = up + down + 1;
        }
        else if(left + right +1 >=3)
        {
            for (int k = j - 1; k >= j - left; k--)
            {
                Destroy(btns[i, k], 0.2f);
                removed[i, k] = true;
            }

            Destroy(btns[i, j], 0.2f);
            removed[i, j] = true;

            for (int k = j + 1; k <= j + right; k++)
            {
                Destroy(btns[i, k], 0.2f);
                removed[i, k] = true;
            }

            removeCount = left + right + 1;
        }

        return removeCount;
    }
}
