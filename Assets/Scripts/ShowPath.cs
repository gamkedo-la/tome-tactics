// ShowPath.cs - Dominick Aiudi 2019
//
// Attached to a line renderer.
// Shows path selected GameObjects
// take through the NavMesh.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPath : MonoBehaviour
{
	public static Vector3[] currPath = new Vector3[0];

	private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (currPath != null && currPath.Length > 1)
        {
        	line.positionCount = currPath.Length;
        	for (int i = 0; i < currPath.Length; ++i)
        		line.SetPosition(i, currPath[i]);
        }
        else
            line.positionCount = 0;
    }
}
