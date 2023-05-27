using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public Sound [] sounds; 
    public static AudioManager Instance { get; private set; }
    [SerializeField] List<Sprite> imageList;
    [SerializeField] Button voiceButton;
    private int imageId;
    bool muteOn;
    
    private void Start()
    {
        imageId= 0;
        muteOn = false;
        
    }
    private void Awake()
    {
        Instance = this;
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume= sound.volume;
            sound.source.loop= sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        if (!s.source.isPlaying)
            s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        s.source.Stop();

    }
    public void ChangeVolumeImage()
    {
        
        imageId++;
        if (imageId == imageList.Count)
        {
            imageId = 0;
        }
        if(imageId== 0)
            muteOn = false;
        else
            muteOn= true;
        ChangeVolumeOfSong("Theme");
        voiceButton.GetComponent<Image>().sprite = imageList[imageId];
    }
    public void ChangeVolumeOfSong(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        if (muteOn||!muteOn)
            s.source.mute = muteOn;

    }

}
