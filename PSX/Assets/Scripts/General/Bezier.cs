using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    const float epsillon=1e-7f;
    public List<Vector3> points=new List<Vector3>();
    public static float Bernstein(int n,int i,float t)
    {
        if(i<0||i>n)
            return 0;
        if(n==0)
            return 1;
        if(i==0)
            return Mathf.Pow(1-t,n);
        if(i==n)
            return Mathf.Pow(t,n);
        return Binomial(n,i)*Mathf.Pow(t,i)*Mathf.Pow(1-t,n-i);
    }

    public static float BernsteinDerivative(int n,int i,float t)
    {
        if(n<=0||i<0||i>n)
            return 0;
        if(n==1&&i==0)
            return 1;
        if(n==1&&i==1)
            return -1;
        if(i==0)
            return -n*Mathf.Pow(1-t,n-1);
        if(i==n)
            return n*Mathf.Pow(t,n-1);

        float first=i*Mathf.Pow(1-t,n-i)*Mathf.Pow(t,i-1);
        float last=-(n-i)*Mathf.Pow(1-t,n-i-1)*Mathf.Pow(t,i);

        return Binomial(n,i)*(first+last);
    }

    public static float BernsteinSecondDerivative(int n,int i,float t)
    {
        if(n<2||i<0||i>n)
            return 0;
        if(i==0)
            return n*(n-1)*(n-2)*Mathf.Pow(1-t,n-2);
        if(i==n)
            return n*(n-1)*Mathf.Pow(t,n-2);

        float a=i*(i-1)*Mathf.Pow(t,i-2)*Mathf.Pow(1-t,n-i);
        float b=2*i*(n-i)*Mathf.Pow(t,i-1)*Mathf.Pow(1-t,n-i-1);
        float c=(n-i)*(n-i-1)*Mathf.Pow(t,i)*Mathf.Pow(1-t,n-i-2);

        return Binomial(n,i)*(a+b+c);
    }

    public static int Binomial(int n,int i)
    {
        if(i<0 || i>n)
            return 0;
        if(n<2||i==n||i==0)
            return 1;
        return Binomial(n-1,i-1)+Binomial(n-1,i);
    }

    public Vector3 get(float t)
    {
        Vector3 point=Vector3.zero;
        int n=points.Count-1;
        for(int i=0;i<=n;i++)
        {
            point+=Bernstein(n,i,t)*points[i];
        }
        return point;
    }

    public Vector3 getDerivative(float t)
    {
        Vector3 point=Vector3.zero;
        int n=points.Count-1;
        for(int i=0;i<=n;i++)
        {
            point+=BernsteinDerivative(n,i,t)*points[i];
        }
        return point;
    }

    public Vector3 getSecondDerivative(float t)
    {
        Vector3 point=Vector3.zero;
        int n=points.Count-1;
        for(int i=0;i<=n;i++)
        {
            point+=BernsteinSecondDerivative(n,i,t)*points[i];
        }
        return point;
    }

    public bool curvature(float t,out float curve)
    {
        curve=0;
        Vector3 derivative=getDerivative(t);
        
        float denominator=Mathf.Pow(derivative.magnitude,3);
        if(denominator==0)
            return false;

        Vector3 secondDerivative=getSecondDerivative(t);

        Vector3 numeratorVector=new Vector3(
                                    secondDerivative.z*derivative.y-secondDerivative.y*derivative.z,
                                    secondDerivative.x*derivative.z-secondDerivative.z*derivative.x,
                                    secondDerivative.y*derivative.x-secondDerivative.x*derivative.y);
        float numerator=numeratorVector.magnitude;

        curve=numerator/denominator;

        return true;
    }
}
