// Spell.cs - Dominick Aiudi 2019
//
// Script to hold spells during spell selection.
// Attached to empty object and read from when matches start.
// (might add match parameters later on)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellContainer : MonoBehaviour
{
    [SerializeField] private Spell[] casterSpells1;
    [SerializeField] private Spell[] casterSpells2;

    public void setSpells(int playerNum, string[] newSpells)
    {
    	if (playerNum == 1)
    	{
    		casterSpells1 = new Spell[newSpells.Length];

    		for(int i = 0; i < newSpells.Length; ++i)
    		{
    			casterSpells1[i] = new Spell();
    			casterSpells1[i].setName(newSpells[i]);
    		}
    	}
    	else
    	{
    		casterSpells2 = new Spell[newSpells.Length];

    		for(int i = 0; i < newSpells.Length; ++i)
    			casterSpells2[i].setName(newSpells[i]);
    	}
    }

    public Spell[] getSpells(int playerNum)
    {
    	if (playerNum == 1)
    		return casterSpells1;
    	else
    		return casterSpells2;
    }
}
