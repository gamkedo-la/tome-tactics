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
    [SerializeField] private mouseInput gameLogic;
    public Animator anim, animMove;
    private NavMeshAgent agent;

	public Transform cam;

    public GameObject transferSpells;
    public GameObject opponent;

    public bool isRobot = false;

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

        if (isRobot)
        {
            float distance = 0.0f;
            Vector3[] corners = agent.path.corners;
            for (int c = 0; c < corners.Length - 1; ++c)
            {
                // print(corners[c]);
                distance += Mathf.Abs((corners[c] - corners[c + 1]).magnitude);
            }

            if (distance == 0)
                return;

            if (stopAdjustAllowed)
            {
                // print(gameObject + " distance: " + distance);
                agent.stoppingDistance = distance - 3.0f;
                agent.speed = 3.5f;
                // print(agent.stoppingDistance + " from Infinity");
                stopAdjustAllowed = false;
            }
            else if (distance <= agent.stoppingDistance)
            {
                agent.destination = transform.position;
                gameLogic.robotCast(spellBook[spellNum]);
            }
        }
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

    ////////
    // AI //
    ////////
    private int spellNum;
    private bool stopAdjustAllowed = false;
    
    public void pickSpell()
    {
        spellNum = Random.Range(1, spellBook.Length);
        checkRange();
    }

    private void checkRange()
    {
        float distance = Vector3.Distance(gameObject.transform.position, opponent.transform.position);
        // print(distance + " vs " + spellBook[spellNum].getRange());

        if (distance >= spellBook[spellNum].getRange())
            moveCaster();
        else
            gameLogic.robotCast(spellBook[spellNum]);
    }

    private void moveCaster()
    {
        agent.destination = opponent.transform.position;
        stopAdjustAllowed = true;
    }

}
