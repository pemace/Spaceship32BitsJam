using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGun : Gun
{
    SimpleGun []guns;
    int gunIndex=0;

    float timer=-1;

    public override void GunStart()
    {
        if(owner==null)
            owner=transform.parent.gameObject.GetComponent<Spaceship>();
        guns=GetComponentsInChildren<SimpleGun>();
        foreach(Gun gun in guns)
        {
            gun.owner=this.owner;
        }
    }

    override public void fire()
    {
        if(guns.Length==0)
            return;
        float currentTime=Time.time;
        if(timer>0 && currentTime-timer<guns[gunIndex].fireInterval/2)
            return;
        timer=currentTime;
        guns[gunIndex].fire();
        gunIndex=(gunIndex+1)%guns.Length;
    }
}
