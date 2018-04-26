using System.Collections;
using System.Linq;
using UnityEngine;

public class boardSpawner : MonoBehaviour
{
    public timeController timeController;
    public objectController objectController;
    public GameObject sourceButton;
    public Transform parentTransform;
    public Texture2D[] textureList;
    public float spacingDistance, generateDelayTime;
    public int boardW, boardH;
    public bool idle = true;

    void Start()
    {
        StartCoroutine(generateBoard(false));
    }

    public IEnumerator generateBoard(bool isUpdate)
    {
        float initX, posX, posY, stepX, stepY;
        GameObject current;

        //prevents other movements
        idle = false;
        objectController.allowInteraction = false;

        //get init positions on x,y axis + moving steps
        initX = posX = parentTransform.position.x + ((-1) * ((sourceButton.GetComponent<RectTransform>().rect.width * (boardW / 2)) + (spacingDistance * (boardW / 2))));
        posY = parentTransform.position.y - sourceButton.GetComponent<RectTransform>().rect.height;
        stepX = sourceButton.GetComponent<RectTransform>().rect.width;
        stepY = sourceButton.GetComponent<RectTransform>().rect.height;

        //Debug.Log("initX = " + posX + " stepX = " + stepX + "initY = " + posY + " stepY = " + stepY);

        for (int i = 0; i < boardH; i++)
        {
            posX = initX;
            for (int j = 0; j < boardW; j++)
            {
                if(parentTransform.GetComponent<objectController>().buttonList[i, j] == null)       //if there's nothing or the object was destroyed at this index 
                {
                    current = Instantiate(sourceButton, new Vector3(posX, posY, 0), Quaternion.identity, parentTransform);      //instantiate a new instance of object
                    current.GetComponent<shape>().texture = randomizeTexture(i, j);                 //random and set color
                    current.GetComponent<interaction>().gridPosH = i;                               //set index i,j
                    current.GetComponent<interaction>().gridPosW = j;
                    parentTransform.GetComponent<objectController>().buttonList[i, j] = current;    //save to array
                    yield return new WaitForSeconds(generateDelayTime);                             //animation delay
                }
                posX += (stepX + spacingDistance);                
            }
            posY -= (stepY + spacingDistance);
        }

        //resumes interactions
        idle = true;
        objectController.allowInteraction = true;

        //start timer on application start/new game
        if (!isUpdate)
            timeController.startTimer();
    }

    private Texture2D randomizeTexture(int i, int j)
    {
        Texture2D[] availableTextures = textureList;
        GameObject[,] btns = parentTransform.GetComponent<objectController>().buttonList;

        //remove color of upper and left objects from random pool
        if (i > 0)
            availableTextures = availableTextures.Where(tex => tex != btns[i - 1, j].GetComponent<shape>().texture).ToArray();
        if (j > 0)
            availableTextures = availableTextures.Where(tex => tex != btns[i, j - 1].GetComponent<shape>().texture).ToArray();

        //randomize and return
        return availableTextures[Random.Range(0, availableTextures.Length)];
    }
}
