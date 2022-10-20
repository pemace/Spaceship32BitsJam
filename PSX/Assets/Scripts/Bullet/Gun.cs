using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Spaceship owner;
    public GameObject bulletPool=null;

    public float BulletVelocity{
        get=>bulletVelocity;
    }

    private float bulletVelocity=1000000;
    // Start is called before the first frame update

    public Vector3 estimateSight(float distance)
    {
        if(owner==null || true) //--------------------------------
            return transform.position+transform.forward*distance;
        Vector4 delta=-owner.Derivative*distance/BulletVelocity;
        Quaternion angularDelta=Quaternion.Euler(delta.x,delta.y,delta.z);

        return owner.transform.position+
            owner.transform.forward*delta.w+
            angularDelta*owner.transform.forward*distance;
    }

    virtual public void GunStart()
    {
    }

    void Start()
    {
        if(bulletPool!=null)
        {
            Bullet bullet=bulletPool.GetComponentInChildren<Bullet>();
            if(bullet!=null)
            {
                bulletVelocity=bullet.velocity;
            }
        }
        GunStart();
    }

    public virtual void fire()
    {
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }

    // Update is called once per frame
}
