using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GUIM : MonoBehaviour {
	

	public GameObject infoPanel;//our main panel;
	public List<GameObject> panel_Info_buttons = new List<GameObject>();
	public string[] buttonPos;  //!------------------ Array -------- DEBORAAAHHHHHHHH
	GameObject nameHold;
	public GameObject laborerFocus;

	// Use this for initialization
	void Start () {
		infoPanel.GetComponent<UIController> ().Public_Hide ();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Panel_Active_Check(){
		if (nameHold == null) {
			nameHold = EventSystem.current.currentSelectedGameObject.gameObject;
			Camera.main.transform.position = new Vector3 (laborerFocus.transform.position.x-3, laborerFocus.transform.position.y, Camera.main.transform.position.z);
			button_Manger ();
		}

		if (infoPanel.activeSelf == true) {
			if (EventSystem.current.currentSelectedGameObject == nameHold) {
				
				Invoke ("Same_Button_Close", 0.5f);
				nameHold = null;
			} else {
				Invoke ("ReOpen", 0.5f);
				button_Manger ();
				nameHold = EventSystem.current.currentSelectedGameObject.gameObject;
			}


		} 

	}

	void Same_Button_Close(){
		infoPanel.GetComponent<UIController> ().Public_Hide ();
		Camera.main.transform.position = new Vector3 (laborerFocus.transform.position.x, laborerFocus.transform.position.y, Camera.main.transform.position.z);
	}

	void ReOpen(){
		infoPanel.GetComponent<UIController> ().Play ();
	}


	void button_Manger(){
		
		//disable all the button specific info panels
		for (int i = 0; i < panel_Info_buttons.Count; i++) { 
			panel_Info_buttons [i].gameObject.SetActive (false);
		}

		//Money
		if (EventSystem.current.currentSelectedGameObject.name == "Money") {
			panel_Info_buttons [0].gameObject.SetActive (true);
		}

		//Energy
		if (EventSystem.current.currentSelectedGameObject.name == "e") {
			panel_Info_buttons [1].gameObject.SetActive (true);
		}

		//Materials
		if (EventSystem.current.currentSelectedGameObject.name == "m") {
			panel_Info_buttons [2].gameObject.SetActive (true);
		}

		//Employees
		if (EventSystem.current.currentSelectedGameObject.name == "l") {
			panel_Info_buttons [3].gameObject.SetActive (true);
		}
		//Upgrades
		if (EventSystem.current.currentSelectedGameObject.name == "u") {
			panel_Info_buttons [4].gameObject.SetActive (true);
		}
		//Bill of Sale
		if (EventSystem.current.currentSelectedGameObject.name == "b") {
			panel_Info_buttons [5].gameObject.SetActive (true);
		}

	}
}
