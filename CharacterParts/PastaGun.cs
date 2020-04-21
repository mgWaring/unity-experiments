using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastaGun : MonoBehaviour {
    public float cooldown = 0.5f;
    public float angle = 0.0f;
    private bool gun_ready = true;

    public void AimTo (Vector3 target, float _angle) {
        angle = _angle;
        transform.position = target;
    }

    public void Shoot (int shot_id) {
        if (gun_ready) {
            //Debug.Log (angle);
            //use object pool
            ObjectPoolManager.instance.ReuseObject (shot_id, transform.position, Quaternion.Euler (0, 0, angle));
            StartCoroutine ("CoolShot", cooldown);
            gun_ready = false;
        }
    }
    public void Empty(){
        Debug.Log("no ammo!");
    }

    private IEnumerator CoolShot (float seconds) {
        yield return new WaitForSeconds (seconds);
        gun_ready = true;
    }
}