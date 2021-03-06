﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour {

	public GameObject ERT;
	public GameObject ERTInit;
	public GameObject miniMap;
	public GameObject miniMapInit;

	public double lat, lon;
	public int zoom = 14;

	public InputField latInputField;
	public InputField lonInputField;
	public double latInput;
	public double lonInput;


	void Start(){
		ERT.SetActive(false);
		miniMap.SetActive(false);
		setLocation();
		PlayerPrefs.SetInt("zoom", zoom);
		PlayerPrefs.SetInt("ERT", 0);
	}

	public void ZoomIn () {
		PlayerPrefs.SetInt("zoom", ++zoom);
	}

	public void ZoomOut () {
		PlayerPrefs.SetInt("zoom", (zoom > 0) ? --zoom : zoom);
	}

	public void ShowERT (){
		ERT.SetActive(true);
		ERTInit.SetActive(false);
		PlayerPrefs.SetInt("ERT", 1);

	}
	public void HideERT (){
		ERT.SetActive(false);
		ERTInit.SetActive(true);
		PlayerPrefs.SetInt("ERT", 0);
	}

	public void ShowMap (){
		miniMap.SetActive(true);
		miniMapInit.SetActive(false);
	}
	public void HideMap (){
		miniMap.SetActive(false);
		miniMapInit.SetActive(true);
	}

	void OnGUI() {
        if (ERT.activeSelf && Event.current.Equals(Event.KeyboardEvent("return"))){
        	latInput = double.Parse(latInputField.text);
        	lonInput = double.Parse(lonInputField.text);

        	PlayerPrefs.SetFloat("latInput", (float)latInput);
			PlayerPrefs.SetFloat("lonInput", (float)lonInput);

            print("Coordinate " + latInput+", "+ lonInput + " Submitted");
        }
    }

    void setLocation(){
    	lat = GPSRead.latitude;
		lon = GPSRead.longitude;
		PlayerPrefs.SetFloat("lat", (float)lat);
		PlayerPrefs.SetFloat("lon", (float)lon);
    }

    void Update(){
    	setLocation();
    }
}
