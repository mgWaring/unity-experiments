using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrzoPasta : Pastini {
    public override void Fly () {
        transform.position += (transform.right + new Vector3 (config.speed, 0, 0)) * Time.deltaTime;
    }
}