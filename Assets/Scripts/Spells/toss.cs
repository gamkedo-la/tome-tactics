// toss.cs - Dominick Aiudi 2019
//
// Attached to Fireball prefab.
// Handles movement of Fireball object, moving
// it in an arc to a specific location.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toss : MonoBehaviour
{
    private RollHandler roller;
    private mouseInput gameLogic;
	private Vector3 destination, origin;
	private SphereCollider hitBox;
	private float time = 0.0f;
    private GameObject caster = null;
    private casterScript enemy = null;
    private minionScript minion = null;
    private bool moving = true;

	[SerializeField] private Vector3 curve;

    void Start()
    {
        GameObject input = GameObject.Find("Input and Game Logic");
        roller = input.GetComponent<RollHandler>();
        gameLogic = input.GetComponent<mouseInput>();
        gameLogic.setIsCasting(true);

        origin = transform.position + new Vector3(0.0f, 2.0f, 0.0f);
        hitBox = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (moving)
        {
            transform.position = quadCurve(time);
            time += Time.deltaTime * 1.5f;

        	if (destination.y > transform.position.y + 10)
            {
                gameLogic.setIsCasting(false);
        		Destroy(gameObject);
                print("desination height higher than positon");
            }
        }

        int dam = roller.isDone();
        if (dam != 0)
        {
            if (enemy)
                enemy.takeDamage(dam);
            else if (minion)
                minion.takeDamage(dam);

            roller.Reset();
            gameLogic.setIsCasting(false);
            Destroy(gameObject, 2.0f); // This delay is because the animation is finished after this call.
        }
    }

    public void setCaster(GameObject source) { caster = source; }

    public void setDestination(Vector3 newDest)
    {
    	destination = newDest;
    	setCurve();
    	// Debug.Log(destination);
    }

    // Mix & Jam: Kratos' Axe
    private Vector3 quadCurve(float t)
    {
    	float u = 1 - t;
    	float tt = t * t;
    	float uu = u * u;

    	return (uu * origin) + (2 * u * t * curve) + (tt * destination);
    }

    // Prevent odd travel arcs
    private void setCurve()
    {
    	float differenceX = origin.x - destination.x;
    	float differenceZ = origin.z - destination.z;

    	// print(differenceX);
    	// print(differenceZ);

    	curve.x = -1 * differenceX;
    	curve.z = -1 * differenceZ;
    }

    void OnCollisionEnter(Collision coll)
    {
    	Debug.Log("OnCollisionEnter ran");

    	if (coll.collider.gameObject != caster)
    	{
			Debug.Log("Hit something...");
			enemy = coll.gameObject.GetComponent<casterScript>();
			if(enemy)
			{
                moving = false;
                transform.position = new Vector3(0.0f, 100.0f, 0.0f);
                roller.rollDice(6);
				Debug.Log("Hit enemy caster! Damage taken!");
			}
            else if (minion = coll.gameObject.GetComponent<minionScript>())
            {
                moving = false;
                transform.position = new Vector3(0.0f, 100.0f, 0.0f);
                roller.rollDice(6);
                Debug.Log("Hit enemy minion! Damage taken!");
            }

    	}
    }
}
