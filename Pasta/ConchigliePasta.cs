using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConchigliePasta : Pastini {
    public void Bounce () {
        bounces++;
        if (bounces >= this.config.bounce_limit) {
            //play death noise
            Destroy (gameObject);
        }
    }
    public void OnCollisionEnter2D (Collision2D collision) {
        //play impact noise
        Bounce ();
    }
    public override void Launch () {
        float _speed = this.config.speed;
        GetComponent<Rigidbody2D> ().AddForce (transform.right * _speed, ForceMode2D.Impulse);
    }
}