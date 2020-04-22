using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacaroniPasta : Pastini {
    public int proximity_range = 3;
    public LayerMask potential_victims;
    private Vector2 stuck_at;
    private GameObject victim;
    private bool primed = false;
    private bool stuck = false;
    public float fuse_time = 1f;
    public override void Reset () {
        speed = 0f;
        lifetime = 10f;
        cooldown = 0.2f;
        bounces = 0;
        bounce_limit = 2;
    }
    public override void Launch () {
        gameObject.GetComponent<Rigidbody2D> ().AddForce (transform.right * speed, ForceMode2D.Impulse);
    }

    void OnEnable () {
        StartCoroutine ("Fuse");
    }
    private IEnumerator Fuse () {
        yield return new WaitForSeconds (fuse_time);
        primed = true;
    }

    public void Stick (GameObject _victim) {
        //record where we're stuck to
        victim = _victim;
        stuck_at = _victim.transform.InverseTransformPoint (transform.position);
        stuck = true;
    }
    public override void Fly () {
        if (primed) {
            Primed ();
        }
    }
    void Primed () {
        transform.position = victim.transform.TransformPoint (stuck_at);
        if (Physics2D.OverlapCircle (transform.position, proximity_range, potential_victims)) {
            Explode ();
        }
    }

    void Explode () {
        //calculate damage
        //play noise
        Expire ();
    }

    public void OnCollisionEnter2D (Collision2D collision) {
        if (primed) {
            Explode ();
        } else {
            //play impact noise
            Stick (collision.gameObject);
        }
    }
}