using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarfallePasta : Pastini {
    public float lerp = 0.1f;
    public float lerp_growth = 1.5f;

    public override void Launch () {
        gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.right * config.speed, ForceMode2D.Impulse);
    }
    public void Bounce () {
        bounces++;
        if (bounces < config.bounce_limit) {
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