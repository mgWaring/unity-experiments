using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject {
    [SerializeField]
    private string _gameVersion = "0.0.0";
    public string GameVersion { get { return _gameVersion; } }

    [SerializeField]
    private string _userName = "max";
    public string UserName {
        get {
            int rand = Random.Range (0, 9999);
            return _userName + rand.ToString ();
        }
    }
}