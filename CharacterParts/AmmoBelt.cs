using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBelt : MonoBehaviour {

    //Dict<instnaceId, number of rounds>
    Dictionary<int, int> ammo = new Dictionary<int, int> ();
    List<GameObject> chamber = new List<GameObject> ();
    int active_id = 0;

    void Start () {
        foreach (GameObject type in GameManager.instance.armoury.ammo_types) {
            this.AddAmmo (type, 100);
        }
    }

    public GameObject ActiveAmmo () {
        return chamber[active_id];
    }
    public void AddAmmo (GameObject type, int count) {
        if (ammo.ContainsKey (type.GetInstanceID ()) && chamber.Contains (type)) {
            ammo[type.GetInstanceID ()] += count;
            return;
        }
        ammo.Add (type.GetInstanceID (), count);
        chamber.Add (type);
    }
    public int Available (int _key) {
        if (ammo.ContainsKey (_key)) {
            return ammo[_key];
        }
        return 0;
    }
    public int Available () {
        int _key = chamber[active_id].GetInstanceID ();
        if (ammo.ContainsKey (_key)) {
            return ammo[_key];
        }
        return 0;
    }
    public GameObject UseRound (int _key) {
        if (ammo.ContainsKey (_key)) {
            ammo[_key]--;
            return chamber.Find (x => x.GetInstanceID () == _key);
        }
        throw new KeyNotFoundException ();
    }
    public GameObject UseRound () {
        int _key = chamber[active_id].GetInstanceID ();
        ammo[_key]--;
        return chamber[active_id];
    }
    public int Cycle_ammo () {
        active_id = (active_id >= chamber.Count-1) ? 0 : active_id + 1;
        return active_id;
    }
}