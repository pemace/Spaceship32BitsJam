using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    private bool isPool=false;
    private AudioSource source=null;
    private SoundPool pool=null;
    public string eventName="dummy";
    // Start is called before the first frame update
    void Start()
    {
        source=GetComponent<AudioSource>();
        if(source==null)
        {
            pool=GetComponent<SoundPool>();
            if(pool!=null)
                isPool=true;
        }
    }

    public void Play(Vector3 position)
    {
        print("--------------------");
        if(isPool)
        {
            pool.Play();
        } else if(source!=null)
        {
            source.PlayOneShot(source.clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
