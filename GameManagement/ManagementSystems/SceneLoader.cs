using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneLoader : MonoBehaviour {
    public string menu_name = "menu";
    public string score_name = "scoreboard";
    public List<string> excludeList;
    public List<string> levels;
    public string sceneFilename = "Levels";
    public string levelsDirectory = "Levels";
    public string resourcesDirectory = "Resources";
    public string[] scenes;
    //-----

#if UNITY_EDITOR   
    //-----------------------------------------------------------------------------------------------------
    private static string[] ReadNames (List<string> excludeList) {
        List<string> temp = new List<string> ();
        //Debug.Log ("Exclusions will be: " + String.Join (", ", excludeList.ToArray ()));
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes) {
            if (S.enabled) {
                string name = S.path.Substring (S.path.LastIndexOf ('/') + 1);
                name = name.Substring (0, name.Length - 6);
                if (excludeList.Contains (name)) {
                    continue;
                }
                temp.Add (name);
            }
        }
        string[] arr = temp.ToArray ();
        //Debug.Log ("Scenes are: " + String.Join (", ", arr));
        return arr;
    }

    [UnityEditor.MenuItem ("CONTEXT/SceneLoader/Update Scene Names")]
    private static void UpdateNames (UnityEditor.MenuCommand command) {
        SceneLoader context = (SceneLoader) command.context;
        context.PopulateExclusions ();
        context.scenes = ReadNames (context.excludeList);
        context.SaveNameFile ();
    }

    [UnityEditor.MenuItem ("CONTEXT/SceneLoader/Force Reload")]
    private static void ForceReload (UnityEditor.MenuCommand command) {
        SceneLoader context = (SceneLoader) command.context;
        context.LoadLevelNames ();
    }

    private void Reset () {
        scenes = ReadNames (excludeList);
        SaveNameFile ();
    }

    void CreateResourceDirs () {
        if (!System.IO.Directory.Exists (Application.dataPath + "/" + resourcesDirectory)) {
            System.IO.Directory.CreateDirectory (Application.dataPath + "/" + resourcesDirectory);
        }
        if (!System.IO.Directory.Exists (Application.dataPath + "/" + resourcesDirectory + "/" + levelsDirectory)) {
            System.IO.Directory.CreateDirectory (Application.dataPath + "/" + resourcesDirectory + "/" + levelsDirectory);
        }
    }

    string BuildSaveFilename () {
        return Application.dataPath + "/" + resourcesDirectory + "/" + levelsDirectory + "/" + sceneFilename + ".bytes";
    }

    void SaveNameFile () {
        CreateResourceDirs ();
        //Debug.Log ("Saving level names..." + BuildSaveFilename ());

        FileStream file = File.Create (BuildSaveFilename ());
        BinaryWriter bw = new BinaryWriter (file);

        bw.Write (scenes.Count ());
        for (int i = 0; i < scenes.Count (); ++i) {
            bw.Write (scenes[i]);
        }

        bw.Close ();
        file.Close ();

        // Ensure the assets are all realoaded and the cache cleared.
        UnityEditor.AssetDatabase.Refresh ();
    }
    private void PopulateExclusions () {
        if (!excludeList.Contains (menu_name)) {
            excludeList.Add (menu_name);
        }
        if (!excludeList.Contains (score_name)) {
            excludeList.Add (score_name);
        }
    }

    void Awake () {
        PopulateExclusions ();
        Reset ();
        //Debug.Log (this.levels);
    }
    //-----------------------------------------------------------------------------------------------------
#else
    void Awake () {
        string str = LoadLevelNames ();
        //Debug.Log ("Levels loaded are: " + str);
    }
    //-----------------------------------------------------------------------------------------------------
#endif

    string BuildLoadFilename () {
        return levelsDirectory + "/" + sceneFilename;
        //return "LevelNames/" + sceneFilename;
    }
    string LoadLevelNames () {
        string returnString = "Loaded: ";
        TextAsset levelNamesAsset = Resources.Load (BuildLoadFilename ()) as TextAsset;
        if (levelNamesAsset != null) {
            Stream s = new MemoryStream (levelNamesAsset.bytes);
            BinaryReader br = new BinaryReader (s);
            int numLevels = br.ReadInt32 ();
            if (numLevels > 0) {
                levels = new List<string> ();
                for (int i = 0; i < numLevels; ++i) {
                    string nom = br.ReadString ();
                    levels.Add (nom);
                    returnString += nom;
                }
            }
        }
        return returnString;
    }
    public void LoadNextScene () {
        int this_index = SceneManager.GetActiveScene ().buildIndex;
        LoadAtIndex (this_index + 1);
    }

    public void LoadByName (string name) {
        SceneManager.LoadScene (name);
    }
    public void LoadAtIndex (int index) {
        SceneManager.LoadScene (index);
    }
    public void LoadMenu () {
        SceneManager.LoadScene (menu_name);
    }

    public void LoadScoreboard () {
        SceneManager.LoadScene (score_name);
    }

}