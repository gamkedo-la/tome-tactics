using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonMaker : MonoBehaviour
{
    void Start() { }

    void Update() { }

    public void setColor(Color newColor) { GetComponent<Image>().color = newColor; }

    public void setText(string newText) { GetComponentInChildren<Text>().text = newText; }
}
