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

    private GameObject[] buttons;

    void Start()
    {
    }

    void Update()
    {
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
            else if (spell.getName() == "Cone of Frost")
                newButton.GetComponentInChildren<Button>().onClick.AddListener(castConeOfFrost);
            else if (spell.getName() == "Cone of Shock")
                newButton.GetComponentInChildren<Button>().onClick.AddListener(castConeOfShock);

            newButton.transform.SetParent(UIpanel);
            newButton.transform.position = new Vector3(160.0f * numSpells, 0.0f, 0.0f);
            newButton.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);

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
        if (inputCS.SpellSelected)
            return;

		inputCS.state = 1;
		inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(8);
        inputCS.SpellSelected = true;
    }

    void castIcicle()
    {
        if (inputCS.SpellSelected)
            return;

        inputCS.state = 2;
        inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(4);
        inputCS.SpellSelected = true;
    }

    void castLightning()
    {
        if (inputCS.SpellSelected)
            return;

        inputCS.state = 3;
        inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(12);
        inputCS.SpellSelected = true;
    }

    void castConeOfFlame()
    {
        if (inputCS.SpellSelected)
            return;

        inputCS.state = 13;
        inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(3);
        inputCS.SpellSelected = true;
    }

    void castConeOfFrost()
    {
        if (inputCS.SpellSelected)
            return;

        inputCS.state = 23;
        inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(3);
        inputCS.SpellSelected = true;
    }

    void castConeOfShock()
    {
        if (inputCS.SpellSelected)
            return;

        inputCS.state = 33;
        inputCS.targetingOrb = Instantiate(caster).GetComponent<targeting>();
        inputCS.targetingOrb.setRange(3);
        inputCS.SpellSelected = true;
    }
}
