using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strike : MonoBehaviour
{
    private ParticleSystem system;

    void Start() { system = GetComponent<ParticleSystem>(); }

    void Update()
    {
        if (!system.IsAlive()) { Destroy(gameObject); }
    }

    void OnCollisionEnter(Collision coll)
    {
    	// Debug.Log("OnEnter ran");
		if (coll.collider.tag == "Opponent" || coll.collider.tag == "Player")
		{
			casterScript sucker = coll.gameObject.GetComponent<casterScript>();
			sucker.takeDamage(20);
		}
    }
}

