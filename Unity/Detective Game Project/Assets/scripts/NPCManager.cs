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
            //Spawn NPC at random position
            var position = new Vector3(Random.Range(xMin, xMax), y, Random.Range(zMin, zMax));
            NPCArray[i] = SpawnNPC(position);

            NPCArray[i].AddComponent(typeof(Citizen));
            NPCArray[i].AddComponent(typeof(UnityEngine.AI.NavMeshAgent));
            NPCArray[i].GetComponent<Citizen>().agent = NPCArray[i].GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        bool random = true;
        GenerateSchedule(random);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void GenerateSchedule(bool random = true)
    {
        //For random generation
        Vector3 randDestination;
        int numItineraries;
        int maxItineraries = 5;
        int minItineraries = 0;
        int randHour;
        int randMin;
        int randTime;
       
        //Randomly assign itineraries
        if (random)
        {
            for (int id = 0; id < numNPC; id++)
            {
                numItineraries = Random.Range(minItineraries, maxItineraries);
                (int, Vector3)[] randSchedule = new (int, Vector3)[numItineraries];
                for (int i = 0; i < numItineraries; i++)
                {
                    randHour = 8; //Random.Range(0,23);
                    randMin = Random.Range(0, 59);
                    randTime = randHour * 100 + randMin;
                    randDestination = new Vector3(Random.Range(xMin, xMax), y, Random.Range(zMin, zMax));
                    randSchedule[i] = (randTime, randDestination);
                }
                NPCArray[id].GetComponent<Citizen>().AddToSchedule(randSchedule);
            }
        }
    }
}
