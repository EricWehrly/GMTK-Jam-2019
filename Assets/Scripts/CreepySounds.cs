using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepySounds : MonoBehaviour
{
    public AudioClip[] AudioClips;
    public int SecondsInterval = 5;

    private AudioSource audioSource;
    private DateTime lastPlay;
    private System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        lastPlay = DateTime.Now;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DateTime currentTime = DateTime.Now;

        if(currentTime.Subtract(lastPlay).Seconds > SecondsInterval)
        {
            int clipIndex = random.Next(0, AudioClips.Length);

            lastPlay = currentTime;
            audioSource.clip = AudioClips[clipIndex];
            audioSource.Play();
        }
    }
}
