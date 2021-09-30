using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    public void Tak(){
        audioSource.clip = audioClips[Random.Range(0,audioClips.Length-1)];
        audioSource.Play();
    }
}
