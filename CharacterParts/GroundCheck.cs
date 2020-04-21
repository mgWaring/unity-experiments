using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public bool grounded = true;
    private bool was = false;
    public LayerMask ground;
    //these are secret\\
    public float groundRadius = 0.2f;

    void Update () {
        was = grounded;
        grounded = Physics2D.OverlapCircle (transform.position, groundRadius, ground);
    }    

    public bool WasGrounded () {
        return was;
    }

    public bool IsGrounded () {
        return grounded;
    }
}