using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrzoPasta : Pastini {
    public override void Fly () {
        transform.Translate (transform.right * config.speed * Time.deltaTime, Space.World);
    }
}