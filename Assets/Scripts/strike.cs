using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strike : MonoBehaviour
{
    void Start() { }

    void Update() { }

    void OnCollisionEnter(Collision coll)
    {
    	// Debug.Log("OnEnter ran");

    	if (coll.collider.tag != "Player")
    	{
    		if (coll.collider.tag == "Opponent")
    		{
    			casterScript sucker = coll.gameObject.GetComponent<casterScript>();
    			sucker.takeDamage(20);
    		}
    	}
    }
}

