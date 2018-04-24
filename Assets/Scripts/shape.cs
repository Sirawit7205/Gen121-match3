using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shape : MonoBehaviour {

    public float opacity = 0.25f;
    public Texture2D texture;
    public RectTransform panelTransform;

    private void OnGUI()
    {
        //GUI.color.a = opacity;
        GUI.depth = 0;
        GUI.DrawTexture(new Rect(panelTransform.position.x - (panelTransform.rect.width/2), 
                                 Screen.height -  panelTransform.position.y - (panelTransform.rect.height/2),
                                 panelTransform.rect.width,
                                 panelTransform.rect.height), 
                                 texture);
        //Debug.Log(panelTransform.position.x + " " + panelTransform.position.y + " " + panelTransform.rect.width + " " + panelTransform.rect.height);
    }
}
