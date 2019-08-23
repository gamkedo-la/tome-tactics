// Spell.cs - Dominick Aiudi 2019
//
// Data Structure for spells.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MySpell", menuName = "ScriptableObjects/Spell", order = 1)]
public class Spell : ScriptableObject
{
    // Member data
    [SerializeField] private string spellName = "";
    [SerializeField] private int damage = 0;
    [SerializeField] private Color UIColor = Color.white;
    [SerializeField] private int range = 0; // Raius from caster
    [SerializeField] private Sprite UISprite;
    [SerializeField] private int power = 0;
    [SerializeField] private int accuracy = 0;
    [SerializeField] private string effect = ""; // unsure if string is best to use
    [SerializeField] private string description;

    // Setters
    public void setName(string newName) { spellName = newName; }
    public void setDamage(int newDamage) { damage = newDamage; }
    public void setColor(Color newColor) { UIColor = newColor; }
    public void setRange(int newRange) { range = newRange; }
    public void setUISprite(Sprite newSprite) { UISprite = newSprite; }
    public void setDescription(string desc) { description = desc; }

    // Getters
    public string getName() { return spellName; }
    public int getDamage() { return damage; }
    public Color getColor() { return UIColor; }
    public int getRange() { return range; }
    public Sprite getUISprite() { return UISprite; }
    public string getDescription() { return description; }

}
