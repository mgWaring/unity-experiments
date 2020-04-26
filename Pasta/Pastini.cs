using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pastini : ReuseableObject, IShootable {
    protected PastaConfig config;
    protected int bounces = 0;

    void Awake () {
        config = GetComponent<PastaConfig> ();
    }
    void Start () {
        OnReuse ();
    }
    void OnDisable () {
        StopCoroutine (Age ());
    }
    public float Cooldown () {
        config = GetComponent<PastaConfig> ();
        return config.cooldown;
    }
    public void Update () {
        this.Fly ();
    }
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
            GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
            Launch ();
            StartCoroutine (Age ());
        }
    }
    public virtual void Fly () { }
    public virtual void Launch () { }
}