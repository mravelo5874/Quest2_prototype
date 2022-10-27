using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioDatabase database;

    public GameObject audioObject;
    public Transform audioObjectHolder;

    private float current_volume = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip clip, float volume, bool loop = false, float pitch = 1f, string id = "default_id")
    {
        var audioObj = Instantiate(audioObject, audioObjectHolder);
        audioObj.GetComponent<AudioObject>().PlayClip(id, clip, volume * current_volume, loop, pitch);
    }

    public bool StopSound(string id)
    {
        // iterate though each FX audio object
        int num = audioObjectHolder.childCount;
        for (int idx = 0; idx < num; idx++)
        {
            // if the id matches - destroy object
            var obj = audioObjectHolder.GetChild(idx).GetComponent<AudioObject>();
            if (obj.id == id)
            {
                obj.InstaDestroy();
                return true;
            }
        }
        return false;
    }

    // clear all fx
    public void ClearAllAudio()
    {
        // iterate though each FX audio object and destory each one
        int num = audioObjectHolder.childCount;
        for (int idx = num - 1; idx >= 0; idx--)
        {
            var obj = audioObjectHolder.GetChild(idx).GetComponent<AudioObject>();
            obj.InstaDestroy();
        }
    }

    public void UpdateVolume(float level)
    {
        // TODO do this
    }
}
