using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCamera : MonoBehaviour {
    // Start is called before the first frame update
    public List<GameObject> targets;

    public float min_viewscale;

    public float max_viewscale;
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        Bounds theArea = new Bounds (targets[0].transform.position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
            theArea.Encapsulate (targets[i].transform.position);
        Debug.Log ("camera x = " + theArea.size.x);
        /*
        //add min/max X
        if(theArea.size.x < min_viewscale)
            theArea.size.x = min_viewscale;
        if(theArea.size.x > max_viewscale)
            theArea.size.x = max_viewscale;
        //add min/max Y
        if(theArea.size.y < min_viewscale)
            theArea.size.y = min_viewscale;
        if(theArea.size.y > max_viewscale)
            theArea.size.y = max_viewscale;
            */
        Camera.main.orthographicSize = Mathf.Max (theArea.size.x, theArea.size.y);
        Vector3 nextStep = new Vector3 ();
        nextStep.x = Mathf.Lerp (this.transform.position.x, theArea.center.x, 0.1f);
        nextStep.y = Mathf.Lerp (this.transform.position.y, theArea.center.y, 0.1f);
        nextStep.z = -10f;
        this.transform.position = nextStep;
    }
}