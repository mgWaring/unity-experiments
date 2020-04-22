using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour {
    public GameObject ammo_renderer;
    public GameObject ammo_text;
    public GameObject character_renderer;
    public GameObject character_text;
    public GameObject scores;
    public void UpdateAmmoDisplay (AmmoBelt ammo){
        ammo_renderer.GetComponent<Image>().sprite = ammo.ActiveAmmo().GetComponent<SpriteRenderer>().sprite;
        ammo_text.GetComponent<TextMeshProUGUI> ().text = ammo.Available().ToString();

    }
    public void UpdateCharacterDisplay (Character character){
        character_renderer.GetComponent<Image>().sprite = character.GetComponent<SpriteRenderer>().sprite;
        character_text.GetComponent<TextMeshProUGUI> ().text = character.transform.position.ToString();

    }
    public void UpdateScoreDisplay (Scoreboard scoreboard){

    }
}