// schoolHandler.cs - Dominick Aiudi 2019
//
// Handles buttons under the tabs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class schoolHandler : MonoBehaviour
{
	private string activeSpell = "";
	private string activeDesc = "";
	private Text descriptor;
	// private Image image;

	public string school = "";
	public string spell_1 = "";
	public string spell_2 = "";

	public void firstSpell()
	{
		descriptor.text = spell_1;

		activeSpell = EventSystem.current.currentSelectedGameObject.name;
		activeDesc = spell_1;
	}

	public void secondSpell()
	{
		descriptor.text = spell_2;

		activeSpell = EventSystem.current.currentSelectedGameObject.name;
		activeDesc = spell_2;
	}

	public void setElements(Text textDesc, Text textBonus)
	{
		descriptor = textDesc;

		textBonus.text = school + " Bonuses";

		activeSpell = "";
	}

	public string getActiveSpell() { return activeSpell; }

	public string getActiveDesc() { return activeDesc; }

	////////////////////////////////////
	// Only used by player tab/canvas //
	////////////////////////////////////
	public void setSpell(string name, string description)
	{
		if (spell_1 == "")
		{
			Transform t = transform.Find("Spell List Canvas/Spells/Spell First/Text");
			Text tex = t.gameObject.GetComponent<Text>();
			tex.text = name;

			spell_1 = description;
		}
		else if (spell_2 == "")
		{
			Transform t = transform.Find("Spell List Canvas/Spells/Spell Second/Text");
			Text tex = t.gameObject.GetComponent<Text>();
			tex.text = name;

			spell_2 = description;
		}
		else
		{
			print("All full!");
		}
	}

	public void clearSpell(string name, string description)
	{
		if (spell_1 == description)
		{
			Transform t = transform.Find("Spell List Canvas/Spells/Spell First/Text");
			Text tex = t.gameObject.GetComponent<Text>();
			tex.text = "Empty!";

			spell_1 = "";
		}
		else if (spell_2 == description)
		{
			Transform t = transform.Find("Spell List Canvas/Spells/Spell Second/Text");
			Text tex = t.gameObject.GetComponent<Text>();
			tex.text = "Empty!";

			spell_2 = "";
		}
		else
		{
			print("Probably empty?");
		}
	}

	public bool isEmpty()
	{
		if (spell_1 == "" && spell_2 == "")
			return true;
		else
			return false;
	}

	public string[] getPlayerSpells()
	{
		byte size = 0;
		if (spell_1 != "")
			size++;
		if (spell_2 != "")
			size++;

		string[] spells = new string[size];

		// First Spell
		Transform t = transform.Find("Spell List Canvas/Spells/Spell First/Text");
		Text tex = t.gameObject.GetComponent<Text>();
		spells[0] = tex.text;

		// Second Spell
		if (size > 1)
		{
			t = transform.Find("Spell List Canvas/Spells/Spell Second/Text");
			tex = t.gameObject.GetComponent<Text>();
			spells[1] = tex.text;
		}

		// foreach (string s in spells)
		// 	print(s);
		return spells;
	}
}
