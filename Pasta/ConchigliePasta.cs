using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConchigliePasta : Pastini {
    public void Bounce () {
        if (bounces < bounce_limit) {
            //assess incoming angle and reflect
        } else {
            //play death noise
            Destroy (gameObject);
        }
    }
    public void OnCollisionEnter2D (Collision2D collision) {
        //play impact noise
        Bounce ();
    }
}