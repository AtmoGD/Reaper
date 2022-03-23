using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAnimationController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    void Update()
    {
        animator.SetFloat("Speed", rb.velocity.x);
    }
}
