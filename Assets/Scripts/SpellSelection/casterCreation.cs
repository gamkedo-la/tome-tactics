// casterCreation.cs - Dominick Aiudi 2019
//
// Handles UI of Caster Creation/Selection scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class casterCreation : MonoBehaviour
{
	[SerializeField] private spellContainer casterSpells;
	[SerializeField] private Button addRemoveSpell;

	public Canvas[] spellCanvas;

	private Canvas activeCanvas;
	public Text spellDesc;
	public Text spellBonus;
    public Image spellImg;

	private schoolHandler handle;
	public schoolHandler player;

	private AudioSource audioSource;
	[Header("SFX")]
	public AudioClip pageTurn;
	public AudioClip pageFlipping;

    void Start()
    {
    	GameObject audioObject = GameObject.Find("Master Audio");
    	if (audioObject)
			audioSource = audioObject.GetComponent<AudioSource>();    	

		switchTab(0);
    }

    void Update() { }

    void switchTab(int tabNum)
    {
    	if (audioSource)
	    	audioSource.PlayOneShot(pageTurn);

    	// If same tab is clicked, ignore
    	if (activeCanvas == spellCanvas[tabNum])
    		return;

    	// Make previous canvas "invisible"
    	if (activeCanvas)
	    	activeCanvas.gameObject.SetActive(false);

	    // Get new canvas based on tab clicked
    	activeCanvas = spellCanvas[tabNum];
    	activeCanvas.gameObject.SetActive(true);

    	// Get canvas handle
		handle = activeCanvas.GetComponent<schoolHandler>();
		handle.setElements(spellDesc, spellBonus, spellImg);

		// If player/selections tab, do stuff
		if (tabNum == 3)
		{
			// Make "add spell" button "remove spell"
			Transform transText = addRemoveSpell.transform.Find("Text");
			Text buttonText = transText.GetComponent<Text>();
			buttonText.text = "Remove Spell";

			// Assign appropriate listener for remove button
			addRemoveSpell.onClick.RemoveListener(addSpell);
			addRemoveSpell.onClick.AddListener(removeSpell);
		}
		else
		{
			// Reset "add spell" button
			Transform transText = addRemoveSpell.transform.Find("Text");
			Text buttonText = transText.GetComponent<Text>();
			buttonText.text = "Add Spell";

			// Re-assign lister for add button
			// Prevent duplicate listeners between tabs
			addRemoveSpell.onClick.RemoveListener(removeSpell);
			addRemoveSpell.onClick.RemoveListener(addSpell);
			addRemoveSpell.onClick.AddListener(addSpell);
		}
    }

    public void addSpell()
    {
    	if (handle.getActiveSpell() != "")
	    	player.setSpell(handle.getActiveSpell(), handle.getActiveDesc());
    }

    public void removeSpell()
    {
    	if (handle.getActiveSpell() != "")
	    	player.clearSpell(handle.getActiveSpell(), handle.getActiveDesc());
	}

	//////////////////////
	// Scene Navigation //
	//////////////////////
	public void startMatch()
	{
		// Check if spells selected
		if (player.isEmpty())
		{
			print("No spells selected!");
			return;
		}

		// Get spells
		string[] playerSpells = player.getPlayerSpells();

		// Pass spells into Caster Spells
		casterSpells.setSpells(1, playerSpells);

		// Prevent loss of Caster Spells object
		DontDestroyOnLoad(casterSpells);

		Destroy(GameObject.Find("Master Audio"));

    	if (audioSource)
			audioSource.PlayOneShot(pageFlipping);

		// Switch scenes
		SceneManager.LoadScene("SampleScene");
	}

    //////////////////////////
    // For the Buttons/Tabs //
    //////////////////////////
    public void shockTab() { switchTab(0); }

    public void fireTab() { switchTab(1); }

    public void iceTab() { switchTab(2); }

    public void playerTab() { switchTab(3); }

}
