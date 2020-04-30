using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {
    public int hp = 100;
    public GameObject death_sprite;

    void Start () {
        ObjectPoolManager.instance.CreatePool (death_sprite, 1);
    }
    public void TakeDamage (int damage) {
        Debug.Log ("Ouch, I took " + damage + " damage");
        hp = hp - damage;
        if (hp <= 0) {
            //play death noise
            //show death sprite
            ObjectPoolManager.instance.ReuseObject (death_sprite.GetInstanceID (), transform.position, transform.rotation);
            gameObject.SetActive (false);
        }
    }
}