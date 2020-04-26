using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenePasta : Pastini {
    public override void Fly () {
        transform.Translate (transform.right * config.speed * Time.deltaTime, Space.World);
    }
}