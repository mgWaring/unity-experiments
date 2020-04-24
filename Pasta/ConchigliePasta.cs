using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConchigliePasta : Pastini {
    public void Bounce () {
        Debug.Log ("Conchiglie " + GetInstanceID () + " says BOING(" + bounces + ")!");
        bounces++;

        if (bounces >= config.bounce_limit) {
            //play death noise
            Destroy (gameObject);
        }
    }
    public void OnCollisionEnter2D (Collision2D collision) {
        //play impact noise
        Bounce ();
    }
    public override void Launch () {
        Debug.Log ("" + this.GetType ().Name + " is launching");
        gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.right * config.speed, ForceMode2D.Impulse);
    }
}