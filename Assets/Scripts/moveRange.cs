using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveRange : MonoBehaviour
{
    public GameObject circ;

    // Create mesh, iterate through # of vertices assigning new values
    public void makeCircle(float scale, GameObject selection)
    {
    	int segCount = 64 * 2;
    	int verCount = segCount + 2;
    	int indCount = segCount * 3;

    	Mesh circle = new Mesh();
    	List<Vector3> vertices = new List<Vector3>(verCount);
    	int[] indices = new int[indCount];
    	float segWidth = Mathf.PI * 2.0f / segCount;
    	float angle = 0.0f;

    	vertices.Add(Vector3.zero);

    	for (int i = 1; i < verCount; ++i)
    	{
    		Vector3 newVect = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));
    		
            // Pass vector to checkPath
    		vertices.Add(checkPath(newVect, selection));
    		angle -= segWidth;

    		if (i > 1)
    		{
    			int j = (i - 2) * 3;

    			indices[j + 0] = 0;
    			indices[j + 1] = i - 1;
    			indices[j + 2] = i;
    		}
    	}

    	circle.SetVertices(vertices);
    	circle.SetIndices(indices, MeshTopology.Triangles, 0);
    	circle.RecalculateBounds();

    	circ.GetComponent<MeshFilter>().mesh = circle;

    	Vector3 newPos = selection.transform.position;
    	newPos.y = 0.1f;
    	circ.transform.position = newPos;
    }

    // Draw ray from center of circle that is length <scale> using NavMesh
	public Vector3 checkPath(Vector3 hitLoc, GameObject selection)
    {
		NavMeshHit hit2;
		float scale = (float)selection.GetComponent<casterScript>().getRange();

		Vector3 start = selection.transform.position;

		hitLoc *= scale;
		hitLoc += start;

		start.y = 0.1f;
		hitLoc.y = 0.1f;

		bool blocked = NavMesh.Raycast(start, hitLoc, out hit2, NavMesh.AllAreas);

		Debug.DrawLine(start, hitLoc, blocked ? Color.red : Color.green);

        // Return location of ray collision if a NavMesh edge is hit
		if (blocked)
		{
			Debug.DrawRay(hit2.position, Vector3.up, Color.red);
			return hit2.position - start;
		}
		else
			return hitLoc - start;
    }

    public void showRange(bool state) { circ.GetComponent<MeshRenderer>().enabled = state; }

    //////////////////
    // Spell ranges //
    //////////////////
    public void spellRange(GameObject selection, int scale)
    {
        int segCount = 64 * 2;
        int verCount = segCount + 2;
        int indCount = segCount * 3;

        Mesh circle = new Mesh();
        List<Vector3> vertices = new List<Vector3>(verCount);
        int[] indices = new int[indCount];
        float segWidth = Mathf.PI * 2.0f / segCount;
        float angle = 0.0f;

        vertices.Add(Vector3.zero);

        for (int i = 1; i < verCount; ++i)
        {
            Vector3 newVect = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));
            
            vertices.Add(newVect * scale); // Multiply by spell range
            angle -= segWidth;

            if (i > 1)
            {
                int j = (i - 2) * 3;

                indices[j + 0] = 0;
                indices[j + 1] = i - 1;
                indices[j + 2] = i;
            }
        }

        circle.SetVertices(vertices);
        circle.SetIndices(indices, MeshTopology.Triangles, 0);
        circle.RecalculateBounds();

        circ.GetComponent<MeshFilter>().mesh = circle;

        if (selection != null)
        {
            Vector3 newPos = selection.transform.position;
            newPos.y = 0.1f;
            circ.transform.position = newPos;
        }
    }

}
