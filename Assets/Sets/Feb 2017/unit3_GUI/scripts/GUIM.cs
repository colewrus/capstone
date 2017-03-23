using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GUIM : MonoBehaviour {
	

	public GameObject infoPanel;//our main panel;
	public List<GameObject> buttons_info = new List<GameObject>();
	public string[] buttonPos;  //!------------------ Array -------- DEBORAAAHHHHHHHH
	GameObject nameHold;
	GameObject infoHold;
	public GameObject laborerFocus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Panel_Active_Check(){
		if (nameHold == null) {
			nameHold = EventSystem.current.currentSelectedGameObject.gameObject;
			Camera.main.transform.position = new Vector3 (laborerFocus.transform.position.x-3, laborerFocus.transform.position.y, Camera.main.transform.position.z);
			button_Manager ();
		}

		if (infoPanel.activeSelf == true) {
			if (EventSystem.current.currentSelectedGameObject == nameHold) {
				
				Invoke ("Same_Button_Close", 0.5f);
				nameHold = null;
			} else {
				Invoke ("ReOpen", 0.35f);
				nameHold = EventSystem.current.currentSelectedGameObject.gameObject;
			}


		} 

	}

	void Same_Button_Close(){
		infoPanel.GetComponent<UIController> ().Public_Hide ();
		button_Manager ();
		Camera.main.transform.position = new Vector3 (laborerFocus.transform.position.x, laborerFocus.transform.position.y, Camera.main.transform.position.z);
	}

	void ReOpen(){
		button_Manager ();
		infoPanel.GetComponent<UIController> ().Play ();
	}


	public void clear_Buttons(){
		for (int i = 0; i < buttons_info.Count; i++) {
			buttons_info [i].gameObject.SetActive (false);
		}
	}

	 public void button_Manager(){
		print (EventSystem.current.currentSelectedGameObject.name);

		if (EventSystem.current.currentSelectedGameObject.name == "Money") {
			for (int i = 0; i < buttons_info.Count; i++) {
				buttons_info [i].gameObject.SetActive (false);
			}
			buttons_info [0].gameObject.SetActive (true);
		}
		//Energy
		if (EventSystem.current.currentSelectedGameObject.name == "e") {
			for (int i = 0; i < buttons_info.Count; i++) {
				buttons_info [i].gameObject.SetActive (false);
			}
			buttons_info [1].gameObject.SetActive (true);
		}

		//Materials
		if (EventSystem.current.currentSelectedGameObject.name == "m") {
			for (int i = 0; i < buttons_info.Count; i++) {
				buttons_info [i].gameObject.SetActive (false);
			}
			buttons_info [2].gameObject.SetActive (true);
		}

		//Employees
		if (EventSystem.current.currentSelectedGameObject.name == "l") {
			for (int i = 0; i < buttons_info.Count; i++) {
				buttons_info [i].gameObject.SetActive (false);
			}
			buttons_info [3].gameObject.SetActive (true);
		}

		//Upgrades
		if (EventSystem.current.currentSelectedGameObject.name == "u") {
			for (int i = 0; i < buttons_info.Count; i++) {
				buttons_info [i].gameObject.SetActive (false);
			}
			buttons_info [4].gameObject.SetActive (true);
		}
		//Bill of Sale
		if (EventSystem.current.currentSelectedGameObject.name == "b") {
			clear_Buttons ();
			print (buttons_info [5].gameObject.name);
			buttons_info [5].gameObject.SetActive (true);
		}
		// Carousel for item
		// Object Cost
		//Total Cost area
	}
}
