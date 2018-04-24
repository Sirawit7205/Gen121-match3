using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class boardSpawner : MonoBehaviour
{

    public GameObject sourceButton;
    public Transform parentTransform;
    public Texture2D[] textureList;
    public float spacingDistance, generateDelayTime;
    public int boardW, boardH;
    public bool idle = true;

    void Start()
    {
        StartCoroutine(generateBoard());
    }

    public IEnumerator generateBoard()
    {
        float initX, posX, posY, stepX, stepY;
        GameObject current;

        idle = false;

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
                if(parentTransform.GetComponent<objectController>().buttonList[i, j] == null)
                {
                    current = Instantiate(sourceButton, new Vector3(posX, posY, 0), Quaternion.identity, parentTransform);
                    current.GetComponent<shape>().texture = randomizeTexture(i, j);
                    current.GetComponent<interaction>().gridPosH = i;
                    current.GetComponent<interaction>().gridPosW = j;
                    parentTransform.GetComponent<objectController>().buttonList[i, j] = current;
                    yield return new WaitForSeconds(generateDelayTime);
                }
                posX += (stepX + spacingDistance);                
            }
            posY -= (stepY + spacingDistance);
        }
        idle = true;
    }

    private Texture2D randomizeTexture(int i, int j)
    {
        Texture2D[] availableTextures = textureList;
        GameObject[,] btns = parentTransform.GetComponent<objectController>().buttonList;

        if (i > 0)
            availableTextures = availableTextures.Where(tex => tex != btns[i - 1, j].GetComponent<shape>().texture).ToArray();
        if (j > 0)
            availableTextures = availableTextures.Where(tex => tex != btns[i, j - 1].GetComponent<shape>().texture).ToArray();

        return availableTextures[Random.Range(0, availableTextures.Length)];
    }
}
