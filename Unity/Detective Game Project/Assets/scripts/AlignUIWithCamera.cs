using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignUIWithCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Camera.main.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
