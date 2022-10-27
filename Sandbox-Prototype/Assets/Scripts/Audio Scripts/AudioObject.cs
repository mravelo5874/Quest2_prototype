using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour
{
    [HideInInspector] public string id;
    private AudioSource audioSource;
    private AudioClip handle;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayClip(string id, AudioClip clip, float volume, bool loop, float pitch)
    {
        // set clip and volume
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        // play clip and destroy object once complete OR loop forever
        audioSource.Play();

        if (loop)
        {
            audioSource.loop = true;
        }
        else
        {
            StartCoroutine(DelayDestruction(clip.length + 1f));
        }
    }

    public void InstaDestroy()
    {
        StopAllCoroutines();
        if (gameObject != null)
            Destroy(gameObject);
    }

    private IEnumerator DelayDestruction(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
