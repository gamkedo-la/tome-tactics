// Spell.cs - Dominick Aiudi 2019
//
// Data Structure for spells.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell : System.Object
{
	// Member data
	[SerializeField] private string spellName = "";
	[SerializeField] private int damage = 0;
	[SerializeField] private Color UIColor = Color.white;
	[SerializeField] private int range = 0;

	// Setters
	public void setName(string newName) { spellName = newName; }
	public void setDamage(int newDamage) { damage = newDamage; }
	public void setColor(Color newColor) { UIColor = newColor; }
	public void setRange(int newRange) { range = newRange; }

	// Getters
	public string getName() { return spellName; }
	public int getDamage() { return damage; }
	public Color getColor() { return UIColor; }
	public int getRange() { return range; }

}
