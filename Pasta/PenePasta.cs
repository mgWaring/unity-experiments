using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenePasta : Pastini {
    public override void Fly () {
        transform.position += (transform.right + new Vector3 (speed, 0, 0)) * Time.deltaTime;
    }
    public override void Launch() {
        //Debug.Log("firing a pene");
    }
}