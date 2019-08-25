﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour
{
    private RollHandler roller;
    private mouseInput gameLogic;
    private casterScript enemy = null;
    private minionScript minion = null;
    private bool slide = false, stop = false;
    private int stopCount = 30;

    void Start()
    {
        GameObject input = GameObject.Find("Input and Game Logic");
        roller = input.GetComponent<RollHandler>();
        gameLogic = input.GetComponent<mouseInput>();
        gameLogic.setIsCasting(true);
    }

    // Manage changes in speed of icicle
    void Update()
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

        if (slide && !stop)
        {
            gameObject.transform.position -= new Vector3(0.0f, 0.3f, 0.0f);

            if (gameObject.transform.position.y < 0)
                stop = true;
        }
        else if (slide && stop)
        {
            if (stopCount > 0)
            {
                stopCount -= 1;
                return;
            }
            else
                gameObject.transform.position -= new Vector3(0.0f, 0.02f, 0.0f);
        }
        else
            gameObject.transform.position -= new Vector3(0.0f, 0.3f, 0.0f);

        if (gameObject.transform.position.y < -2 && enemy == null)
        {
            gameLogic.setIsCasting(false);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("OnEnter ran");

        if (slide || stop)
            return;

        Rigidbody rig = gameObject.GetComponent<Rigidbody>();
        rig.isKinematic = true;

		if (coll.collider.tag == "Player2" || coll.collider.tag == "Player")
		{
            enemy = coll.gameObject.GetComponent<casterScript>();
            roller.rollDice(3);
		}
        else if (coll.collider.tag == "Player2Minion" || coll.collider.tag == "PlayerMinion")
        {
            minion = coll.gameObject.GetComponent<minionScript>();
            roller.rollDice(3);
        }

        slide = true;
    }
}
