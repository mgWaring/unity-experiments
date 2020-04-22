using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrzoPasta : Pastini {
    public void Reset () {
        speed = 15f;
        lifetime = 0.5f;
        cooldown = 0.08f;
        bounces = 0;
        bounce_limit = 1;
    }
    public override void Fly () {
        transform.position += (transform.right + new Vector3 (speed, 0, 0)) * Time.deltaTime;
    }
    public override void Launch () { }
}