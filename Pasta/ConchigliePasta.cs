using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConchigliePasta : Pastini {
    public void Reset () {
        speed = 0.5f;
        lifetime = 6f;
        cooldown = 0.5f;
        bounces = 0;
        bounce_limit = 5;
    }
    public void Bounce () {
        Debug.Log ("Conchiglie " + GetInstanceID () + " says BOING(" + bounces + ")!");
        bounces++;

        if (bounces < bounce_limit) { } else {
            //play death noise
            Destroy (gameObject);
        }
    }
    public void OnCollisionEnter2D (Collision2D collision) {
        //play impact noise
        Bounce ();
    }
    public override void Launch () {
        gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.right * speed, ForceMode2D.Impulse);
    }
}