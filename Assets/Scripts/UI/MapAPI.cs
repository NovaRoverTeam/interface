using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAPI : MonoBehaviour {

	public RawImage img;

	string url;
	string APIKey = "AIzaSyCmAjN1F0s1S9VuvKJXnBF-z0Sez9c5L38";
	int mapWidth = 250;
	int mapHeight = 250;

	float lat, lon, latInput, lonInput;
	int zoom;
	bool ERT = false;

	public enum mapType {roadmap, satellite, hybrid, terrain}
	public mapType mapSelected;

	//IEnumerator Map(float markerLat = lat, float markerLon = lon){
	IEnumerator Map(){
		while (true) {
			lat = PlayerPrefs.GetFloat("lat", 0);
			lon = PlayerPrefs.GetFloat("lon", 0);
			latInput = PlayerPrefs.GetFloat("latInput", 0);
			lonInput = PlayerPrefs.GetFloat("lonInput", 0);
			zoom = PlayerPrefs.GetInt("zoom", 0);
			ERT = (PlayerPrefs.GetInt("ERT", 0) == 1);

			url = "https://maps.googleapis.com/maps/api/staticmap?key="+APIKey+"&center="+lat+","+lon+"&markers=color:red%7C"+lat+","+lon+"&size="+mapWidth+"x"+mapHeight+"&maptype="+mapSelected+"&zoom="+zoom+((latInput != 0 && lonInput != 0 && ERT)?"&markers=color:green%7C"+latInput+","+lonInput:"");
			//Debug.Log(url);
			WWW www = new WWW (url);
			while (!www.isDone);

			img.texture = www.texture;
			img.SetNativeSize();
			yield return new WaitForSeconds(2f);
		}
	}

	// Use this for initialization
	void Start () {
		img = gameObject.GetComponent<RawImage> ();
		StartCoroutine (Map());
	}
}
