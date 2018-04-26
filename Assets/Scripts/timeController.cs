using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class timeController : MonoBehaviour {

    public Text timeText, timeoutText;
    public objectController objectcontroller;
    public int time;
    private IEnumerator timeCoroutine;

    private void Start()
    {
        timeoutText.enabled = false;
        timeText.text = "Time left:\r\n  " + time;
    }

    public void startTimer()
    {
        timeCoroutine = timerUpdate(time);
        StartCoroutine(timeCoroutine);
    }

    public void resetTimer()
    {
        timeoutText.enabled = false;
        timeText.text = "Time left:\r\n  " + time;
        objectcontroller.allowInteraction = true;
        StopCoroutine(timeCoroutine);
    }

    IEnumerator timerUpdate(int initTime)
    {
        for(int currentTime = initTime; currentTime >= 0; currentTime --)
        {
            timeText.text = "Time left:\r\n  " + currentTime;
            yield return new WaitForSeconds(1f);
        }

        timeoutText.enabled = true;
        objectcontroller.allowInteraction = false;
    }
}
