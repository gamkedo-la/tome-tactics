// casterScript.cs - Dominick Aiudi 2019
//
// Attached to "Casters" in the game.
// Contains information regarding the casters,
// including spells, health, etc.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class casterScript : MonoBehaviour
{
	[SerializeField] private int hp, range = 0;
	[SerializeField] private Spell[] spellBook;
    public Animator anim, animMove;
    private NavMeshAgent agent;

	public Transform cam;

    public GameObject transferSpells;

    void Start()
    {
        transferSpells = GameObject.Find("Caster Spells");
        if (this.tag == "Player" && transferSpells != null)
            spellBook = transferSpells.GetComponent<spellContainer>().getSpells(1);
        else if (this.tag == "Player2" && transferSpells != null)
            spellBook = transferSpells.GetComponent<spellContainer>().getSpells(2);

        // anim = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (animMove.GetBool("move") && !agent.hasPath)
            stopMove();
    }

    public void takeDamage(int damage)
    {
        print("Damage: " + damage);
        hp -= damage;
    }

    public Spell[] getSpells() { return spellBook; }

    public int getRange() { return range; }

    public void startCast()
    {
        agent.enabled = false;
        gameObject.transform.parent = anim.transform;
        anim.SetBool("cast", true);
    }

    public void stopCast()
    {
        anim.SetBool("cast", false);
        anim.transform.DetachChildren();
        agent.enabled = true;
    }

    public void startMove()
    {
        gameObject.transform.parent = animMove.transform;
        animMove.SetBool("move", true);
    }

    private void stopMove()
    {
        animMove.SetBool("move", false);
        animMove.transform.DetachChildren();
    }

}
