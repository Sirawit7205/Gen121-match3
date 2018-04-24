using System.Linq;
using UnityEngine;

public class boardSpawner : MonoBehaviour {

    public GameObject sourceButton;
    public Transform parentTransform;
    public Texture2D[] textureList;
    public float spacingDistance;
    public int boardW, boardH;

	void Start () {

        float initX, posX, posY, stepX, stepY;
        GameObject current;

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
                current = Instantiate(sourceButton, new Vector3(posX, posY, 0), Quaternion.identity, parentTransform);
                current.GetComponent<shape>().texture = randomizeTexture(i, j);
                current.GetComponent<interaction>().gridPosH = i;
                current.GetComponent<interaction>().gridPosW = j;
                parentTransform.GetComponent<objectController>().buttonList[i, j] = current;
                posX += (stepX + spacingDistance);
            }
            posY -= (stepY + spacingDistance);
        }
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
