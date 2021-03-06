﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusiliPasta : Pastini {
    public override void Launch () {
        StartCoroutine (Rotate ());
        gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.right * config.speed, ForceMode2D.Impulse);
    }
    public override void Fly () { }
    protected IEnumerator Rotate () {
        while (gameObject.activeSelf) {
            Vector3 prev = transform.position;
            yield return new WaitForSeconds (0.04f);
            if (gameObject.activeSelf) {
                Vector3 dir = transform.position - prev;
                float _angle = Vector2.SignedAngle (Vector2.right, dir.normalized);
                transform.rotation = Quaternion.Euler (0, 0, _angle);
            }
        }
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