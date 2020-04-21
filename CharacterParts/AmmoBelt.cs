using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBelt : MonoBehaviour {
    Dictionary<int, int> ammo = new Dictionary<int, int> ();
    public int[] keys;
    public int active_id = 0;

    void Start () {
        foreach (GameObject type in GameManager.instance.armoury.ammo_types) {
            this.AddAmmo (type.GetInstanceID (), 10);
        }
        Debug.Log ("Added " + ammo.Count + " ammo types");
    }

    public bool Available (int _key) {
        if (ammo.ContainsKey (_key)) {
            return true;
        }
        return false;
    }
    public bool Available () {
        int _key = keys[active_id];
        if (ammo.ContainsKey (_key)) {
            return true;
        }
        return false;
    }
    public void AddAmmo (int _key, int count) {
        if (ammo.ContainsKey (_key)) {
            ammo[_key] = count;
        }
        ammo.Add (_key, count);
        ammo.Keys.CopyTo (keys, ammo.Count);
    }
    public bool UseRound (int _key) {
        if (ammo.ContainsKey (_key)) {
            ammo[_key] = ammo[_key] - 1;
        }
        return _key;
    }
    public bool UseRound () {
        int _key = keys[active_id];
        if (ammo.ContainsKey (_key)) {
            ammo[_key] = ammo[_key] - 1;
        }
        return _key;
    }
    public int CountRounds (int _key) {
        if (ammo.ContainsKey (_key)) {
            return ammo[_key];
        }
        return 0;
    }
    public int CountRounds () {
        int _key = keys[active_id];
        if (ammo.ContainsKey (_key)) {
            return ammo[_key];
        }
        return 0;
    }

    public void cycle_ammo () {
        active_id++;
        if (active_id > keys.Length) {
            active_id = 0;
        }
    }
}