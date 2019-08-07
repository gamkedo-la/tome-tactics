// UIHandler.cs - Dominick Aiudi 2019
//
// Attached to UI object.
// Handles creation of targeting items
// based on chosen spells.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
	public mouseInput inputCS;
	public GameObject caster, die;
    public Transform UIpanel;
	[Header("Buttons")]
	public GameObject buttonTemplate;
	public Button dice;

	private GameObject dieInst;
	private Rigidbody rig;
    private GameObject[] buttons;

    void Start()
    {
        dice.onClick.AddListener(rollDice);
    }

    void Update()
    {
    	if (rig != null)
    	{
	    	if (rig.IsSleeping())
	    	{
	    		getResult();
	    		rig = null;
	    		dieInst = null;
	    	}
    	}
    }

    // Takes the Spellbook of a Caster and creates
    // buttons for the spells
    public void makeUI(Spell[] spellbook)
    {
        int numSpells = 0;
        buttons = new GameObject[spellbook.Length];

        foreach (Spell spell in spellbook)
        {
            GameObject newButton = Instantiate(buttonTemplate);

            newButton.GetComponent<buttonMaker>().setColor(spell.getColor());
            newButton.GetComponent<buttonMaker>().setText(spell.getName());

            // Make buttons
            if (spell.getName() == "Fireball")
                newButton.GetComponentInChildren<Button>().onClick.AddListener(castFireball);
            else if (spell.getName() == "Icicle")
                newButton.GetComponentInChildren<Button>().onClick.AddListener(castIcicle);
            else if (spell.getName() == "Lightning")
                newButton.GetComponentInChildren<Button>().onClick.AddListener(castLightning);
            else if (spell.getName() == "Cone of Flame")
                newButton.GetComponentInChildren<Button>().onClick.AddListener(castConeOfFlame);

            newButton.transform.SetParent(UIpanel);
            newButton.transform.position = new Vector3(160.0f * numSpells, 0.0f, 0.0f);

            buttons[numSpells] = newButton;
            numSpells += 1;
        }
    }

    public void deleteUI()
    {
        if (buttons == null || buttons.Length == 0)
            return;

        foreach (GameObject button in buttons)
            Destroy(button);
    }

    void castFireball()
    {
		inputCS.state = 1;
		inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(8);
    }

    void castIcicle()
    {
        inputCS.state = 2;
        inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(4);
    }

    void castLightning()
    {
        inputCS.state = 3;
        inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(12);
    }

    void castConeOfFlame()
    {
        inputCS.state = 13;
        inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(3);
    }

    ////////////////////////////////////////////////////////////////
    //                                                            //
    // Already in another script, it just needs to be implemented //
    //                                                            //
    //                                                            //
    void rollDice()
    {
    	Vector3 pos = new Vector3(5.0f, 7.5f, 5.0f);
    	dieInst = Instantiate(die, pos, Random.rotation);
    	rig = dieInst.GetComponent<Rigidbody>();

    	rig.AddForce(-5.0f, 0.0f, -5.0f, ForceMode.Impulse);
    	rig.AddTorque(Random.Range(30.0f, 40.0f), 
    				Random.Range(30.0f, 40.0f),
    				Random.Range(30.0f, 40.0f));
    }

    void getResult()
    {
    	int diceCount = 0;

    	// Debug.Log(Vector3.Dot (dieInst.transform.forward, Vector3.up));
    	// Debug.Log(Vector3.Dot (dieInst.transform.up, Vector3.up));
    	// Debug.Log(Vector3.Dot (dieInst.transform.right, Vector3.up));

    	if (Vector3.Dot (dieInst.transform.forward, Vector3.up) > 0.9)
			diceCount = 4; // 5
		else if (Vector3.Dot (-dieInst.transform.forward, Vector3.up) > 0.9)
			diceCount = 5; // 2
		else if (Vector3.Dot (dieInst.transform.up, Vector3.up) > 0.9)
			diceCount = 3; // 3
		else if (Vector3.Dot (-dieInst.transform.up, Vector3.up) > 0.9)
			diceCount = 1; // 4
		else if (Vector3.Dot (dieInst.transform.right, Vector3.up) > 0.9)
			diceCount = 6; // 6
		else if (Vector3.Dot (-dieInst.transform.right, Vector3.up) > 0.9)
			diceCount = 2; // 1

		Debug.Log ("diceCount :" + diceCount);
    }
}
