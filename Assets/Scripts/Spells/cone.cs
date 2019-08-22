using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cone : MonoBehaviour
{
    private RollHandler roller;
    private mouseInput gameLogic;
	private SphereCollider collSphere;
    private ParticleSystem system;
    private casterScript enemy = null;
    public float sphereMoveRate = 0.0f;
    public float sphereGrowRate = 0.0f;
    private int travelTime = 0;

    void Start()
    {
        collSphere = GetComponent<SphereCollider>();
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
                enemy.takeDamage(dam);
                roller.Reset();
                gameLogic.setIsCasting(false);
                Destroy(gameObject);
            }
        }

        collSphere.radius = system.time * sphereGrowRate;
        if (travelTime < 60)
        {
            collSphere.center += transform.forward * sphereMoveRate;
            travelTime++;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Player2" || coll.collider.tag == "Player")
        {
            roller.rollDice(3);
            enemy = coll.gameObject.GetComponent<casterScript>();
        }
    }
}
