using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementProfile : MonoBehaviour {
    public float max_move;
    public float max_fall;
    public float min_move;
    public float tolerance;
    public float speed;
    public float jump_init;
    public float jump_repeat;
    public float speed_decay;
    public float fall_amp;
    public float grounded_gravity;
    public float airborne_gravity;
}