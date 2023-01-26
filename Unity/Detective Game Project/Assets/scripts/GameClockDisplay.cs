using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClockDisplay : MonoBehaviour
{
    //Allows this class to be accessed anywhere
    public static GameClockDisplay gameClock;

    public TMPro.TextMeshProUGUI timeCounter;
    //Written as a military time integer, i.e. 1:00 PM = 1300
    public int currTime { get; private set; }
    //private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;
    private int currSec;
    private int nextSec;
    private int hour;
    private int min;
    private bool isAM;

    //purpose is to determine how many seconds are in a minute in-game.
    private int numSecInMin = 1;

    void Awake() => gameClock = this;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter.text = "8:00 AM";
        timerGoing = false;
        hour = 8;
        min = 0;
        BeginTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            nextSec = (int)(elapsedTime % 60);
            if (currSec != nextSec && nextSec % numSecInMin == 0)
            {
                min += 1;
            }
            if (min >= 60)
            {
                min = 0;
                if (++hour >= 24)
                {
                    hour = 0;
                }
            }
            //Debug.Log(hour >= 13);
            currSec = (int)(elapsedTime % 60); 
            timeCounter.text = ((hour < 13) ? hour : hour - 12) + ":" + ((min < 10) ? "0" + min : min) + " " + ((hour < 13) ? "AM" : "PM");
            //Convert hour and min string to int, then assign to currTime
            currTime = hour * 100 + min; 
            //Debug.Log(currTime);
        }
    }

    public void BeginTimer()
    {
        timerGoing = true;
        //startTime = Time.time;
        elapsedTime = 0f;
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

}
