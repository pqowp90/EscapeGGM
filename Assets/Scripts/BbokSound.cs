using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbokSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;
    //[SerializeField]
    [SerializeField]
    private AudioSource[] audioSource;
    private Animator animator;
    void Awake(){
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        animator.SetFloat("Blend",Random.Range(0,3));
        audioSource[0].clip = audioClips[Random.Range(0,audioClips.Length)];
        audioSource[0].time = 0f;
        audioSource[0].Play();
        audioSource[1].Play();
    }
 
    
}
