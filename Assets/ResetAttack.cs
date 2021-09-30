using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttack : MonoBehaviour
{
    private Animator myAnimator;
    void Awake()
    {
        myAnimator=GetComponent<Animator>();
    }
    public void ResetAttackAni(){
        myAnimator.ResetTrigger("atk");
    }
}
