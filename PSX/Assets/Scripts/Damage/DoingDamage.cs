using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoingDamage : MonoBehaviour
{
    public GameObject owner=null;
    public bool active=true;
    public float power=50;
    public System.Action<Vector3> hitAction=null;
    void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.name);
        if(!active)
            return;
        GameObject obj=collider.gameObject;
        if(obj==owner)
            return;
        Damageable damageable=obj.GetComponent<Damageable>();
        if(damageable!=null)
        {
            Vector3 position=transform.position*0.8f+collider.bounds.center*0.2f;
            damageable.doDamage(power,position);
            damageable.hitCollision(position);
            if(hitAction!=null)
                hitAction(position);
        }
    }
}
