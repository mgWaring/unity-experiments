using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour {
    public SpriteRenderer ammo_renderer;
    public SpriteRenderer character_renderer;
    public GameObject scores;
    public void UpdateAmmoDisplay (AmmoBelt ammo){
        ammo_renderer.sprite = ammo.ActiveAmmo().gameObject.GetComponent<SpriteRenderer>().sprite;

    }
    public void UpdateCharacterDisplay (Character character){
        character_renderer.sprite = character.gameObject.GetComponent<SpriteRenderer>().sprite;

    }
    public void UpdateScoreDisplay (Scoreboard scoreboard){

    }
}