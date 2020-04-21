using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armoury : MonoBehaviour {
        public GameObject[] ammo_types;

        void Start () {
                foreach (GameObject type in ammo_types) {
                        ObjectPoolManager.instance.CreatePool (type, 5);
                }
        }

        public void Speak(string str){
                Debug.Log("Armoury says: " + str);
        }
}