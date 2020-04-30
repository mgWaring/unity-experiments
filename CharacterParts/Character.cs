using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter {
    //public Dictionary<string, AudioGroup> pools = new Dictionary<string, AudioGroup> ();
    //how to cleanly and easily bind groups of sounds?
    private Rigidbody2D body;
    private PlayerController owning_player;
    private MovementProfile moves;
    private int facing = 1;
    public float jump_cool = 0.05f;
    public float arm_height = 0.61f;
    public float noClip = 0.5f;
    public bool can_jump = true;
    public bool can_double_jump = false;
    public bool jump_ready = true;
    public GroundCheck groundCheck;
    public Sprite death_sprite;
    public GameObject hat;

    public void PledgeAliegence (PlayerController player) {
        this.owning_player = player;
    }
    public void Awake () {
        body = GetComponent<Rigidbody2D> ();
        moves = GetComponent<MovementProfile> ();
    }
    public Vector3 Aim (Vector3 aim) {
        if (aim == Vector3.zero)
            aim = new Vector3 (facing, 0, 0);
        aim = aim + new Vector3 (0, arm_height, 0);
        return transform.position + (aim.normalized * noClip);
    }
    public float AimAngle (Vector3 aim) {
        if (aim == Vector3.zero)
            aim = new Vector2 (facing, 0);
        float _angle = Vector2.SignedAngle (Vector2.right, aim);
        return _angle;
    }
    void Update () {
        CheckLanded ();
    }
    void CheckLanded () {
        if (!groundCheck.WasGrounded () && groundCheck.IsGrounded ()) {
            //neutralise latent Y velocities 
            //body.angularVelocity = 0f;
            //play landing sound
        }
        if (groundCheck.IsGrounded ()) {
            can_jump = true;
            can_double_jump = true;
            jump_ready = true;
        }
    }
    public void Move (Vector2 movement) {
        //called in fixed update
        if(groundCheck.IsGrounded ()){
            body.gravityScale = moves.grounded_gravity;
        } else {
            body.gravityScale = moves.airborne_gravity;
        }

        if (movement.x != 0) {
            FaceForward (movement);
        }

        //if player isn't moving stick, come to a halt
        if ((Mathf.Abs (movement.x) < moves.tolerance)) {
            if (Mathf.Abs (body.velocity.x) <= moves.min_move) {
                body.velocity = new Vector2 (0, body.velocity.y);
            } else {
                body.velocity = new Vector2 (body.velocity.x - (facing * moves.speed_decay), body.velocity.y);
            }
        }
        //if we're under the min movement speed, go straight to the min movespeed
        if ((Mathf.Abs (movement.x) >= moves.tolerance) && (Mathf.Abs (body.velocity.x) < moves.min_move)) {
            body.velocity = new Vector2 (facing * moves.min_move, body.velocity.y);
        }
        //add movement from controller
        if (Mathf.Abs (movement.x) >= moves.tolerance) {
            body.AddForce(new Vector2 (movement.x * moves.speed,0));
        }

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
    public void Jump () {
        if (jump_ready) {
            jump_ready = false;
            if (groundCheck.IsGrounded () && can_jump) {
                can_jump = false;
                body.velocity = new Vector2 (body.velocity.x, body.velocity.y + moves.jump_init);
            } else if (!groundCheck.IsGrounded () && can_double_jump) {
                can_double_jump = false;
                body.velocity = new Vector2 (body.velocity.x, body.velocity.y + moves.jump_repeat);
            }
            StartCoroutine (ResetJump ());
        }
    }
    private IEnumerator ResetJump () {
        yield return new WaitForSeconds (jump_cool);
        jump_ready = true;
    }
    private void TrimVelocity (Vector2 movement) {
        float decay = moves.speed_decay;
        //if we've hit max speed, trim speed
        if (Mathf.Abs (body.velocity.x) > moves.max_move) {
            Debug.Log("trimming x");
            body.velocity = new Vector2 (Mathf.Lerp (body.velocity.x, facing * moves.max_move, decay), body.velocity.y);
        }
        //if we're falling, amplify the fall speed
        if (body.velocity.y < 0) {
            body.velocity = new Vector2 (body.velocity.x, (body.velocity.y * moves.fall_amp));
        }
        //if we're falling more than /3 speed and player is pushing up, float a bit
        if (body.velocity.y < (moves.max_fall / 3) && movement.y > 0.9) {
            body.velocity = new Vector2 (body.velocity.x, Mathf.Lerp (body.velocity.y, (moves.max_fall / 3), decay));
            //if player isn't doing anything and we're exceeding fall speed, trim fall speed
        } else if (body.velocity.y < moves.max_fall && movement.y > -0.9) {
            body.velocity = new Vector2 (body.velocity.x, Mathf.Lerp (body.velocity.y, moves.max_fall, decay));
            //if we're not falling max speed (or rising) and player pulls down, plummet towards max fall speed
        } else if (body.velocity.y > moves.max_fall && movement.y < -0.9) {
            body.velocity = new Vector2 (body.velocity.x, Mathf.Lerp (body.velocity.y, moves.max_fall, decay));
        }
    }
}