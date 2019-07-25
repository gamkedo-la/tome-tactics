// mouseInput.cs - Dominick Aiudi 2019
//
// Attached to empty object.
// Handles spells to cast from mouseInput.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellHandler : MonoBehaviour
{
	[Header("Spells")]
	public GameObject fireball;
	public GameObject icicle;
	public GameObject lightning;
    
    void Start() { }

    void Update() { }

    public void castFireball(GameObject selection, Vector3 target)
    {
    	selection.GetComponent<casterScript>().startCast();

		GameObject ball = Instantiate(fireball,
			selection.transform.position,
			Quaternion.identity);

		ball.GetComponent<toss>().setDestination(target);
    }

    public void castIcicle(GameObject selection, Vector3 target)
    {
		selection.GetComponent<casterScript>().startCast();

		GameObject ice = Instantiate(icicle,
				(target + (new Vector3(0.0f, 10.0f, 0.0f))),
				Quaternion.identity);

		ice.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
    }

    public void castLightning(GameObject selection, Vector3 target)
    {
		selection.GetComponent<casterScript>().startCast();
		
		Instantiate(lightning,
				(target + (new Vector3(0.0f, 10.0f, 0.0f))),
				Quaternion.identity);
    }
}
