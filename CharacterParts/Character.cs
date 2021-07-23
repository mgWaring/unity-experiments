using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Controller2D))]

//previously known as player
public class Character : MonoBehaviour
{
    // Inspector Variables
    [SerializeField] private float moveSpeed = 6f; // 6f
    public float MoveSpeed { get { return moveSpeed; } }
    // Assign values to these to determine gravity and jumpVelocity
    [SerializeField] private float maxJumpHeight = 4f; // 4f
    [SerializeField] private float timeToJumpApex = 0.4f; // 0.4f
    // X Velocity Smoothing Variables
    [SerializeField] private float accelerationTimeAirborne = 0.2f; // 0.2f
    [SerializeField] private float accelerationTimeGrounded = 0.1f; // 0.1f
    [SerializeField] private float wallSlideSpeedMax = 3;
    [SerializeField] private Vector2 wallJumpClimb;
    [SerializeField] private Vector2 wallJumpOff;
    [SerializeField] private Vector2 wallLeap;
    [SerializeField] private float wallStickTime = 0.25f;
    [SerializeField] private float jump_cool, flip_duration = 0.1f;
    [SerializeField] private float arm_height = 0.25f;
    [SerializeField] private float arm_length = 0.25f;
    //move this
    [SerializeField] public Sprite death_sprite;

    private Player player;
    private bool jump_ready, jump, swing;
    private float flip_progress, facing = 1f;
    private Coroutine cooldown;
    // Start Variables
    public Controller2D controller;
    public Movement movement;

    // Interfaces
    public IUnityService UnityService;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D>();
        //movement = new Movement();

        if (UnityService == null)
            UnityService = new UnityService();

    }

    // Input dependent variables should be checked here because
    // Update is called more frequently than FixedUpdate()
    public void Move(Vector2 input)
    {
        int wallDirX = (controller.Collisions.left) ? -1 : 1;

        movement.CalculateVelocityX(
            input.x,
            (controller.Collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne
            );

        bool wallSliding = false;
        if ((controller.Collisions.left || controller.Collisions.right) && !controller.Collisions.below && movement.Velocity.y < 0)
        {
            wallSliding = true;
            movement.CalculateWallSlide(input.x, wallDirX, UnityService.GetFixedDeltaTime());
        }

        if (jump && jump_ready)
        {
            if (wallSliding)
            {
                movement.CalculateWallJump(input.x, wallDirX);

            }
            if (controller.Collisions.below)
            {
                movement.Jump(transform.position.y);
            }
        }

        if (!jump)
        {
            movement.DoubleGravity();
        }

        Vector3 velocity = movement.CalculateVelocity(UnityService.GetFixedDeltaTime(), transform.position.y);
        controller.Move(velocity);
        FaceForward(velocity);


        // Removes the accumulation of gravity
        if (controller.Collisions.above || controller.Collisions.below)
        {
            movement.ZeroVelocityY();
        }
    }
    public void Jump()
    {
        jump = true;
        cooldown = StartCoroutine(ResetJump());
    }
    public void JumpReleased()
    {
        jump = false;
        if (cooldown != null)
        {
            StopCoroutine(cooldown);
            jump_ready = true;
        }
    }

    public void PledgeAliegence(Player _player)
    {
        player = _player;
    }
    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(jump_cool);
        jump_ready = true;
    }
    private void FaceForward(Vector3 input)
    {
        flip_progress += Time.deltaTime;
        float oldFacing = facing;
        facing = Mathf.Sign(input.x);
        Quaternion target = Quaternion.Euler(0, (facing > 0) ? 0 : 180, 0);

        if (facing != oldFacing) flip_progress = 0;
        if (flip_progress <= flip_duration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, flip_progress / flip_duration);
        }
    }
    public Vector3 Aim(Vector3 aim)
    {
        aim = (aim == Vector3.zero) ? new Vector3(facing, 0, 0) : aim;
        aim += (new Vector3(0, arm_height, 0));
        return transform.position + (aim.normalized * arm_length);
    }
    public float AimAngle(Vector3 aim)
    {
        aim = (aim == Vector3.zero) ? new Vector3(facing, 0) : aim;
        float _angle = Vector2.SignedAngle(Vector2.right, aim);
        return _angle;
    }
}
