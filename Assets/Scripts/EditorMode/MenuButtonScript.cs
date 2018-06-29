﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoAV.Editor.User;

/* 
	Script to handle the callbacks for the user menu: 
		Save
		Load
		Exit
*/
public class MenuButtonScript : MonoBehaviour {

	private Color32 defaultColor;
	
	void Start () {
		defaultColor = GetComponent<Image>().color;
		SelectMenuItem.menuSelect += OnPointerEnter;
		SelectMenuItem.menuDeselect += OnPointerExit;
		SelectMenuItem.menuPress += Press;
	}

	public void Press(GameObject obj) {
		if(obj != null && GameObject.ReferenceEquals(obj, this.gameObject))
			GetComponent<Image>().color = new Color32(224, 0, 0, 77); // EUis	
//			buttonAction(this.name);
	}

	public void OnPointerEnter(GameObject obj) {
		if(obj != null && GameObject.ReferenceEquals(obj, this.gameObject))
			GetComponent<Image>().color = new Color32(224, 243, 74, 77); // giallino		
	}

	public void OnPointerExit() {
		GetComponent<Image>().color = defaultColor;
	}

	private void buttonAction(string name) {

		switch (name)
		{
			case "3DBtn1":
				SceneController.Dictionary.Save();
				Debug.Log("ROOM SAVED");
				// TODO: Add Confirmation yes/no
				break;
			case "3DBtn2":
				SceneController.Dictionary.Load();
				Debug.Log("ROOM LOADED");
				// TODO: Reload scene
				break;
			case "3DBtn3":
				// TODO exit
				Debug.Log("TODO: EXIT");
				break;
		}
	}

	public void OnDestroy() {

		SelectMenuItem.menuSelect -= OnPointerEnter;
		SelectMenuItem.menuDeselect -= OnPointerExit;
		SelectMenuItem.menuPress -= Press;
	}
}
