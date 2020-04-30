using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roster : MonoBehaviour {
        public GameObject[] characters;
        public int instance_count = 2;

        private Dictionary<int, GameObject> _prefabs = new Dictionary<int, GameObject> ();

        void Start () {
                foreach (GameObject type in characters) {
                        ObjectPoolManager.instance.CreatePool (type, instance_count);
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