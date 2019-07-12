// casterCreation.cs - Dominick Aiudi 2019
//
// Handles UI of Caster Creation/Selection scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class casterCreation : MonoBehaviour
{
	public Canvas[] spellCanvas;

	private Canvas activeCanvas;
	public Text spellDesc;
	public Text spellBonus;
	// public Image spellImg; (to be used later)

	private schoolHandler handle;
	public schoolHandler player;

    void Start()
    {
		switchTab(0);
    }

    void Update() { }

    void switchTab(int tabNum)
    {
    	if (activeCanvas == spellCanvas[tabNum])
    		return;

    	if (activeCanvas)
	    	activeCanvas.gameObject.SetActive(false);

    	activeCanvas = spellCanvas[tabNum];
    	activeCanvas.gameObject.SetActive(true);

		handle = activeCanvas.GetComponent<schoolHandler>();

		handle.setElements(spellDesc, spellBonus);
    }

    public void addSpell()
    {
    	if (handle.getActiveSpell() != "")
	    	player.setSpell(handle.getActiveSpell(), handle.getActiveDesc());
    }

    //////////////////////////
    // For the Buttons/Tabs //
    //////////////////////////
    public void shockTab() { switchTab(0); }

    public void fireTab() { switchTab(1); }

    public void iceTab() { switchTab(2); }

    public void playerTab() { switchTab(3); }

}
