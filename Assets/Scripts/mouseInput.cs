﻿// mouseInput.cs - Dominick Aiudi 2019
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
	[SerializeField] private GameObject minionPrefab;
	[SerializeField] private GameObject[] Objectives;

	private byte turn = 1;
	public bool skipRangeCalc = false;
    public targeting targetingOrb;
	private RaycastHit hit;
	MeshRenderer hoverIndicatorMesh;

    public bool SpellSelected { get; set; }

	// state checks so inputs are ignored when spells are casting
	// or when minions are moving
    private bool isCasting = false;
    private bool isMovingMinions = false;

    void Start() { }

    void Update()
    {
    	// Prevent range calculation from happening more than once after a spell is selected
        if (state > 0 && !skipRangeCalc)
    	{
			rangeCircle.GetComponent<moveRange>().spellRange(selection, targetingOrb.getRange());
			rangeCircle.GetComponent<moveRange>().showRange(true);
			skipRangeCalc = true;
    	}
		
		// Highlight hovered object
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

			// If spell is still active,
			// or if minions are being moved,
			// ignore input
			if (isCasting || isMovingMinions)
				return;
			
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
					case 1: // Fireball
						if (!spellHandle.castFireball(selection, hit.point, targetingOrb.getRange()))
							return;
						break;
					case 2: // Icicle
						if (!spellHandle.castIcicle(selection, hit.point, targetingOrb.getRange()))
							return;
						break;
					case 3: // Lightning
						if (!spellHandle.castLightning(selection, hit.point, targetingOrb.getRange()))
							return;
						break;
					case 13: // Cone of Flame
						spellHandle.castConeOfFlame(selection, hit.point);
						break;
					case 23: // Cone of Frost
						spellHandle.castConeOfFrost(selection, hit.point);
						break;
					case 33: // Cone of Shock
						spellHandle.castConeOfShock(selection, hit.point);
						break;
				}
				turn++; // Switch turns
				clearUI(); // Clear the UI and selection field
				targetingOrb.remove();
                SpellSelected = false;

				// Check if there are any minions to move
				// if so, move them!
				if (listMinions == null || listMinions.Length == 0 || listMinions[0])
					foreach (minionScript minion in listMinions)
						if (minion != null)
							minion.moveMinion();

				// Add minion to board
				minionScript[] minionListDuplicate = new minionScript[listMinions.Length + 1];

				for (int i = 0; i < listMinions.Length; ++i)
					minionListDuplicate[i] = listMinions[i];

				GameObject newMinon = Instantiate(minionPrefab,
					(new Vector3(0f, 0f, 0f)),
					Quaternion.identity);

				if (turn % 2 != 0)
				{
					newMinon.transform.position = new Vector3(5f, 1.1f, 5f);
					newMinon.tag = "Player2";
				}
				else
				{
					newMinon.transform.position = new Vector3(-5f, 1.1f, -5f);
					newMinon.tag = "Player";
				}

				newMinon.GetComponent<minionScript>().setOwner(turn);
				minionListDuplicate[minionListDuplicate.Length - 1] = newMinon.GetComponent<minionScript>();

				listMinions = minionListDuplicate;

				return;
			}
			////////////////////////////////
			// End of Spell Cast Sequence //
			////////////////////////////////


			// Debug.Log("Contact made");

			if (hit.collider.gameObject.tag != "Player" ||
                hit.collider.gameObject.tag != "Player2")
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
		if (Input.GetKeyUp("mouse 1") && state == 0)
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
			// clearUI();
			ShowPath.currPath = null;
		}
    }

    // Clear selection, state, and UI
    void clearUI()
    {
    	print("UI cleared");
    	rangeCircle.GetComponent<moveRange>().showRange(false);
		selection = null;
		state = 0;
		handle.deleteUI();
		debug.text = "Selection = none";
		skipRangeCalc = false;
		return;
    }

    // Set extra states
    public void setIsCasting(bool state) { isCasting = state; }

    public void setIsMovingMinions(bool state) { isMovingMinions = state; }
}
