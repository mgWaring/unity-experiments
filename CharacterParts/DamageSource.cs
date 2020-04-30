using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.GetComponent<Destructable>()){
            Damage(coll.gameObject.GetComponent<Destructable>());
        }
    }

    public virtual void Damage(Destructable target){
        target.TakeDamage(10);
    }

}
