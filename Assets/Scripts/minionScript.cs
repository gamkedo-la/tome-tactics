// minionScript.cs - Dominick Aiudi 2019
//
// Attached to minions, handles their logic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minionScript : MonoBehaviour
{
	[SerializeField] private GameObject target;
    private NavMeshAgent agent;
    private float distStart = 0.0f, distLeft = 0.0f;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 0.0f;
    }

    void Update()
    {
    	if (distStart == 0.0f)
    	{
    		distStart = agent.remainingDistance;
    		return;
    	}
    	else
	    	distLeft = distStart - agent.remainingDistance;

        if (distLeft > 3.0f)
        {
        	agent.speed = 0.0f;
        	distStart = 0.0f;
        }
    }

    public void moveMinion()
    {
    	agent.destination = target.transform.position;

    	distStart = agent.remainingDistance;

    	agent.speed = 3.5f;
    }
}
