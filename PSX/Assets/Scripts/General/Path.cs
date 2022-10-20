using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    PathNode rootNode=null;
    public bool closed=true;
    public bool import=true;
    public float roundFactor=1f;
    private PathNode currentNode=null;
    public PathNode CurrentNode
    {
        get=>currentNode;
    }
    void OnDrawGizmosSelected()
    {
        if(rootNode==null)
            return;
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        float delta=1f/10f;
        float t;

        t=0;
        for(int j=0;j<10;j++,t+=delta)
        {
            Gizmos.DrawLine(rootNode.get(t),rootNode.get(t+delta));
        }

        for(PathNode node=rootNode.next;node!=null && node!=rootNode;node=node.next)
        {
            t=0;
            for(int j=0;j<10;j++,t+=delta)
            {
                Gizmos.DrawLine(node.get(t),node.get(t+delta));
            }
        }
            
                
    }

    public void cruise(float dt)
    {
        if(CurrentNode==null)
            return;
        float t=CurrentNode.T;
        t+=dt;
        if(t>1)
        {
            t-=1;
            currentNode=currentNode.next;
        }
        if(currentNode!=null)
            currentNode.T=t;
    }

    public Vector3 get()
    {
        if(currentNode==null)
            return Vector3.zero;
        return currentNode.get();
    }

    public Vector3 getDerivative()
    {
        if(currentNode==null)
            return Vector3.zero;
        return currentNode.getDerivative();
    }

    public Vector3 getSecondDerivative()
    {
        if(currentNode==null)
            return Vector3.zero;
        return currentNode.getSecondDerivative();
    }

    

    // Start is called before the first frame update
    void Awake()
    {
        if(import && transform.childCount>1)
        {
            PathNode node=null;
            int n=transform.childCount-1;
            Vector3 previous;
            if(closed)
                previous=transform.GetChild(n).position;
            else
                previous=transform.GetChild(0).position;

            for(int i=0;i<n;i++)
            {
                Vector3 directionVector1=(transform.GetChild(i).position-previous)*roundFactor;
                Vector3 directionVector2=(transform.GetChild(i+1).position-transform.GetChild(i).position)*roundFactor;
                PathNode nextNode=new PathNode(
                    transform.GetChild(i).position,
                    transform.GetChild(i).position+directionVector1,
                    transform.GetChild(i+1).position-directionVector2,
                    transform.GetChild(i+1).position
                    );
                previous=transform.GetChild(i).position;
                if(node==null)
                {
                    rootNode=nextNode;
                } else
                {
                    node.next=nextNode;
                    nextNode.previous=node;
                }
                node=nextNode;
            }

            if(closed)
            {
                Vector3 directionVector1=(transform.GetChild(n).position-previous)*roundFactor;
                Vector3 directionVector2=(transform.GetChild(0).position-transform.GetChild(n).position)*roundFactor;
                PathNode nextNode=new PathNode(
                    transform.GetChild(n).position,
                    transform.GetChild(n).position+directionVector1,
                    transform.GetChild(0).position-directionVector2,
                    transform.GetChild(0).position
                    );

                node.next=nextNode;
                nextNode.previous=node;
                node=nextNode;
                node.next=rootNode;
            }

            currentNode=rootNode;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
