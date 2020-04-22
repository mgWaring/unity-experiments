using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarfallePasta : Pastini {
    public float lerp = 0.1f;
    public float lerp_growth = 1.5f;
    public void Reset () {
        speed = 0.5f;
        lifetime = 5f;
        cooldown = 0.6f;
        bounces = 0;
        bounce_limit = 3;
    }
    public override void Launch () {
        gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.right * speed, ForceMode2D.Impulse);
    }
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
    public void Fly () {
        transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0, 0, 90), lerp * lerp_growth * Time.deltaTime);
        transform.position += (transform.right + new Vector3 (speed, 0, 0)) * Time.deltaTime;
    }
}