using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollower : MonoBehaviour
{
    public BezierComponent bezier=null;
    private Spaceship ship=null;
    float radius=1;

    public Vector3  axes=new Vector3(1,1,1);
    float t=0;
    float minimumThrottle=0.3f;
    float maximumThrottle=0.7f;
    // Start is called before the first frame update
    void Start()
    {
        ship=GetComponent<Spaceship>();
        if(bezier!=null)
        {
            transform.position=bezier.get(0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ship==null || bezier==null)
            return;
        if(t>1)
            t-=1;
        else if(t<0)
            t=0;
        Vector3 position=bezier.get(t);

        Vector3 derivative=bezier.getDerivative(t);


        float derivativeMagnitude=derivative.magnitude;
        if(Mathf.Approximately(derivativeMagnitude,0))
            return;
        Vector3 derivativeNormal=derivative.normalized;

        Vector3 relative=position-transform.position;
        float relativeDistance=relative.magnitude;
        float choice=1;
        if(relativeDistance>radius)
            choice=1/relativeDistance/relativeDistance;
        
        Vector4 delta1=new Vector4(
            axes.x*Vector3.SignedAngle(transform.forward,derivativeNormal,transform.right),
            axes.y*Vector3.SignedAngle(transform.forward,derivativeNormal,transform.up),
            axes.z*Vector3.SignedAngle(derivativeNormal,transform.up,transform.forward),
            Vector3.Dot(transform.forward,derivativeNormal)
        );
        for(int i=0;i<3;i++)
        {
            float plus360=delta1[i]+360;
            float minus360=delta1[i]-360;
            float equal=delta1[i];

            float absplus360=Mathf.Abs(plus360);
            float absminus360=Mathf.Abs(minus360);
            float absEqual=Mathf.Abs(equal);

            if(absplus360<absEqual && absplus360<absminus360)
                delta1[i]=plus360;
            else if(absminus360<absplus360 && absminus360<absEqual)
                delta1[i]=minus360;
        }

        Vector4 delta2=new Vector4(
            axes.x*Vector3.SignedAngle(transform.forward,relative,transform.right),
            axes.y*Vector3.SignedAngle(transform.forward,relative,transform.up),
            axes.z*Vector3.SignedAngle(relative,transform.up,transform.forward),
            Vector3.Dot(transform.forward,relative)
        );

        Vector4 delta=choice*delta1+(1-choice)*delta2;
        delta.w=Mathf.Clamp(delta.w,minimumThrottle,maximumThrottle);

        Vector4 effectivePower=ship.effectivePower(
            new Vector4(1,1,1,delta.w)
        );

        for(int i=0;i<3;i++)
        {
            if(Mathf.Approximately(effectivePower[i],0))
                delta[i]=0;
            else
                delta[i]/=effectivePower[i];
        }

        float ratio=Mathf.Max(Mathf.Abs(delta.x),Mathf.Abs(delta.y),Mathf.Abs(delta.z),Mathf.Abs(delta.w));

        

        float tSpeed=effectivePower.w/derivativeMagnitude*choice;

        

        if(ratio>=-1 && ratio<=1)
        {
            ship.shipCommand=delta;
            t+=tSpeed;
        }
        else
        {
            delta/=ratio;
            delta.w=Mathf.Clamp(delta.w,minimumThrottle,maximumThrottle);
            ship.shipCommand=delta;
            t+=tSpeed;
        }

        print(ship.shipCommand);
    }
}
