using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideable:MonoBehaviour
{
    public void changeLayer(int layer)
    {
        gameObject.layer=layer;
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.layer=layer;
        }
    }
}
