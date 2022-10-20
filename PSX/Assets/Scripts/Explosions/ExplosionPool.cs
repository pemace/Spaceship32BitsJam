using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public int poolCount=100;
    // Start is called before the first frame update
    void Start()
    {
        SimpleExplosion explosion=GetComponentInChildren<SimpleExplosion>();
            if(explosion!=null)
                for(int i=0;i<poolCount;i++)
                    Instantiate<GameObject>(explosion.gameObject,transform);
    }

    public void explode(Vector3 position)
    {
        foreach(SimpleExplosion explosion in GetComponentsInChildren<SimpleExplosion>())
        {
            if(!explosion.Exploding)
            {
                explosion.explode(position);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
