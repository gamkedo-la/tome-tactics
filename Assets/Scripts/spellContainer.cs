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
}
