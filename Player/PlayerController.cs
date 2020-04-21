using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //used when querying the Rewired input manager
    public int player_id;
    private AmmoBelt ammo;
    //provide access to actions through this array
    public IAct[] actions;
    public PastaGun gun;

    //bind this properly ----------------------------------------
    public Character character;
    public float noClip = 2.5f;
    private Player rw_player;
    //input receivers\\
    private Vector2 aim;
    private Vector2 movement;
    private float defend = 0.0f;
    private float fire = 0.0f;
    private bool utility_1 = false;
    private bool utility_2 = false;
    private bool jump = false;
    private bool roll = false;
    private bool swap = false;
    private bool grab = false;
    private bool crouch = false;
    public bool double_jump = false;
    // Start is called before the first frame update
    void Start () {
        rw_player = ReInput.players.GetPlayer (player_id);
        character = (character == null) ? gameObject.GetComponent<Character> () : character;
        ammo = (ammo == null) ? gameObject.GetComponent<AmmoBelt> () : ammo;
        character.PledgeAliegence (this);
    }

    public void AssignCharacter (Character new_character) {
        character = new_character;
    }

    void FixedUpdate () {
        character.Move (movement);
    }

    void Update () {
        GetInput ();
        HandleButtons ();
        Aim ();
    }

    private void GetInput () {
        aim.x = rw_player.GetAxis ("a_horiz");
        aim.y = rw_player.GetAxis ("a_verti");
        movement.x = rw_player.GetAxis ("m_horiz");
        movement.y = rw_player.GetAxis ("m_verti");
        fire = rw_player.GetAxis ("fire");
        defend = rw_player.GetAxis ("defend");
        if (rw_player.GetButtonDown ("utility_1")) {
            utility_1 = true;
            Debug.Log("utility_1");
        }
        if (rw_player.GetButtonDown ("utility_2")) {
            utility_2 = true;
            Debug.Log("utility_2");
        }
        if (rw_player.GetButtonDown ("jump")) {
            jump = true;
            Debug.Log("jump");
        }
        if (rw_player.GetButtonDown ("roll")) {
            roll = true;
            Debug.Log("roll");
        }
        if (rw_player.GetButtonDown ("swap")) {
            swap = true;
            Debug.Log("swap");
        }
        if (rw_player.GetButtonDown ("grab")) {
            grab = true;
            Debug.Log("grab");
        }
    }

    public void HandleButtons () {
        if (fire > 0) {
            if (ammo.Available ()) {
                gun.Shoot (ammo.UseRound ());
            } else {
                gun.Empty();
            }
        }
        if(swap){
            ammo.cycle_ammo();
        }
        if (jump) {
            character.TryToJump ();
            jump = false;
        }
    }

    public void Aim () {
        gun.AimTo (character.Aim (aim), character.AimAngle (aim));
    }
}