using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ADSR
{
    
    [Range(0.001f,100)] public float attack=0.1f;
    [Range(0.001f,100)] public float decay=1;
    [Range(0.001f,100)] public float sustain=1;
    [Range(0.001f,100)] public float release=0.1f;

    private float timer=-1f;

    private float previousValue=1;

    public bool Trigger
    {
        set=>setTrigger(value);
        get=>trigger;
    }
    bool trigger=false;
    bool setTrigger(bool trig)
    {
        trigger=trig;
        previousValue=floatValue;
        timer=Time.time;
        if(trigger)
        {
            status=ADSRstatus.ATTACK;
        } else
        {
            status=ADSRstatus.RELEASE;
        }
        return trig;
    }

    public float FloatValue
    {
        get=>floatValue;
    }

    private float floatValue=0f;
    private enum ADSRstatus
    {
        IDLE=0,
        ATTACK,
        DECAY,
        SUSTAIN,
        RELEASE
    }
    private ADSRstatus status=ADSRstatus.IDLE;

    private void _attack(float timing)
    {
        if(timing>=attack)
        {
            floatValue=1;
            status=ADSRstatus.DECAY;
            timer=Time.time;
            return;
        }
        floatValue=timing/attack*(1-previousValue)+previousValue;
    }

    private void _decay(float timing)
    {
        if(timing>=decay)
        {
            floatValue=sustain;
            status=ADSRstatus.SUSTAIN;
            return;
        }
        floatValue=1+(sustain-1)*timing/decay;
    }
    private void _sustain(float timing)
    {
    }
    private void _release(float timing)
    {
        if(timing>=release)
        {
            floatValue=0;
            status=ADSRstatus.IDLE;
            return;
        }
        floatValue=previousValue*(1-timing/release);
    }

    public void update()
    {
        float timing=Time.time-timer;
        switch(status)
        {
            case ADSRstatus.IDLE:
                floatValue=0f;
                break;
            case ADSRstatus.ATTACK:
                _attack(timing);
                break;
            case ADSRstatus.DECAY:
                _decay(timing);
                break;
            case ADSRstatus.SUSTAIN:
                _sustain(timing);
                break;
            case ADSRstatus.RELEASE:
                _release(timing);
                break;
        }
    }
}