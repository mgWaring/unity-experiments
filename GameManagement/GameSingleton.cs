using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSingleton<T> : MonoBehaviour where T : Component {
    private static T _instance = null;
    public static T instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<T> ();
                if (_instance == null) {
                    GameObject go = new GameObject ();
                    go.name = typeof (T).Name;
                    _instance = go.AddComponent<T> ();
                    DontDestroyOnLoad (go);
                }
            }
            return _instance;
        }
    }
    public virtual void Awake () {
        if (_instance == null) {
            _instance = this as T;
            DontDestroyOnLoad (this.gameObject);
        } else {
            Destroy (gameObject);
        }
        OnAwake ();
    }
    protected virtual void OnAwake () { 
        //Debug.Log (typeof (T).Name + " is awake");
    }
}