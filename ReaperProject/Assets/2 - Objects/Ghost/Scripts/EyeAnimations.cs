using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] Rigidbody2D rb;

    private void Update() {
        if(!animator || !rb) return;
        
        animator.SetFloat("VelocityX", rb.velocity.x);
        animator.SetFloat("VelocityY", rb.velocity.y);
    }
}
