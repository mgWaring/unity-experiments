using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameSingleton<GameManager> {
      public Armoury armoury;
      public NetworkManager networkManager;
      public PlayerIndex playerIndex;
      public Roster roster;
      public SceneLoader sceneLoader;
      public Scoreboard scoreboard;
      public SoundPlayer soundPlayer;
      void Awake () {
            armoury = gameObject.GetComponent<Armoury> ();
            networkManager = gameObject.GetComponent<NetworkManager> ();
            playerIndex = gameObject.GetComponent<PlayerIndex> ();
            roster = gameObject.GetComponent<Roster> ();
            sceneLoader = gameObject.GetComponent<SceneLoader> ();
            scoreboard = gameObject.GetComponent<Scoreboard> ();
            soundPlayer = gameObject.GetComponent<SoundPlayer> ();
      }
      public void Speak (string str) {
            Debug.Log ("we're inside the GameManager: " + str);
      }
}