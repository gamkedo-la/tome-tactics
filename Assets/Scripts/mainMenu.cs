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
	[Header("Buttons")]
	public Button create;
	public Button sample;

    void Start()
    {
        create.onClick.AddListener(onCreateCaster);
        sample.onClick.AddListener(onSample);
    }

    void onCreateCaster()
    {
    	SceneManager.LoadScene("spellSelection");
    }

    void onSample()
    {
    	SceneManager.LoadScene("SampleScene");
    }
}
