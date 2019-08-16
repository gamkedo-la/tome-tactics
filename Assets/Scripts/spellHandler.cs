// mouseInput.cs - Dominick Aiudi 2019
//
// Attached to empty object.
// Handles spells to cast from mouseInput.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellHandler : MonoBehaviour
{
    [SerializeField] private RollHandler roller;

	[Header("Spells")]
	public GameObject fireball;
	public GameObject icicle;
	public GameObject lightning;
	public GameObject coneOfFlame;
	public GameObject coneOfFrost;
	public GameObject coneOfShock;
    
    private Spell currentSpell;

    void Start() { }

    void Update() { }

    public bool castFireball(GameObject selection, Vector3 target, int range)
    {
        roller.rollDice(6);

		if (Vector3.Distance(target, selection.transform.position) > range)
		{
			print("Out of range Spell");
			return false;
		}

    	selection.GetComponent<casterScript>().startCast();

		GameObject ball = Instantiate(fireball,
			selection.transform.position,
			Quaternion.identity);

        var toss_ball = ball.GetComponent<toss>();
        toss_ball.setDestination(target);
        toss_ball.setCaster(selection);

        return true;
    }

    public bool castIcicle(GameObject selection, Vector3 target, int range)
    {
		if (Vector3.Distance(target, selection.transform.position) > range)
		{
			print("Out of range Spell");
			return false;
		}

		selection.GetComponent<casterScript>().startCast();

		GameObject ice = Instantiate(icicle,
				(target + (new Vector3(0.0f, 10.0f, 0.0f))),
				Quaternion.identity);

		ice.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
		return true;
    }

    public bool castLightning(GameObject selection, Vector3 target, int range)
    {
		if (Vector3.Distance(target, selection.transform.position) > range)
		{
			print("Out of range Spell");
			return false;
		}

		selection.GetComponent<casterScript>().startCast();
		
		Instantiate(lightning,
				(target + (new Vector3(0.0f, 10.0f, 0.0f))),
				Quaternion.identity);
		return true;
    }

    public void castConeOfFlame(GameObject selection, Vector3 target)
    {
    	selection.GetComponent<casterScript>().startCast();

    	GameObject coneInst = Instantiate(coneOfFlame, selection.transform.position, Quaternion.identity);

    	coneInst.transform.rotation = Quaternion.LookRotation(
    									Vector3.RotateTowards(coneInst.transform.forward,
    														target - coneInst.transform.position,
    														3.0f, 0.0f));
    }

    public void castConeOfFrost(GameObject selection, Vector3 target)
    {
    	selection.GetComponent<casterScript>().startCast();

    	GameObject coneInst = Instantiate(coneOfFrost, selection.transform.position, Quaternion.identity);

    	coneInst.transform.rotation = Quaternion.LookRotation(
    									Vector3.RotateTowards(coneInst.transform.forward,
    														target - coneInst.transform.position,
    														3.0f, 0.0f));
    }

    public void castConeOfShock(GameObject selection, Vector3 target)
    {
    	selection.GetComponent<casterScript>().startCast();

    	GameObject coneInst = Instantiate(coneOfShock, selection.transform.position, Quaternion.identity);

    	coneInst.transform.rotation = Quaternion.LookRotation(
    									Vector3.RotateTowards(coneInst.transform.forward,
    														target - coneInst.transform.position,
    														3.0f, 0.0f));
    }
}
