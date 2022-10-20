using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableCube : MonoBehaviour
{
    Damageable damageable=null;
    public ExplosionPool explosionPool=null;
    public ExplosionPool hitPool=null;
    void hit(float points,Vector3 position)
    {
        print("Cube hit "+position);
        if(hitPool!=null)
            hitPool.explode(position);
    }

    void destroy()
    {
        SoundSystem.PlayEvent("EXPLOSION",transform.position);
        print("cube explodes");
       if(explosionPool!=null)
            explosionPool.explode(transform.position);
        GameObject.Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        damageable=GetComponent<Damageable>();
        if(damageable!=null)
        {
            damageable.hitAction=hit;
            damageable.destroyAction=destroy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
