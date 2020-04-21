using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pastini : ReuseableObject, IShootable {
    public float speed = 1f;
    public float lifetime = 1f;
    public float cooldown = 1f;
    public int bounces = 0;
    public int bounce_limit = 1;
    public void Update () {
        Fly ();
    }
    //override this with specific pastini re-set requirements if needed
    public void Expire () {
        Destroy ();
    }
    protected IEnumerator Age () {
        yield return new WaitForSeconds (lifetime);
        Expire ();
    }
    public override void OnReuse () {
        Launch ();
        StartCoroutine ("Age");
        //Debug.Log ("Re-using a pastini");
    }
    public virtual void Fly (){}
    public virtual void Launch (){}
}