using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusiliPasta : Pastini {
    public void Reset () {
        speed = 1f;
        lifetime = 2f;
        cooldown = 0.4f;
        bounces = 0;
        bounce_limit = 1;
    }
    public void Fly () { }
    public void Launch () {
        gameObject.GetComponent<Rigidbody2D> ().AddForce(transform.right * speed, ForceMode2D.Impulse);
    }
    public void Bounce () {
        if (bounces >= bounce_limit) {
            //play death noise
            Destroy (gameObject);
        }
    }

    public void OnCollisionEnter2D (Collision2D collision) {
        //play impact noise
        Bounce ();
    }
}