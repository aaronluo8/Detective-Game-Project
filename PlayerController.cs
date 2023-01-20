using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 1;
    private int detectionRange = 1;
    private Citizen closestNPC;
    private GameObject playerObj = null;
    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null) playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Adapted for isometric movement
        var dir = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        transform.Translate(dir * speed * Time.deltaTime);

        bool npcInRange = false;
        closestNPC = getClosestNPCInRange();
        if (closestNPC) npcInRange = true;
        
        if (npcInRange && Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("NPC nearby");
        }
    }

    /*
     * Returns the closest NPC within detection range of the player character.
     */
    private Citizen getClosestNPCInRange()
    {
        float dist;
        float minDist = detectionRange;
        GameObject minNPC = null;
        Vector3 currPos = playerObj.transform.position;
        foreach (var npc in NPCManager.npcManager.NPCArray){
            dist = Vector3.Distance(currPos, npc.transform.position);
            if (dist<= detectionRange && dist < minDist)
            {
                minDist = dist;
                minNPC = npc;                   
            }
        }
        if (minNPC)
        {
            return minNPC.GetComponent<Citizen>();
        } 
        else
        {
            return null;
        }
        
    }
}
