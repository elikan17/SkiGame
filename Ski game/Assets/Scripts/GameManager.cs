using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DateTime raceStart;
    private TimeSpan raceTime;
    private bool racing = false;
    public delegate void TimerEvent();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartGate.StartRace += StartRace;
        FinishGate.FinishRace += FinishRace;
    }

    void FinishRace()
    {
        racing = false;
        Debug.Log("Finish Race");
    }

    void StartRace()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("Starting race...");
    }

    // Update is called once per frame
    void Update()
    {
        if(racing)
            raceTime = DateTime.Now - raceStart;
        Debug.Log("race time: " + raceTime);
    }
}
