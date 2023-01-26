using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public string test = "hi";
    public int id { get; private set; }
    public IDictionary<int, Vector3> schedule { get; private set; } = new Dictionary<int, Vector3>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (schedule.ContainsKey(GameClockDisplay.gameClock.currTime))
        {
           agent.destination = schedule[GameClockDisplay.gameClock.currTime];
        }
    }

    /**
     * Setter method to add or modify schedule itineraries
     */
    public void AddToSchedule((int time, Vector3 destination)[] itineraries)
    {
        foreach (var item in itineraries)
        {
            if (schedule.ContainsKey(item.time))
            {
                schedule[item.time] = item.destination;
            }
            else
            {
                schedule.Add(item.time, item.destination);
            }
        }
        
    }

    /**
     * Setter method to remove schedule itineraries
     */
    public void RemoveFromSchedule(List <int> timeList)
    {
        foreach (var time in timeList)
        {
            schedule.Remove(time);
        }
    }

    /**
     * Setter method to clear the schedule
     */
    public void ClearSchedule()
    {
        schedule.Clear();
    }
}
