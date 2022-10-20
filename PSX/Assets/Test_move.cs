using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_move : MonoBehaviour
{
    public Vector3 velocity=Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=velocity*Time.deltaTime;
    }
}
