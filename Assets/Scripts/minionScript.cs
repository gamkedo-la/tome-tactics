﻿// minionScript.cs - Dominick Aiudi 2019
//
// Attached to minions, handles their logic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minionScript : MonoBehaviour
{
    [SerializeField] private Animator moveAnimator;
	[SerializeField] private GameObject target = null;
    private NavMeshAgent agent;
    private bool stopAdjustAllowed = false;
    private int ownerCaster = 0;
    private int hp = 20;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 0.0f;
    }

    void Update()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

    	if (agent.remainingDistance != Mathf.Infinity && agent.remainingDistance > 0f)
    	{
            if (stopAdjustAllowed)
            {
                agent.stoppingDistance = agent.remainingDistance - 3.0f;
                agent.speed = 3.5f;
                print(agent.stoppingDistance);
                stopAdjustAllowed = false;
            }
            return;
    	}
        else
        {
            float distance = 0.0f;
            Vector3[] corners = agent.path.corners;
            for (int c = 0; c < corners.Length - 1; ++c)
            {
                // print(corners[c]);
                distance += Mathf.Abs((corners[c] - corners[c + 1]).magnitude);
            }

            if (distance == 0)
                return;

            if (stopAdjustAllowed)
            {
                // print(gameObject + " distance: " + distance);
                agent.stoppingDistance = distance - 3.0f;
                agent.speed = 3.5f;
                // print(agent.stoppingDistance + " from Infinity");
                stopAdjustAllowed = false;
            }
            else if (distance <= agent.stoppingDistance)
            {
                agent.destination = transform.position;
            }
        }
    }

    public void takeDamage(int damage)
    {
        print("Damage: " + damage);
        hp -= damage;
    }

    public void moveMinion()
    {
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance <= 5f)
            return;

    	agent.destination = target.transform.position;;

        stopAdjustAllowed = true;
    }

    public void setOwner(int turnNum)
    {
        if ((turnNum % 2) != 0 )
            ownerCaster = 1;
        else
            ownerCaster = 2;
    }

    public int getOwner() { return ownerCaster; }

    public void setTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public bool hasTarget()
    {
        if (target == null)
            return false;
        else
            return true;
    }

    public Vector3 getTargetPos()
    {
        return target.transform.position;
    }

    public Vector3 getPos()
    {
        return transform.position;
    }
}
