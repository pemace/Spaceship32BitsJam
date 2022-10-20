using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowStrictly : MonoBehaviour
{
    // Start 
    public GameObject objectToFollow=null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objectToFollow!=null)
        {
            transform.position=objectToFollow.transform.position;
        }
    }
}
