using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleExplosion : MonoBehaviour
{
    public float explosionTime=2;
    public float inflationTime=1;
    public float minimumScale=0.2f;
    public float maximumScale=2;

    private bool exploding=false;
    public bool Exploding
    {
        get=>exploding;
    }
    private bool inflating=true;
    float timer=-1;

    int hiddenLayer;
    int shownLayer;
    Hideable hideable=null;
    // Start is called before the first frame update
    void Start()
    {
        hiddenLayer=LayerMask.NameToLayer("Hidden");
        shownLayer=LayerMask.NameToLayer("Fired");
        hideable=GetComponent<Hideable>();
        if(hideable!=null)
            hideable.changeLayer(hiddenLayer);
    }
    
    public void explode(Vector3 position)
    {
        transform.position=position;
        exploding=true;
        inflating=true;
        if(hideable!=null)
            hideable.changeLayer(shownLayer);
        timer=Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!exploding)
            return;
        float time=Time.time;
        if(time-timer>=explosionTime)
        {
            if(hideable!=null)
                hideable.changeLayer(hiddenLayer);
            exploding=false;
            return;
        }
        float scale=1;
        if(time-timer>=inflationTime)  inflating=false;
        if(inflating)
        {
            scale=minimumScale+(maximumScale-minimumScale)*(time-timer)/inflationTime;
        } else
        {
            if(Mathf.Approximately(explosionTime,inflationTime))
                scale=minimumScale;
            else
                scale=maximumScale+(minimumScale-maximumScale)*(time-inflationTime-timer)/(explosionTime-inflationTime);
        }
        transform.localScale=new Vector3(scale,scale,scale);
    }
}
