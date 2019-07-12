// targeting.cs - Dominick Aiudi 2019
//
// Attached to TargetingOrb prefab.
// Makes GameObject follow mouse when aiming spells.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targeting : MonoBehaviour
{
	public Camera cam;

    void Start()
    {
    	cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
        	gameObject.transform.position = hit.point;


        if (Input.GetKeyUp("mouse 0") || Input.GetKeyUp("mouse 1"))
            Destroy(gameObject);
    }
}
