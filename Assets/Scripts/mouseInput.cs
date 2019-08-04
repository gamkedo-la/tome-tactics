// mouseInput.cs - Dominick Aiudi 2019
//
// Attached to empty object.
// Handles left & right mouse button presses
// and tracks selected Player GameObjects & spells.
// Also handles game logic

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
	public GameObject selection = null;
	public int state = 0;
	public UIHandler handle;

	[SerializeField] private Text debug;
	[SerializeField] private GameObject rangeCircle;
	[SerializeField] private minionScript[] listMinions;
	[SerializeField] private spellHandler spellHandle;

	private byte turn = 1;
	private bool skipRangeCalc = false;
	public targeting targetingOrb;
	private RaycastHit hit;
	MeshRenderer hoverIndicatorMesh;

    void Start() { }

    void Update()
    {
    	if (state > 0 && !skipRangeCalc)
    	{
			rangeCircle.GetComponent<moveRange>().spellRange(selection, targetingOrb.getRange());
			rangeCircle.GetComponent<moveRange>().showRange(true);
			skipRangeCalc = true;
    	}
		
		bool isHit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit);
		if (isHit && hit.collider.GetComponent<casterScript>() != null)
		{			
			hoverIndicatorMesh = hit.collider.GetComponentsInChildren<MeshRenderer>()[1];
			hoverIndicatorMesh.enabled = true;
		}
		else if (hoverIndicatorMesh) 
		{
			hoverIndicatorMesh.enabled = false;
		}

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
			
			if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
			{
				clearUI(); // No contact
			}

			/////////////////////
			// Casting Spells! //
			/////////////////////
			if (state > 0)
			{
				switch (state)
				{
					case 1:
						if (!spellHandle.castFireball(selection, hit.point, targetingOrb.getRange()))
							return;
						break;
					case 2:
						if (!spellHandle.castIcicle(selection, hit.point, targetingOrb.getRange()))
							return;
						break;
					case 3:
						if (!spellHandle.castLightning(selection, hit.point, targetingOrb.getRange()))
							return;
						break;
				}
				turn++; // Switch turns
				clearUI(); // Clear the UI and selection field
				targetingOrb.remove();

				// Check if there are any minions to move
				// if so, move them!
				if (listMinions == null || listMinions.Length == 0 || listMinions[0])
					foreach (minionScript minion in listMinions)
						minion.moveMinion();
				return;
			}

			// Debug.Log("Contact made");

			if (hit.collider.gameObject.tag != "Player")
			{
				clearUI(); // Ignore non-Player objects
			}

			///////////////////////////////////////////////////////////////
			// Player turns, prevent selection of other player's objects //
			///////////////////////////////////////////////////////////////
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
			rangeCircle.GetComponent<moveRange>().makeCircle(scale, selection);

			rangeCircle.GetComponent<moveRange>().showRange(true);

			return;
		}

		////////////////////////
		// Right Mouse Button //
		////////////////////////
		if (Input.GetKeyUp("mouse 1"))
		{			
			if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
				return; // No contact

			if (hit.collider.gameObject == selection)
				return; // Clicked on selected object

			if (selection != null)
			{
				NavMeshAgent agent = selection.GetComponent<NavMeshAgent>();
				agent.speed = 0.0f;
				agent.destination = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
				rangeCircle.GetComponent<moveRange>().showRange(false);
			}

			return;
		}

		////////////////////////////////
		// Update path as agent moves //
		// Handle out-of-range moves  //
		////////////////////////////////
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
		{
			clearUI();
			ShowPath.currPath = null;
		}
    }

    // Clear selection, state, and UI
    void clearUI()
    {
    	rangeCircle.GetComponent<moveRange>().showRange(false);
		selection = null;
		state = 0;
		handle.deleteUI();
		debug.text = "Selection = none";
		skipRangeCalc = false;
		return;
    }
}
