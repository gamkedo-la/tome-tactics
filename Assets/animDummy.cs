using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animDummy : MonoBehaviour
{
	public GameObject childCaster;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void childStop()
    {
    	childCaster.GetComponent<casterScript>().stopCast();
    }
}
