// mainMenu.cs - Dominick Aiudi 2019
//
// Assigns functions to buttons in Main Menu scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
	[SerializeField] private GameObject audioController;

	[Header("Buttons")]
	public Button create;
	public Button sample;

	[Header("Audio")]
	public AudioSource source;
	public AudioClip pageFlipping;

    void Start()
    {
        create.onClick.AddListener(onCreateCaster);
        sample.onClick.AddListener(onSample);
    }

    void onCreateCaster()
    {
    	DontDestroyOnLoad(audioController);
    	source.PlayOneShot(pageFlipping);
    	SceneManager.LoadScene("spellSelection");
    }

    void onSample()
    {
    	source.PlayOneShot(pageFlipping);
    	SceneManager.LoadScene("SampleScene");
    }
}
