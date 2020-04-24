using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pastini : ReuseableObject, IShootable {
    protected PastaConfig config;
    protected int bounces = 0;

    void Awake () {
        PastaConfig config = GetComponent<PastaConfig> ();
    }
    void Start () {
        OnReuse ();
    }
    public float Cooldown(){
        return config.cooldown;
    }

    public void Update () {
        this.Fly ();
    }
    //override this with specific pastini re-set requirements if needed
    public void Expire () {
        this.Destroy ();
    }
    protected IEnumerator Age () {
        yield return new WaitForSeconds (config.lifetime);
        if (gameObject.activeSelf) {
            Expire ();
        }
    }
    public override void OnReuse () {
        if (gameObject.activeSelf) {
        StopCoroutine(Age());
            GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            Launch ();
            StartCoroutine (Age());
        }
    }
    public virtual void Fly () {

     }
    public virtual void Launch () {
        Debug.Log ("PASTINI: " + this.GetType ().Name + " is launching");
    }
}