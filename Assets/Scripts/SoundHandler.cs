using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public Sound[] soundFx;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Sound s in soundFx){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name){
        Sound s = Array.Find(soundFx, sound => sound.name == name);
        if(s == null){
            Debug.Log("No Such Sound Found");
            return;
        }
        s.source.Play();
    }
    public void PlaySound(string name, float p){
        Sound s = Array.Find(soundFx, sound => sound.name == name);
        if(s == null){
            Debug.Log("No Such Sound Found");
            return;
        }
        s.source.pitch = p;
        s.source.Play();
    }
    public void PlaySound(int x){
        var s = soundFx[x];
        s.source.Play();
    }
    public void PlaySound(int x, float p){
        var s = soundFx[x];
        s.pitch = p;
        s.source.Play();
    }

    public void StopSound(string name){
        Sound s = Array.Find(soundFx, sound => sound.name == name);
        if(s == null){
            Debug.Log("No Such Sound Found");
            return;
        }
        s.source.Stop();
    }
    public void StopSound(int x){
        var s = soundFx[x];
        s.source.Stop();
    }
}
