using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private Animation animation;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimations(string _animationName)
    {
       
        anim.Play(_animationName); 
    }
}