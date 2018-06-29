﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoAV.Editor.ObjectUtil;
using UnityEngine.UI;

namespace DemoAV.Editor.User{

	public class VRChooseObject : MonoBehaviour {

		public delegate void SelectAction(GameObject obj);
		public delegate void DeselectAction();
		public static event SelectAction select;
		public static event DeselectAction deselect;
		public static event SelectAction menuSelect;
		public static event DeselectAction menuDeselect;
		public static event SelectAction menuPress;

		private SteamVR_TrackedObject trackedObj;
		private SteamVR_Controller.Device Controller {
			get { return SteamVR_Controller.Input((int)trackedObj.index); }
		}

		UpdateLineRenderer lineRenderer;
		string prefabName = "";
//		GameObject objToPlace = null;
		VRPlaceObject placingScript;

		// Mask
		int furnitureMask;
		private int menuMask;

		void Awake() {
			trackedObj = GetComponent<SteamVR_TrackedObject>();
			placingScript = GetComponent<VRPlaceObject>();		
		}

		// Use this for initialization
		void Start () {
			furnitureMask = LayerMask.GetMask("FurnitureLayer");
			menuMask = LayerMask.GetMask("Menu Layer");
			lineRenderer = GetComponent<UpdateLineRenderer>();
		}
		
		// Update is called once per frame
		void Update () {

			/*			
			if (chooseFurniture(out objToPlace)) {

				placingScript.setObject(objToPlace, prefabName);
				objToPlace.GetComponent<Interactible>().RemoveSelectionEvent();

				// Update status: switch working scripts
				this.enabled = false;
				placingScript.enabled = true;
			}
			*/

			// Check if the user wants to modify an already placed furniture
			Ray ray = new Ray(lineRenderer.GetPosition(), lineRenderer.GetForward());

			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit, 1000f, furnitureMask)) {

				GameObject obj = hit.transform.gameObject;

				if(deselect != null) deselect(); // Call Deselect event: otherwise if objects overlap they all stay blue
				if(select != null) select(obj); // Call Select event

				if(Controller.GetHairTriggerDown()) {

					placingScript.setObject(obj, obj.name);

					// Update status: switch working scripts
					this.enabled = false;
					placingScript.enabled = true;
				}
			}
			else {
				if(deselect != null) deselect(); // Call Deselect event
			}


			// TODO Add action to fire UI
			// TODO UI to left controller

		}




		void OnEnable() {
//			objToPlace = null;
			SelectMenuItem.menuPress += chooseObjectFromEvent;
		}		

		void OnDisable() {
			SelectMenuItem.menuPress -= chooseObjectFromEvent;
		}



		void chooseObjectFromEvent(GameObject obj) {

			Text btnText = GameObject.Find(obj.name + "/Text").GetComponent<Text>();

			GameObject objToPlace = loadResource(btnText.text);


			placingScript.setObject(objToPlace, btnText.text);

			objToPlace.GetComponent<Interactible>().RemoveSelectionEvent();

			// Update status: switch working scripts
			this.enabled = false;
			placingScript.enabled = true;			
		}


		bool chooseFurniture(out GameObject newObject){

			/* 
			if(Input.GetKeyDown ("1")){
				newObject = loadResource("Tavolo");
				return true;
			}

			newObject = null;
			return false;
			*/
			/* 
			RaycastHit hit;
			Ray ray = new Ray(lineRenderer.GetPosition(), lineRenderer.GetForward());

			if(Physics.Raycast(ray, out hit, 1000f, menuMask)) {

				GameObject obj = hit.transform.gameObject;
				lineRenderer.UpdateEnd(hit.point);

				if(menuDeselect != null) menuDeselect(); // Call Deselect event: otherwise if objects overlap they all stay blue
				if(menuSelect != null) menuSelect(obj); // Call Select event


				if(Controller.GetHairTriggerDown()) {

					if(menuPress != null) menuPress(obj);

					

					Text btnText = GameObject.Find(hit.transform.gameObject.name + "/Text").GetComponent<Text>();





					
					if(btnText.text == "Tavolo"){
						newObject = loadResource("Tavolo");
						return true;
					}
					if(btnText.text == "Quadro"){
						newObject = loadResource("Quadro");
						return true;
					}


				}
				
			}
			else {
				if(menuDeselect != null) menuDeselect(); // Call Deselect event: otherwise if objects overlap they all stay blue
				lineRenderer.ResetEnd();
			}
			*/

			newObject = null;
			return false;
		}


		private GameObject loadResource(string res) {

			prefabName = res;
			return Instantiate(Resources.Load("EditorPrefabs/" + res, typeof(GameObject)),
					new Vector3(0, 5, 0), Quaternion.identity) as GameObject;
		}	
	}


}
