using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rigidBody;
    

    private Gun gun=null;
    private Spaceship ship=null;
    public float cruiseThrottle=0.6f;

    public Vector3 estimateSight(float distance)
    {
        if(gun==null)
            return transform.position+transform.forward*distance;
        
        return gun.estimateSight(distance);
    }

    private float activeRoll, activePitch, activeYaw;

    void Start()
    {
        gun=GetComponentInChildren<Gun>();
        ship=GetComponent<Spaceship>();
        if(ship==null)
            print(gameObject.name+": no starship attached to player");
        if(gun==null)
            print(gameObject.name+": no gun attached to player");
    }

    private void FixedUpdate()
    {
        if(gun!=null && Input.GetButton("Fire1"))
            gun.fire();
        if(ship!=null)
        {
            float throttle=cruiseThrottle;
            /*if(Input.GetButton("R1"))
                throttle=ship.power[1].w;
            else if(Input.GetButton("L1"))
                throttle=ship.power[0].w;
                */
            float yaw=0;
            if(Input.GetButton("R1"))
                yaw+=1;
            if(Input.GetButton("L1"))
                yaw-=1;
            ship.shipCommand=new Vector4(Input.GetAxisRaw("Vertical"),yaw,-Input.GetAxisRaw("Horizontal"),throttle);
        }
            
    }
}
