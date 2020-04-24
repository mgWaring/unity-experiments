using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusiliPasta : Pastini {
    public override void Launch () {
        Debug.Log ("" + this.GetType ().Name + " is launching");
        gameObject.GetComponent<Rigidbody2D> ().AddForce(transform.right * config.speed, ForceMode2D.Impulse);
    }
    public void Bounce () {
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
}