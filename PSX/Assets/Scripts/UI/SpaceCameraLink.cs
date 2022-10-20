using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCameraLink : MonoBehaviour
{
    public Camera renderCamera=null;
    public float distanceCorrection=0.01f;
    Vector3 rcOrigin;
    Vector3 spaceOrigin;
    // Start is called before the first frame update
    void Start()
    {
        spaceOrigin=transform.position;
        if(renderCamera==null)
        {
            print(gameObject.name+": no camera attached to space environment");
            return;
        }
        rcOrigin=renderCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(renderCamera==null)
            return;
        transform.rotation=renderCamera.transform.rotation;
        transform.position=spaceOrigin+(renderCamera.transform.position-rcOrigin)*distanceCorrection;
    }
}
