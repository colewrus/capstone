using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GUIM : MonoBehaviour {
	

	public GameObject infoPanel;//our main panel;
	public string[] buttonPos;  //!------------------ Array -------- DEBORAAAHHHHHHHH
	GameObject nameHold;
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
		}

		if (infoPanel.activeSelf == true) {
			if (EventSystem.current.currentSelectedGameObject == nameHold) {
				print (nameHold + " : " + EventSystem.current.currentSelectedGameObject);
				Invoke ("Same_Button_Close", 0.4f);
				nameHold = null;
			} else {
				Invoke ("ReOpen", 0.31f);
				nameHold = EventSystem.current.currentSelectedGameObject.gameObject;
			}
			print (Camera.main.transform.position);

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
		//Energy

		//Materials

		//Employees

		//Upgrades

		//Bill of Sale
		// Carousel for item
		// Object Cost
		//Total Cost area
	}
}
