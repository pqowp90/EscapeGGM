using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbokSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;
    //[SerializeField]
    private AudioSource audioSource;
    private Animator animator;
    void Awake(){
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        animator.SetFloat("Blend",Random.Range(0,3));
        audioSource.clip = audioClips[Random.Range(0,audioClips.Length)];
        audioSource.time = 0f;
        audioSource.Play();
    }
 
    
}
