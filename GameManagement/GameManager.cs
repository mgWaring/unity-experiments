using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameSingleton<GameManager> {
      public Armoury armoury;
      public PlayerIndex playerIndex;
      public Roster roster;
      public SceneLoader sceneLoader;
      public Scoreboard scoreboard;
      public SoundPlayer soundPlayer;
      protected override void OnAwake () {
            armoury = gameObject.GetComponent<Armoury> ();
            playerIndex = gameObject.GetComponent<PlayerIndex> ();
            roster = gameObject.GetComponent<Roster> ();
            sceneLoader = gameObject.GetComponent<SceneLoader> ();
            scoreboard = gameObject.GetComponent<Scoreboard> ();
            soundPlayer = gameObject.GetComponent<SoundPlayer> ();
      }
}