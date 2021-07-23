using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrappleAction : IAct
{
    public void Act(Player player)
    {
        Debug.Log("Grapple it!");
    }    
}