using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridPopulator : MonoBehaviour {
    public GameObject buttonPrefab;
    private GameManager manager;
    void Start () {
        manager = GameManager.instance;
        //Debug.Log ("Loaded " + manager.sceneLoader.levels.Count + " scenes.");

        foreach (string level in manager.sceneLoader.levels) {
            //Debug.Log ("one is: " + level);
            GameObject thisButton = Instantiate (buttonPrefab);
            thisButton.GetComponent<Button> ().onClick.AddListener (delegate { manager.sceneLoader.LoadByName (level); });
            thisButton.GetComponentInChildren<TextMeshProUGUI> ().text = level;
            thisButton.transform.parent = gameObject.transform;
        }
    }
}