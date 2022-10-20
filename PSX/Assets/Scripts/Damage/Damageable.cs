using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float lifePoints=100;
    public System.Action destroyAction=null;
    public System.Action<float,Vector3> hitAction=null;
    public System.Action<Vector3> hitCollisionAction=null;
    public bool active=true;
    public void doDamage(float points,Vector3 position)
    {
        if(!active)
            return;
        lifePoints=Mathf.Max(lifePoints-points,0);
        if(hitAction!=null)
            hitAction(lifePoints,position);
        if(Mathf.Approximately(lifePoints,0) && destroyAction!=null)
            destroyAction();
    }
    public void hitCollision(Vector3 position)
    {
        if(hitCollisionAction!=null)
            hitCollisionAction(position);
    }
}
