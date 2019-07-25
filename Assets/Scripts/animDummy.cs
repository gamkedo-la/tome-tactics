using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animDummy : MonoBehaviour
{
	public GameObject childCaster;
    private Transform casterTrans;

    void Start() { transform.DetachChildren(); }

    void Update() { }

    public void childStop()
    {
        childCaster = gameObject.transform.GetChild(0).gameObject;
    	childCaster.GetComponent<casterScript>().stopCast();
    }
}
