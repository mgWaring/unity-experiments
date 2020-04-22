using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter {
    //public Dictionary<string, AudioGroup> pools = new Dictionary<string, AudioGroup> ();
    //how to cleanly and easily bind groups of sounds?
    private Rigidbody2D body;
    private PlayerController owning_player;
    private MovementProfile moves;
    public float jump_cool = 0.2f;
    private int facing = 1;
    public float arm_height = 0.61f;
    public GroundCheck groundCheck;
    public float noClip = 1.5f;
    public float can_defend = 0.0f;
    public float can_fire = 0.0f;
    public bool utility_1 = false;
    public bool utility_2 = false;
    public bool can_jump = true;
    public bool can_roll = false;
    public bool can_swap = false;
    public bool can_grab = false;
    public bool can_crouch = false;
    public bool can_double_jump = false;

    public void PledgeAliegence (PlayerController player) {
        this.owning_player = player;
    }
    public void Awake () {
        body = GetComponent<Rigidbody2D> ();
        moves = GetComponent<MovementProfile> ();
    }
    public Vector3 Aim (Vector3 aim) {
        float vert = Mathf.Sign (aim.y) * noClip;
        float horz = Mathf.Sign (aim.x) * noClip;
        if (aim == Vector3.zero) {
            vert = 0f;
            horz = facing * noClip;
        }
        Vector3 target = new Vector3 (aim.x + horz, (aim.y + vert) + arm_height, 0) + transform.position;
        return target;
    }
    public float AimAngle (Vector3 aim) {
        if (aim == Vector3.zero) {
            aim = new Vector2 (facing, 0);
        }
        float _angle = Vector2.Angle (Vector2.right, aim);
        _angle = (aim.y > 0) ? _angle : _angle * -1;
        return _angle;
    }
    void Update () {
        CheckLanded ();
    }
    void CheckLanded () {
        if (!groundCheck.WasGrounded () && groundCheck.IsGrounded ()) {
            //neutralise latent Y velocities 
            body.angularVelocity = 0f;
            can_jump = true;
            //play landing sound
        }
    }
    public void Move (Vector2 movement) {
        //called in fixed update

        if (movement.x != 0) {
            FaceForward (movement);
        }

        float verticalForce = 0;

        //if player isn't moving stick, come to a halt
        if ((Mathf.Abs (movement.x) < moves.tolerance) && groundCheck.IsGrounded ()) {
            if (Mathf.Abs (body.velocity.x) <= moves.min_move) {
                body.velocity = new Vector2 (0, body.velocity.y);
            } else {
                body.velocity = new Vector2 (body.velocity.x - (facing * moves.speed_decay), body.velocity.y);
            }
        }
        //
        if ((Mathf.Abs (movement.x) >= moves.tolerance) && (Mathf.Abs (body.velocity.x) < moves.min_move)) {
            body.velocity = new Vector2 (facing * moves.min_move, body.velocity.y);
        }
        body.velocity = new Vector2 (body.velocity.x + (movement.x * moves.speed), body.velocity.y);

        TrimVelocity (movement);
    }
    private void FaceForward (Vector2 movement) {
        int oldFacing = facing;
        facing = (int) Mathf.Sign (movement.x);
        if (facing != oldFacing) {
            Quaternion target = Quaternion.Euler (0, (facing > 0) ? 0 : 180, 0);
            transform.rotation = Quaternion.Slerp (transform.rotation, target, 0.9f);
        }
    }
    public void TryToJump () {
        if (can_jump) {
            can_jump = false;
            Jump ();
            StartCoroutine ("PrepareJump", true);
        }
    }
    public void Jump () {
        if (groundCheck.IsGrounded ()) {
            body.velocity = new Vector2 (body.velocity.x, body.velocity.y + moves.jump_init);
            can_double_jump = false;
            StartCoroutine ("PrepareJump", true);
        }
        if (!groundCheck.IsGrounded () && can_double_jump) {
            can_double_jump = false;
            body.velocity = new Vector2 (body.velocity.x, body.velocity.y + moves.jump_repeat);
        }
    }
    //separate double and single jump to improve reliability
    private IEnumerator PrepareJump (bool cool_double) {
        yield return new WaitForSeconds (jump_cool);
        if (cool_double) {
            can_double_jump = true;
        }
        can_jump = true;
    }
    private void TrimVelocity (Vector2 movement) {
        if (Mathf.Abs (body.velocity.x) > moves.max_move) {
            body.velocity = new Vector2 (Mathf.Lerp (body.velocity.x, facing * moves.max_move, moves.speed_decay), body.velocity.y);
        }
        if (body.velocity.y < 0) {
            body.velocity = new Vector2 (body.velocity.x, body.velocity.y * moves.fall_amp);
        }
        if (body.velocity.y < moves.max_fall && movement.y > 0.9) {
            body.velocity = new Vector2 (body.velocity.x, Mathf.Lerp (body.velocity.y, moves.max_fall / 3, moves.speed_decay));
        } else if (body.velocity.y < moves.max_fall && movement.y > -0.9) {
            body.velocity = new Vector2 (body.velocity.x, Mathf.Lerp (body.velocity.y, moves.max_fall, moves.speed_decay));
        } else if (body.velocity.y > moves.max_fall && movement.y < -0.9) {
            body.velocity = new Vector2 (body.velocity.x, Mathf.Lerp (body.velocity.y, moves.max_fall, moves.speed_decay));
        }
    }
}