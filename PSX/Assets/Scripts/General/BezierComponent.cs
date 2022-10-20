using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierComponent : MonoBehaviour
{
    public bool closed;
    Bezier bezier=new Bezier();

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Awake();
        float delta=1f/50f;
        float t=0;
        for(int i=0;i<50;i++,t+=delta)
            Gizmos.DrawLine(get(t),get(t+delta));
    }


    public Vector3 get(float t)
    {
        return bezier.get(t);
    }

    public Vector3 getDerivative(float t)
    {
        return bezier.getDerivative(t);
    }

    public Vector3 getSecondDerivative(float t)
    {
        return bezier.getSecondDerivative(t);
    }

    void Awake()
    {
        bezier.points.Clear();
        for(int i=0;i<transform.childCount;i++)
        {
            bezier.points.Add(transform.GetChild(i).position);
        }
        if(closed && transform.childCount>0)
        {
            bezier.points.Add(transform.GetChild(0).position);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
