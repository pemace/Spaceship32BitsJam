using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow=null;
    public float rotationSpeed=10;
    public Vector3 pursuitVector=new Vector3(0,1,-3);
    // Start is called before the first frame update
    void Start()
    {
        if(objectToFollow==null)
        {
            print(gameObject.name+": The camera needs an object to follow");
            return;
        }
        
        transform.rotation=objectToFollow.transform.rotation;
        transform.position=objectToFollow.transform.position+transform.rotation*pursuitVector;
    }

    // Update is called once per frame
    void Update()
    {
        if(objectToFollow==null)
            return;

        transform.rotation=Quaternion.Slerp(
            transform.rotation,
            objectToFollow.transform.rotation,rotationSpeed*Time.deltaTime);
        transform.position=objectToFollow.transform.position+transform.rotation*pursuitVector;
    }
}
