using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GUIM : MonoBehaviour {
	
	public static GUIM instance = null;
	public GameObject infoPanel;//our main panel;
	public List<GameObject> buttons_info = new List<GameObject>();
	public string[] buttonPos;  //!------------------ Array -------- DEBORAAAHHHHHHHH
	GameObject nameHold;
	GameObject infoHold;
	public Vector3 laborerFocus;
	public List<GameObject> button_Obj = new List<GameObject>();




	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Panel_Active_Check(){
		if (nameHold == null) {
			//print (tutorial.instance);
			nameHold = EventSystem.current.currentSelectedGameObject.gameObject;

			if (tutorial.instance == null) {
				
				if (employeeManager.instance.Active_Employees.Count > 0) {
					Camera_Panel_Reset (1);
				}

			}else if (!tutorial.instance.camStart) {
				if (employeeManager.instance.Active_Employees.Count > 0) {
					Camera_Panel_Reset (1);
				}
				tutorial.instance.icon.transform.position = Camera.main.WorldToScreenPoint (tutorial.instance.table.transform.position + (transform.up * 0.25f));
			}
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

	public void Camera_Panel_Reset(int state){
		if (state == 0) {
			Camera.main.transform.position = laborerFocus;
		} else if (state == 1) {
			Camera.main.transform.position = laborerFocus + (transform.right * -2);
		}
		
	}

	void Same_Button_Close(){
		infoPanel.GetComponent<UIController> ().Public_Hide ();
		button_Manager ();
		if (tutorial.instance == null) {
			print ("Check");
			if (employeeManager.instance.Active_Employees.Count > 0) {
				Camera_Panel_Reset (0);
			}
		}else if (!tutorial.instance.camStart) {
			if (employeeManager.instance.Active_Employees.Count > 0) {
				Camera_Panel_Reset (0);
			}
			tutorial.instance.icon.transform.position = Camera.main.WorldToScreenPoint (tutorial.instance.table.transform.position + (transform.up * 0.25f));
		}
	}

	void ReOpen(){
		button_Manager ();
		infoPanel.GetComponent<UIController> ().Play ();
	}


	public void clear_Buttons(){ //actually clears the corresponding info panel
		for (int i = 0; i < buttons_info.Count; i++) {
			buttons_info [i].gameObject.SetActive (false);
		}
	}


	public void clear_Buttons_Actual(){ // hides the actual damn buttons themselves. BKG is 0, 8 is title
		for (int i = 0; i < button_Obj.Count; i++) {
			button_Obj [i].gameObject.SetActive (false);
		}
	}

	 public void button_Manager(){
		
		if (EventSystem.current.currentSelectedGameObject.name == "Money") {
			clear_Buttons ();
			buttons_info [0].gameObject.SetActive (true);
		}
		//Energy
		if (EventSystem.current.currentSelectedGameObject.name == "e") {
			clear_Buttons ();
			buttons_info [1].gameObject.SetActive (true);
		}

		//Materials
		if (EventSystem.current.currentSelectedGameObject.name == "m") {
			clear_Buttons ();
			buttons_info [2].gameObject.SetActive (true);
		}

		//Employees
		if (EventSystem.current.currentSelectedGameObject.name == "l") {
			clear_Buttons ();
			buttons_info [3].gameObject.SetActive (true);
		}

		//Upgrades
		if (EventSystem.current.currentSelectedGameObject.name == "u") {
			clear_Buttons ();
			buttons_info [4].gameObject.SetActive (true);
		}
		//Bill of Sale
		if (EventSystem.current.currentSelectedGameObject.name == "b") {
			clear_Buttons ();
			buttons_info [5].gameObject.SetActive (true);
		}
		// Carousel for item
		// Object Cost
		//Total Cost area
	}
}
