using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armoury : MonoBehaviour {
        public GameObject[] ammo_types;

        private Dictionary<int, GameObject> _prefabs = new Dictionary<int, GameObject> ();

        void Start () {
                foreach (GameObject type in ammo_types) {
                        ObjectPoolManager.instance.CreatePool (type, 500);
                        _prefabs.Add (type.GetInstanceID (), type);
                }
        }

        public GameObject Find (int instance_id) {
                if (_prefabs.ContainsKey (instance_id)) {
                        return _prefabs[instance_id];
                }
                throw new KeyNotFoundException();
        }
}