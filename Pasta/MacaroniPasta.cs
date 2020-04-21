using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacaroniPasta : Pastini {
    public int proximity_range = 3;
    private Vector2 stuck_at;
    private GameObject victim;
    private bool primed = false;

    public void Stick (GameObject _victim) {
        //record where we're stuck to
        victim = _victim;
        stuck_at = _victim.transform.InverseTransformPoint(transform.position);
    }

    public void OnCollisionEnter2D (Collision2D collision) {
        if (primed) {
            //explode
        } else {
            //play impact noise
            //Stick (_victim);
        }
    }
}