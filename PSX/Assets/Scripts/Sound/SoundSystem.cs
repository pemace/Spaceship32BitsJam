using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    private static SoundSystem instance=null;
    public static SoundSystem Instance
    {
        get=>instance;
    }

    public void playEvent(string name,Vector3 position)
    {
        foreach(SoundEvent e in GetComponentsInChildren<SoundEvent>())
        {
            if(e.eventName==name)
            {
                e.Play(position);
            }
        }
    }

    public static void PlayEvent(string name,Vector3 position)
    {
        if(instance!=null)
            instance.playEvent(name,position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        if(instance!=null)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        instance=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
