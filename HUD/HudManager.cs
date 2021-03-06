﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {
    public GameObject ammo_renderer;
    public GameObject ammo_text;
    public GameObject character_renderer;
    public GameObject character_text;
    public GameObject gun_pos_text;
    public GameObject gun_angle_text;
    public GameObject scores;
    public void UpdateAmmoDisplay (AmmoBelt ammo) {
        ammo_renderer.GetComponent<Image> ().sprite = ammo.ActiveAmmo ().GetComponent<SpriteRenderer> ().sprite;
        ammo_text.GetComponent<TextMeshProUGUI> ().text = ammo.Available ().ToString ();

    }
    public void UpdateCharacterDisplay (Character character) {
        Sprite _sprite = character.death_sprite;
        string _text = "Sei morto";
        if (character.gameObject.activeSelf) {
            _sprite = character.GetComponent<SpriteRenderer> ().sprite;
            _text = character.GetComponent<Rigidbody2D> ().velocity.x.ToString ();
        }
        character_renderer.GetComponent<Image> ().sprite = _sprite;
        character_text.GetComponent<TextMeshProUGUI> ().text = _text;

    }
    public void UpdateGunDisplay (PastaGun gun) {
        gun_pos_text.GetComponent<TextMeshProUGUI> ().text = "Deg: " + gun.angle.ToString ();
        gun_angle_text.GetComponent<TextMeshProUGUI> ().text = " X: " + gun.transform.position.x.ToString () + " Y: " + gun.transform.position.y.ToString ();

    }
    public void UpdateScoreDisplay (Scoreboard scoreboard) {

    }
}