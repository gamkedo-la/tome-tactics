// casterScript.cs - Dominick Aiudi 2019
//
// Attached to "Casters" in the game.
// Contains information regarding the casters,
// including spells, health, etc.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class casterScript : MonoBehaviour
{
	[SerializeField] private int hp, range = 0;
	[SerializeField] private Spell[] spellBook;
    private Animator anim;
	public Transform cam;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update() { }

    public void takeDamage(int damage) { hp -= damage; }

    public Spell[] getSpells() { return spellBook; }

    public int getRange() { return range; }

    public void castAnimation() { anim.Play("basicCast 0", 0, 0.0f); }
}
