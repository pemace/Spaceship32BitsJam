using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public Vector4 shipCommand=Vector4.zero;
    public Vector4  [] power={Vector4.one,Vector4.one};

    private Vector4 derivative;
    public Vector4 Derivative
    {
        get=>derivative;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public Vector4 effectivePower(Vector4 shipCommand)
    {
        Vector4 derivative=Vector4.zero;
        Vector4 effectivePower;

        for(int i=0;i<3;i++)
            shipCommand[i]=Mathf.Clamp(shipCommand[i],-1,1);
        shipCommand.w=Mathf.Clamp(shipCommand.w,0,1);
        float throttleVelocity=shipCommand.w*power[1].w;
        
        if(Mathf.Approximately(power[1].w,power[0].w))
            effectivePower=power[0];

        else
            effectivePower=power[0]+(throttleVelocity-power[0].w)*(power[1]-power[0])/(power[1].w-power[0].w);

        derivative.x = shipCommand.x * effectivePower.x;
        derivative.y = shipCommand.y * effectivePower.y;
        derivative.z = shipCommand.z * effectivePower.z;
        derivative.w=effectivePower.w;

        return derivative;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(power.Length!=2)
            return;
        
        derivative=effectivePower(shipCommand);

        

        Vector4 deltaPower=derivative*Time.fixedDeltaTime;

        print("test---="+derivative+" "+deltaPower);
        

        transform.Rotate(deltaPower.x,deltaPower.y,deltaPower.z,Space.Self);
        transform.position += transform.forward * deltaPower.w;
    }
}
