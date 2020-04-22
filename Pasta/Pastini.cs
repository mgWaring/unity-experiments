using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pastini : ReuseableObject, IShootable {
    public float speed;
    public float lifetime;
    public float cooldown;
    public int bounces;
    public int bounce_limit;

    public virtual void Reset () {
        speed = 1f;
        lifetime = 1f;
        cooldown = 1f;
        bounces = 0;
        bounce_limit = 1;
    }
    public void Update () {
        this.Fly ();
    }
    //override this with specific pastini re-set requirements if needed
    public void Expire () {
        this.Destroy ();
    }
    protected IEnumerator Age () {
        yield return new WaitForSeconds (lifetime);
        if (gameObject.activeSelf){
            Expire ();
        }
    }
    public override void OnReuse () {
        if (gameObject.activeSelf) {
            GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
            Launch ();
            StartCoroutine ("Age");
            Debug.Log ("Re-using a pastini");
        }
    }
    public virtual void Fly () { }
    public virtual void Launch () { }
}