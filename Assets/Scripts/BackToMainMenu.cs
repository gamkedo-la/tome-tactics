using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
	private GameObject audioObject;
	private AudioSource source;
	public AudioClip bookClose;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => { 
        	
        	GameObject audioObject = GameObject.Find("Master Audio");
    	
	    	if (audioObject != null)
				source = audioObject.GetComponent<AudioSource>();

			source.PlayOneShot(bookClose);

        	SceneManager.LoadScene(1); 
        	});
    }
}
