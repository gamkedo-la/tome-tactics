using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cone : MonoBehaviour
{
    private RollHandler roller;
    private mouseInput gameLogic;
	private SphereCollider collSphere;
    private ParticleSystem system;
    public float sphereMoveRate = 0.0f;
    public float sphereGrowRate = 0.0f;

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
        if (!system.IsAlive())
        {
            gameLogic.setIsCasting(false);
            Destroy(gameObject);
        }

        collSphere.radius = system.time * sphereGrowRate;
        collSphere.center += transform.forward * sphereMoveRate;
    }
}
