// mouseInput.cs - Dominick Aiudi 2019
//
// Attached to empty object.
// Handles left & right mouse button presses
// and tracks selected Player GameObjects & spells.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mouseInput : MonoBehaviour
{
	public Camera cam;
	public ShowPath path;
	public GameObject fireball, icicle, lightning;
	public GameObject selection = null;
	public int state = 0;
	public UIHandler handle;

	[SerializeField] private Text debug;

	private byte turn = 1;

    void Start() { }

    void Update()
    {
    	///////////////////////
    	// Left Mouse Button //
    	///////////////////////
		if (Input.GetKeyUp("mouse 0"))
		{
			if (EventSystem.current.IsPointerOverGameObject()
				&& Input.mousePosition.y <= 75)
			{
				print("UI click");
				return; // Clicked on UI
			}

			RaycastHit hit;
			if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
			{
				clearUI(); // No contact
			}

			// Casting Fireball spell
			if (state == 1)
			{
				selection.GetComponent<casterScript>().castAnimation();

				GameObject ball = Instantiate(fireball, selection.transform.position, Quaternion.identity);
				ball.GetComponent<toss>().setDestination(hit.point);

				turn++;
				clearUI();
			}

			// Casting Icicle spell
			if (state == 2)
			{
				selection.GetComponent<casterScript>().castAnimation();

				GameObject ice = Instantiate(icicle,
						(hit.point + (new Vector3(0.0f, 10.0f, 0.0f))),
						Quaternion.identity);

				ice.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);

				turn++;
				clearUI();
			}

			// Casting Lightning spell
			if (state == 3)
			{
				selection.GetComponent<casterScript>().castAnimation();
				
				Instantiate(lightning,
						(hit.point + (new Vector3(0.0f, 10.0f, 0.0f))),
						Quaternion.identity);

				turn++;
				clearUI();
			}

			// Debug.Log("Contact made");

			if (hit.collider.gameObject.tag != "Player")
			{
				clearUI(); // Ignore non-Player objects
			}

			// Player turns
			// Odd = player 1
			if ((turn % 2 != 0) && (hit.collider.gameObject.tag != "Player"))
			{
				return;
			} // Even = player 2
			else if ((turn % 2 == 0) && (hit.collider.gameObject.tag != "Player2"))
			{
				return;
			}


			// If method hasn't returned yet, probably hit a Caster
			selection = hit.collider.gameObject;
			debug.text = "Selection = " + selection;
			handle.makeUI(selection.GetComponent<casterScript>().getSpells());

			float scale = (float)selection.GetComponent<casterScript>().getRange() * 2.0f;
			testCircle(scale);

			showRange(true);

			return;
		}

		////////////////////////
		// Right Mouse Button //
		////////////////////////
		if (Input.GetKeyUp("mouse 1"))
		{
			RaycastHit hit;
			if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
				return; // No contact

			if (hit.collider.gameObject == selection)
				return; // Clicked on selected object

			if (selection != null)
			{
				NavMeshAgent agent = selection.GetComponent<NavMeshAgent>();
				agent.speed = 0.0f;
				agent.destination = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
				showRange(false);
			}

			return;
		}

		// Update path as agent moves
		if (selection != null)
		{
			// testCircle((float)selection.GetComponent<casterScript>().getRange() * 2.0f);

			NavMeshAgent agent = selection.GetComponent<NavMeshAgent>();
			if (!agent.hasPath)
				return;

			if (agent.remainingDistance > 0)
			{
				if (agent.remainingDistance >= selection.GetComponent<casterScript>().getRange())
				{
					print("Out of range");
					agent.destination = agent.transform.position;
					return;
				}
				else
				{
					agent.speed = 3.5f;

					selection.GetComponent<casterScript>().startMove();
				}
			}

			// print(agent.remainingDistance);
			ShowPath.currPath = agent.path.corners;
		}
		else
			ShowPath.currPath = null;
    }

    // Clear selection, state, and UI
    void clearUI()
    {
    	showRange(false);
		selection = null;
		state = 0;
		handle.deleteUI();
		debug.text = "Selection = none";
		return;
    }

    //
    //
    // Put all this in a separate class later //
    //
    //
    public GameObject circ;
    void testCircle(float scale)
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
    		
    		vertices.Add(checkPath(newVect));
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

	Vector3 checkPath(Vector3 hitLoc)
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

		if (blocked)
		{
			Debug.DrawRay(hit2.position, Vector3.up, Color.red);
			return hit2.position - start;
		}
		else
			return hitLoc - start;
    }

    void showRange(bool state) { circ.GetComponent<MeshRenderer>().enabled = state; }

}
