using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    //private Transform playerTransform;

    void Start(){
        audioSource = GetComponent<AudioSource>();

    }
    public void Tak(int hi){
        //if((playerTransform.eulerAngles.z+90f)/90==hi)
        audioSource.clip = audioClips[Random.Range(0,audioClips.Length-1)];
        audioSource.Play();

    }

}
