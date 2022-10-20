using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public int poolCount=100;
    // Start is called before the first frame update
    void Start()
    {
        Bullet bullet=GetComponentInChildren<Bullet>();
            if(bullet!=null)
                for(int i=0;i<poolCount;i++)
                    Instantiate<GameObject>(bullet.gameObject,transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
