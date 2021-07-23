using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscQuit : MonoBehaviour
{
    // F**k this s**t i'm out
    void Update(){
        if(Input.GetKeyDown("escape")){
            Application.Quit();
        }
    }
}
