using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : GameSingleton<ObjectPoolManager> {
    Dictionary<int, Queue<PoolInstance>> pools = new Dictionary<int, Queue<PoolInstance>> ();
    public void CreatePool (GameObject prefab, int size) {
        int key = prefab.GetInstanceID ();

        GameObject pool = new GameObject (prefab.name + "_pool");
        pool.transform.parent = transform;

        if (!pools.ContainsKey (key)) {
            pools.Add (key, new Queue<PoolInstance> ());

            for (int i = 0; i < size; i++) {
                PoolInstance instance = new PoolInstance (Instantiate (prefab) as GameObject);
                pools[key].Enqueue (instance);
                instance.SetParent (pool.transform);
            }
        }
    }

    public void ReuseObject (int key, Vector3 pos, Quaternion rot) {
        if (pools.ContainsKey (key)) {
            PoolInstance reused = pools[key].Dequeue ();
            pools[key].Enqueue (reused);
            reused.Reuse (pos, rot);
        }
    }
}