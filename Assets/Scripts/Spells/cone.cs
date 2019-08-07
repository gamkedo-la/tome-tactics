using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cone : MonoBehaviour
{
	private SphereCollider collSphere;
    private ParticleSystem system;
    public float sphereMoveRate = 0.0f;
    public float sphereGrowRate = 0.0f;

    void Start()
    {
        collSphere = GetComponent<SphereCollider>();
        system = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!system.IsAlive()) { Destroy(gameObject); }

        collSphere.radius = system.time * sphereGrowRate;
        collSphere.center += transform.forward * sphereMoveRate;
    }
}
