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

    public bool Ready () {
        return gun_ready;
    }

    public void Shoot (GameObject shot) {
        ObjectPoolManager.instance.ReuseObject (shot.GetInstanceID (), transform.position, Quaternion.Euler (0, 0, angle));
        if (shot.GetComponent<Pastini> ())
            StartCoroutine ("CoolShot", shot.GetComponent<Pastini> ().cooldown);
        gun_ready = false;
    }
    public void Empty () {
        Debug.Log ("no ammo!");
    }

    private IEnumerator CoolShot (float seconds) {
        yield return new WaitForSeconds (seconds);
        gun_ready = true;
    }
}