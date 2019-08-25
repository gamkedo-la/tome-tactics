﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strike : MonoBehaviour
{
    private RollHandler roller;
    private mouseInput gameLogic;
    private ParticleSystem system;
    private casterScript enemy = null;
    private minionScript minion = null;

    void Start()
    {
        system = GetComponent<ParticleSystem>();

        GameObject input = GameObject.Find("Input and Game Logic");
        roller = input.GetComponent<RollHandler>();
        gameLogic = input.GetComponent<mouseInput>();
        gameLogic.setIsCasting(true);
    }

    void Update()
    {
        if (!system.IsAlive() && enemy == null)
        {
            gameLogic.setIsCasting(false);
            Destroy(gameObject); 
        }
        else
        {
            int dam = roller.isDone();
            if (dam != 0)
            {
                if (enemy)
                    enemy.takeDamage(dam);
                else if (minion)
                    minion.takeDamage(dam);

                roller.Reset();
                gameLogic.setIsCasting(false);
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
        else if (coll.collider.tag == "Player2Minion" || coll.collider.tag == "PlayerMinion")
        {
            roller.rollDice(2);
            minion = coll.gameObject.GetComponent<minionScript>();
        }
    }
}

