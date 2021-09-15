using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbokSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;
    //[SerializeField]
    private AudioSource audioSource;
    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        audioSource.clip = audioClips[Random.Range(0,audioClips.Length)];
        audioSource.time = 0f;
        audioSource.Play();
    }
 
    
}
