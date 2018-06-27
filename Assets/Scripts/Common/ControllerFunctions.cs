﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoAV.Common {
public class ControllerFunctions : MonoBehaviour {
	// Vibration.
	struct Vibration{
		public ushort duration, intensity;
	};
	Vibration vibration;

	// Get controller object.
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	// Use this for initialization
	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update(){
		// Vibrate the controller.
		if(vibration.duration > 0){
			controller.TriggerHapticPulse(vibration.intensity);
			--vibration.duration;
		}
	}
	
	/// <summary>
	/// 	Makes the controller vibrate.
	/// </summary>
	/// <param name="cycle"> The number of updates it has to vibrate (90 = ~1sec)</param>
	/// <param name="intensity"> The intensity of the vibration. </param>
	public void Vibrate(ushort cycle, ushort intensity){
		vibration.duration = cycle;
		vibration.intensity = intensity;
	}
}
	
}