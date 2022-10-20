using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{
    public int poolCount=100;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource source=GetComponentInChildren<AudioSource>();
            if(source!=null)
                for(int i=0;i<poolCount;i++)
                    Instantiate<GameObject>(source.gameObject,transform);
    }

    public void Play()
    {
        foreach(AudioSource source in GetComponentsInChildren<AudioSource>())
        {
            if(!source.isPlaying)
            {
                source.PlayOneShot(source.clip);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
