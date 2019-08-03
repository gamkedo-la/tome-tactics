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
	private Vector3 destination, origin;
	private SphereCollider hitBox;
	private float time = 0.0f;
    private GameObject caster = null;

	[SerializeField] private Vector3 curve;

    void Start()
    {
        origin = transform.position + new Vector3(0.0f, 2.0f, 0.0f);
        hitBox = GetComponent<SphereCollider>();
    }

    void Update()
    {
        transform.position = quadCurve(time);
        time += Time.deltaTime * 1.5f;

    	if (destination.y > transform.position.y)
        {
    		Destroy(gameObject);
            print("desination height higher than positon");
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
			casterScript sucker = coll.gameObject.GetComponent<casterScript>();
			if(sucker)
			{
				sucker.takeDamage(20);
				Debug.Log("Hit ennemi caster! Damage taken!");

			}
			
    		Destroy(gameObject, 2.0f); // This delay is because the animation is finished after this call.
    	}
    }
}
