﻿using UnityEngine;
using UnityEngine.UI;

namespace DemoAV.StartMenu.Keyboard{
public class Backspace : MonoBehaviour {
	Button button;
    Color normalColor, highlightedColor;
	ColorBlock colors;
	InputName input;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();

		// Delay the collider re-dimensioning to allow the layout script to set the correct size of button
		// after a frame.
		Invoke("SetCollider", 0.2f);

		input = GameObject.Find("Canvas/Create Room Tab/Panel/Room Name").GetComponent<InputName>();
		colors = button.colors;
		normalColor = button.colors.normalColor;
		highlightedColor = button.colors.highlightedColor;
	}
	
	/// <summary>
	/// 	Sets the correct collider size.
	/// </summary>
	void SetCollider(){
		BoxCollider collider = gameObject.AddComponent<BoxCollider>();
		collider.size = GetComponent<RectTransform>().sizeDelta;
	}

    // Called on entering collision with pad.
	void OnTriggerEnter(Collider collider) {
		colors.normalColor = highlightedColor;
		button.colors = colors;
		input.RemoveLastCharacter();
	}

	// Called on exiting collision with pad.
	void OnTriggerExit(Collider collider) {
		colors.normalColor = normalColor;
		button.colors = colors;
	}
}
}