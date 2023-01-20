using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * TODO: 
 */
public class NPCManager : MonoBehaviour
{
    public static NPCManager npcManager;
    public GameObject[] NPCArray { get; private set; }
    public int numNPC { get; private set; } = 5;
    private GameObject NPC;
    private IDictionary<int, List<(int id, Vector3 destination)>> npcSchedule;
    private int xMin = -2;
    private int xMax = 8;
    private float y = 0.4f;
    private int zMin = -5;
    private int zMax = 5;

    void Awake()
    {
        npcManager = this;
        NPCArray = new GameObject[numNPC];
    }
    // Start is called before the first frame update
    void Start()
    {
        NPC = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        for (var i = 0; i < NPCArray.Length; i++)
        {
            var position = new Vector3(Random.Range(xMin, xMax), y, Random.Range(zMin, zMax));
            NPCArray[i] = SpawnNPC(position);
            NPCArray[i].AddComponent(typeof(Citizen));
            NPCArray[i].AddComponent(typeof(UnityEngine.AI.NavMeshAgent));
            NPCArray[i].GetComponent<Citizen>().agent = NPCArray[i].GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        npcSchedule = GenerateSchedule(NPCArray.Length);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var itinerary in npcSchedule[GameClockDisplay.gameClock.currTime])
        {
            NPCArray[itinerary.id].GetComponent<Citizen>().agent.destination = itinerary.destination;
        }
    }

    private GameObject SpawnNPC(Vector3 spawnPosition)
    {
        return Instantiate(NPC, spawnPosition, Quaternion.identity);
    }

    /**
     * Function to generate a random schedule dictionary. Each key corresponds to a time of day, while each value corresponds
     * to a list of tuples. Each tuple is formatted as (NPC ID #, Destination). Essentially, the schedule is generated as a
     * lookup table, where a list of NPC destinations are stored corresponding to a time of day.
     **/
    private IDictionary<int, List<(int, Vector3)>> GenerateSchedule(int numNPCS, bool random = true)
    {
        IDictionary<int, List<(int, Vector3)>> initSchedule = new Dictionary<int, List<(int, Vector3)>>();
        int key;
        (int id, Vector3 destination) randItinerary;
        //For random generation
        int numItineraries;
        int randHour;
        int randMin;
        int randTime;
       
        //Generate an empty schedule dictionary
        for (int hour = 0; hour < 24; hour++)
        {
            for (int min = 0; min < 60; min++)
            {
                key = hour * 100 + min;
                initSchedule.Add(key, new List<(int id, Vector3 destination)>());
            }
        }
        
        //Randomly assign itineraries
        if (random)
        {
            for (int id = 0; id < NPCArray.Length; id++)
            {
                numItineraries = Random.Range(0, 5);
                for (int i = 0; i < numItineraries; i++)
                {
                    randHour = 8; //Random.Range(0,23);
                    randMin = Random.Range(0, 59);
                    randTime = randHour * 100 + randMin;
                    randItinerary = (id,new Vector3(Random.Range(xMin, xMax), y, Random.Range(zMin, zMax)));
                    initSchedule[randTime].Add(randItinerary);
                }
            }
        }
        
        return initSchedule;
    }
}
