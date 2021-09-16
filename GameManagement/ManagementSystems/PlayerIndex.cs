using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndex : MonoBehaviour
{
    private Dictionary<string, SWPlayer> players;
    //when a new player connects, add them to this list
    public void AddPlayer(SWPlayer player)
    {
        players.Add(player.name, player);
    }
}