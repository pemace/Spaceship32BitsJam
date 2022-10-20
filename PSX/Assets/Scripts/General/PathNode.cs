using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class PathNode
{
    Bezier bezier=new Bezier();
    //test
    public PathNode previous=null;
    public PathNode next=null;
    float t=0;
    public float T
    {
        get=>t;
        set=>setT(value);
    }
    public float setT(float t)
    {
        return this.t=Mathf.Clamp(t,0,1);
    }

    public Vector3 get()
    {
        return bezier.get(t);
    }

    public Vector3 getDerivative()
    {
        return bezier.getDerivative(t);
    }

    public Vector3 getSecondDerivative()
    {
        return bezier.getSecondDerivative(t);
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

    public PathNode addNodeReplace(params Vector3 [] points)
    {
        PathNode newNode=new PathNode(points);
        next=newNode;
        newNode.previous=this;
        return newNode;
    }

    public PathNode addNode(params Vector3 [] points)
    {
        PathNode newNode=new PathNode(points);
        PathNode node=this;
        for(;node.next!=null;node=node.next);
        node.next=newNode;
        newNode.previous=node;
        return newNode;
    }

    public PathNode insertAfter(params Vector3 [] points)
    {
        PathNode newNode=new PathNode(points);
        PathNode nextNode=this.next;
        newNode.next=nextNode;
        newNode.previous=this;
        if(nextNode!=null)
            nextNode.previous=newNode;
        next=newNode;
        return newNode;
    }

    public PathNode(params Vector3 [] points)
    {
        foreach(Vector3 point in points)
            bezier.points.Add(point);
    }
}
