using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public static Animator animator;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    public static void Shake()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("CameraShake");
            animator.Play("CameraShakeToo");
        }
    }
}
