using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : Gun
{
    public float fireInterval=0.2f;
    float timer=-1;

    public string soundEventName="";
    
    // Start is called before the first frame update
    override public void fire()
    {
        float currentTime=Time.time;
        if(bulletPool==null || owner==null ||
            (timer>0 && currentTime-timer<fireInterval))
            return;
        timer=currentTime;

        SoundSystem.PlayEvent(soundEventName,transform.position);
    
        Bullet []bullets=bulletPool.GetComponentsInChildren<Bullet>();
        foreach(Bullet bullet in bullets)
        {
            if(!bullet.Firing)
            {
                bullet.fire(owner,transform);
                break;
            }
        }
    }

    // Update is called once per frame
}
