using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeSpan=5;
    public float velocity=30;
    public bool Firing
    {
        get=>firing;
    }
    bool firing;
    GameObject owner;
    float timer=0f;
    int hiddenLayer=0;
    int shownLayer=0;
    DoingDamage doingDamage=null;

    Rigidbody body;
    Collider col;
    Hideable hideable=null;
    // Start is called before the first frame update
    void Start()
    {
        body=GetComponent<Rigidbody>();
        hiddenLayer=LayerMask.NameToLayer("Hidden");
        shownLayer=LayerMask.NameToLayer("Fired");
        hideable=GetComponent<Hideable>();
        if(hideable!=null)
            hideable.changeLayer(hiddenLayer);
        col=GetComponent<Collider>();
        if(doingDamage!=null)
        {
            doingDamage.active=false;
            doingDamage.hitAction=hit;
        }
        if(body!=null)
            body.detectCollisions=false;
        doingDamage=GetComponent<DoingDamage>();
    }

    public void fire(Spaceship owner,Transform gunTransform)
    {
        if(owner==null || body==null)
            return;
            
        timer=Time.time;
        if(hideable!=null)
            hideable.changeLayer(shownLayer);
        transform.rotation=gunTransform.rotation;
        transform.position=gunTransform.position;
        firing=true;
        if(doingDamage!=null)
            doingDamage.active=true;
        if(body!=null)
            body.detectCollisions=true;
    }

    void hit(Vector3 position)
    {
        destroy();
    }

    void destroy()
    {
        if(hideable!=null)
            hideable.changeLayer(hiddenLayer);
        firing=false;
        if(doingDamage!=null)
            doingDamage.active=false;
            
        if(body!=null)
            body.detectCollisions=false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!firing)
            return;
        if(Time.time-timer>=lifeSpan)
        {
            destroy();
            return;
        }
        transform.position+=transform.forward*velocity*Time.fixedDeltaTime;
    }
}
