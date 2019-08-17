using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strike : MonoBehaviour
{
    private RollHandler roller;
    private ParticleSystem system;
    private casterScript enemy = null;

    void Start()
    {
        system = GetComponent<ParticleSystem>();

        GameObject input = GameObject.Find("Input and Game Logic");
        roller = input.GetComponent<RollHandler>();
    }

    void Update()
    {
        if (!system.IsAlive() && enemy == null)
        {
            Destroy(gameObject); 
        }
        else
        {
            int dam = roller.isDone();
            if (dam != 0)
            {
                enemy.takeDamage(dam);
                roller.Reset();
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
    	// Debug.Log("OnEnter ran " + coll.collider.tag);
		if (coll.collider.tag == "Player2" || coll.collider.tag == "Player")
		{
            roller.rollDice(2);
			enemy = coll.gameObject.GetComponent<casterScript>();
		}
    }
}

