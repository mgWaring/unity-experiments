using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] public Character character;
    [SerializeField] public PastaGun gun;
    [SerializeField] private Hat hat;
    [SerializeField] public IAct[] actions;
    [SerializeField] private AmmoBelt ammo;
    [SerializeField] private HudManager hud;

    private Vector2 aim_val = new Vector2(0, 0);
    private Vector2 move_val = new Vector2(0, 0);
    private PlayerInput input;

    void Start()
    {
        character = (character == null) ? GetComponent<Character>() : character;
        ammo = (ammo == null) ? GetComponent<AmmoBelt>() : ammo;
        hud = (hud == null) ? GetComponentInChildren<HudManager>() : hud;
        character.PledgeAliegence(this);
    }
    void OnEnable()
    {
        input.Enable();
    }
    void OnDisable()
    {
        input.Disable();
    }
    void Awake()
    {
        //wire this into the stolen character class and remove the imput processing code that already exists there
        input = new PlayerInput();
        input.Player.Jump.performed += ctx => character.Jump();
        input.Player.Jump.canceled += ctx => character.JumpReleased();
        input.Player.Swap.performed += ctx => ammo.Cycle_ammo();
        input.Player.Fire.performed += ctx =>
        {
            if (ammo.Available() > 0) gun.Shoot(ammo.UseRound());
            else gun.Empty();
        };
        input.Player.Move.started += move => move_val = input.Player.Move.ReadValue<Vector2>();
        input.Player.Move.canceled += move => move_val = new Vector2(0, 0);
        input.Player.Aim.started += move => move_val = input.Player.Move.ReadValue<Vector2>();
        input.Player.Aim.canceled += move => move_val = new Vector2(0, 0);
    }
    public void AssignCharacter(Character new_character)
    {
        character = new_character;
    }

    void Update()
    {
        Hud();
        Aim();
        character.Move(move_val);
    }

    private void Hud()
    {
        hud.UpdateAmmoDisplay(ammo);
        hud.UpdateCharacterDisplay(character);
        hud.UpdateGunDisplay(gun);
        hud.UpdateScoreDisplay(GameManager.instance.scoreboard);
    }

    public void Aim()
    {
        gun.AimTo(character.Aim(aim_val), character.AimAngle(aim_val));
    }
}