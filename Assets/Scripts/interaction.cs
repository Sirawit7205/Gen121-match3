using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour {

    public int gridPosH, gridPosW, animationSteps;
    public RectTransform panelTransform;
    public float spacingDistance, animationDelay;
    private float posX, posY;

    public void RecordInitialPosition()
    {
        posX = Input.mousePosition.x;
        posY = Input.mousePosition.y;
    }

    public void DetermineDragDirection()
    {
        objectController objCon = transform.parent.gameObject.GetComponent<objectController>();
        float newPosX = Input.mousePosition.x;
        float newPosY = Input.mousePosition.y;

        if(transform.GetComponentInParent<objectController>().allowInteraction)
        {
            if (Mathf.Abs(newPosX - posX) >= Mathf.Abs(newPosY - posY))         //left-right
            {
                if (newPosX >= posX)
                    objCon.moveAdjacent(gridPosH, gridPosW, 1, true);    //right
                else
                    objCon.moveAdjacent(gridPosH, gridPosW, 0, true);    //left
            }
            else                                                                //up-down
            {
                if (newPosY >= posY)
                    objCon.moveAdjacent(gridPosH, gridPosW, 2, true);     //up
                else
                    objCon.moveAdjacent(gridPosH, gridPosW, 3, true);     //down
            }
        }
    }

    public void SlideObject(int direction)
    {
        if (direction == 0)            //left
        {
            StartCoroutine(dragWithAnimation(transform.position, transform.position + (Vector3.left * (panelTransform.rect.width + spacingDistance))));
            gridPosW--;
        }
        else if (direction == 1)       //right
        {
            StartCoroutine(dragWithAnimation(transform.position, transform.position + (Vector3.right * (panelTransform.rect.width + spacingDistance))));
            gridPosW++;
        }
        else if (direction == 2)       //up
        {
            StartCoroutine(dragWithAnimation(transform.position, transform.position + (Vector3.up * (panelTransform.rect.height + spacingDistance))));
            gridPosH--;
        }
        else if (direction == 3)       //down
        {
            StartCoroutine(dragWithAnimation(transform.position, transform.position + (Vector3.down * (panelTransform.rect.height + spacingDistance))));
            gridPosH++;
        }
    }

    IEnumerator dragWithAnimation(Vector3 startPos, Vector3 endPos)
    {
        //Debug.Log("Now dragging from" + startPos + " to " + endPos);
        transform.GetComponentInParent<objectController>().allowInteraction = false;

        for (int i = 1;i <= animationSteps;i++)
        {
            transform.position = Vector3.Lerp(startPos, endPos, ((float)i/animationSteps));
            //Debug.Log("Step " + i + " current position = " + transform.position);
            yield return new WaitForSeconds(animationDelay);
        }

        transform.GetComponentInParent<objectController>().allowInteraction = true;
    }
}
