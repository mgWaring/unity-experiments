using System.Collections;
using UnityEngine;

public class ReuseableObject : MonoBehaviour {
    public virtual void OnReuse (){}
    protected void Destroy () {
        gameObject.SetActive (false);
    }
}