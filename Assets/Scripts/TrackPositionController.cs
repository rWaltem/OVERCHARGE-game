using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPositionController : MonoBehaviour
{
    [Header("Gates")]
    public int currentGate = -1;
    public int prevGate = -1;
    public int finalGate;

    [Header("Time")]
    public int currentLap = 0;
    public List<float> lapTimes = new List<float>();
    public float startTime;
    public float currentTime;

    void Start()
    {
        // start method
    }

    void Update()
    {
        currentTime = Time.time;

        if (currentGate == 0 && prevGate == -1)
        {
            // start race
            Debug.Log("Start race");
            currentLap++;
            startTime = Time.time;

            prevGate = currentGate;

        } else if (currentGate == 0 && prevGate == finalGate)
        {
            // new lap
            lapTimes.Add(currentTime - startTime);
            Debug.Log($"Lap {currentLap} had a time of: {lapTimes[currentLap - 1]}");
            
            startTime = Time.time;
            Debug.Log($"New lap. Starts at: {startTime}");

            currentLap++;
            prevGate = currentGate;

        } else if (currentGate == (prevGate + 1))
        {
            // passed gate
            Debug.Log($"Gate {currentGate} passed");
            prevGate = currentGate;
        }
    }
}
