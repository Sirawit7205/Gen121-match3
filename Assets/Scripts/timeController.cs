using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class timeController : MonoBehaviour {

    public Text timeText, timeoutText;
    public objectController objectcontroller;
    public int time, currentTime;
    private IEnumerator timeCoroutine;

    private void Start()
    {
        timeoutText.enabled = false;                //init texts
        timeText.text = "Time left:\r\n  " + time;
    }

    public void startTimer()
    {
        timeCoroutine = timerUpdate(time);      //save coroutine for stopping later
        StartCoroutine(timeCoroutine);          //start coroutine
    }

    public void resetTimer()
    {
        timeoutText.enabled = false;                //reset texts
        timeText.text = "Time left:\r\n  " + time;
        objectcontroller.allowInteraction = true;   //enable interaction
        StopCoroutine(timeCoroutine);               //stop previous timer
    }

    IEnumerator timerUpdate(int initTime)
    {
        for(int i = initTime; i >= 0; i --)
        {
            currentTime = i;                        //save current time
            timeText.text = "Time left:\r\n  " + currentTime;   //update text
            yield return new WaitForSeconds(1f);    //wait for 1 second
        }

        timeoutText.enabled = true;                 //time out
        objectcontroller.allowInteraction = false;
    }
}
