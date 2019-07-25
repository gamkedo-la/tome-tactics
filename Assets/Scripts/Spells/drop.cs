using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour
{
    bool slide = false, stop = false;
    int stopCount = 30;

    void Start() { }

    // Manage changes in speed of icicle
    void Update()
    {
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

        if (gameObject.transform.position.y < -2)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        // Debug.Log("OnEnter ran");

        if (slide || stop)
            return;

    	if (coll.collider.tag != "Player")
    	{
    		if (coll.collider.tag == "Opponent")
    		{
                coll.gameObject.GetComponent<casterScript>().takeDamage(20);
                
                Rigidbody rig = gameObject.GetComponent<Rigidbody>();
                rig.isKinematic = true;

                slide = true;
    		}
    	}
    }
}
