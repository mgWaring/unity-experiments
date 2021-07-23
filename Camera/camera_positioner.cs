using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_positioner : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
    }

    void Update()
    {
        Vector3 nextStep = new Vector3();
        nextStep.x = Mathf.Lerp(this.transform.position.x, target.transform.position.x, 0.1f); 
        nextStep.y = Mathf.Lerp(this.transform.position.y, target.transform.position.y, 0.1f); 
        nextStep.z = -10f;
        this.transform.position = nextStep;       
    }
}
