using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrzoPasta : Pastini {
    public void Fly () { 
        transform.position += (transform.right + new Vector3 (speed, 0, 0)) * Time.deltaTime;
    }
}