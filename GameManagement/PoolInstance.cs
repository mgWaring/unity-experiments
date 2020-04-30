using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoolInstance {
    GameObject _gameObject;
    Transform transform;
    bool reusable;
    ReuseableObject reusableObject;

    public PoolInstance (GameObject instance) {
        _gameObject = instance;
        transform = _gameObject.transform;
        _gameObject.SetActive (false);

        if (_gameObject.GetComponent<ReuseableObject> ()) {
            reusable = true;
            reusableObject = _gameObject.GetComponent<ReuseableObject> ();
        }
    }
    public void Reuse (Vector3 pos, Quaternion rot) {
        _gameObject.SetActive (true);
        if (reusable) {
            reusableObject.OnReuse ();
        }
        transform.position = pos;
        transform.rotation = rot;
    }

    public void SetParent (Transform parent) {
        transform.parent = parent;
    }
    public GameObject GameObject(){
        return _gameObject;
    }
}