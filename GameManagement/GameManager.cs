using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameSingleton<GameManager>
{
    public NetworkManager networkManager;
    public Armoury armoury;
    public Roster roster;
    public Scoreboard scoreboard;
    public SoundPlayer soundPlayer;
    //scenery pool
    //particle pool
    //enemy pool
    protected override void OnAwake()
    {
        networkManager = gameObject.GetComponent<NetworkManager>();
        armoury = gameObject.GetComponent<Armoury>();
        roster = gameObject.GetComponent<Roster>();
        scoreboard = gameObject.GetComponent<Scoreboard>();
        soundPlayer = gameObject.GetComponent<SoundPlayer>();
        if (networkManager)
        {
            networkManager.playerIndex = gameObject.GetComponent<PlayerIndex>();
            networkManager.sceneLoader = gameObject.GetComponent<SceneLoader>();
        }
    }
}