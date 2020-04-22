using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenePasta : Pastini {
    public void Reset () {
        speed = 10f;
        lifetime = 2f;
        cooldown = 0.4f;
        bounces = 0;
        bounce_limit = 1;
    }
    public override void Fly () {
        transform.position += (transform.right + new Vector3 (speed, 0, 0)) * Time.deltaTime;
    }
    public override void Launch() {
    }
}