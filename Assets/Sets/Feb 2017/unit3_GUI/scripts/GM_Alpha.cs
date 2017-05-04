﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GM_Alpha : MonoBehaviour {  //------------------------BASICALLY THE EMPLOYEE MANAGER------------------------

	public static GM_Alpha instance = null;

	public float money;
	public Text money_Text;

	public GameObject employee_Fire_List; //make the list handler appear in the GUI should be named "current_Employees"
	public GameObject employee_Listing; //add the actual UI object that holds the info
	public GameObject employee_hire_view; //this is the sprite for the employee you are viewing to hire


	public float employee_Offset_x;
	public float employee_Offset_y;
	public int rowLength; //controls hoe many workers in a row, dynamically changes with a max employee upgrade
	public int columCount;

	//public Animator hireAnimation;

	public GameObject wagesObj;
	public GameObject max_employee_Obj;
	public int ListPos;

	public Text employee_Name; 

	public List<Vector3> employeePos = new List<Vector3> ();

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		ListPos = 0;
		//wagesObj = employee_Fire_List.transform.parent.transform.GetChild (0).gameObject;
		wagesObj.SetActive(false);

		max_employee_Obj.GetComponent<Text> ().text = "Max Employees: " + employeeManager.instance.Active_Employees.Count + "/" + employeeManager.instance.MaxEmployees;
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public void AddEmployee(){
		if (employeeManager.instance.Active_Employees.Count < employeeManager.instance.MaxEmployees) {			

			GameObject tmp = (GameObject)Instantiate (employeeManager.instance.Employee_List [0], new Vector3 (0, 1, 1), Quaternion.identity);


			if (employeeManager.instance.Active_Employees.Count < employeePos.Count) {
				tmp.transform.position = employeePos [employeeManager.instance.Active_Employees.Count];
			}

	

			SpriteRenderer tmpSprite = tmp.GetComponent<SpriteRenderer> ();
			tmpSprite.sprite = tmp.GetComponent<laborer_script> ().characterSprite;

			//tmp.gameObject.name = employeeManager.instance.Employee_List [ListPos].GetComponent<laborer_script> ().name;
			//laborer_script tmpLS = tmp.AddComponent <laborer_script>() as laborer_script;
			employeeManager.instance.Active_Employees.Add (tmp);
			employeeManager.instance.total_Daily_Cost += tmp.GetComponent<laborer_script> ().wage; //add the newest wage to the daily cost
			//employeeManager.instance.Employee_List[ListPos].GetComponent<laborer_script>().hired = true; //set the employee bool to hired so e we can make sure stuff in the carousel works

			//employeeManager.instance.Carousel (); //this guy is causing trouble, problem with not having a new employee to pull the character sprite from

			if (employeeManager.instance.Active_Employees.Count == 1) { //if we actually have employees then add 
				employee_Fire_List.SetActive (true);
				wagesObj.SetActive (true);//activate the text that shows the daily cost
			}
			Animator anim = employee_hire_view.GetComponent<Animator> ();
			anim.Play ("employee_hire");
			// Function below will 
			tmp.GetComponent<Animator> ().Play ("work_idle");
			//update the employee that we are viewing to hire		

			Employee_List_Obj (tmp); //Add this peep to the list
			Update_Wage_Text ();//update the text
			Update_Max_Employees ();
			CameraManager ();
			CameraSize ();
			ListPos++;
            employeeManager.instance.startTick();
			if (employeeManager.instance.Active_Employees.Count > employeePos.Count) {
				print ("hellfire and destruction");
				tmp.SetActive (false);
				/*
				tmp.transform.GetChild (0).gameObject.SetActive (false);
				tmp.GetComponent<SpriteRenderer> ().gameObject.SetActive (false);
				tmp.GetComponent<Animator> ().gameObject.SetActive (false);
				*/
			}

		} else {
			print ("max employees");
			//do something to show max reached
				//play sounds
				//animate the max text
		}
	}




	public void NextEmployeeView(){ //coursel control	
		if (employeeManager.instance.Employee_List [ListPos] != null && employeeManager.instance.Active_Employees.Count < employeeManager.instance.MaxEmployees) {			
			if (!employeeManager.instance.Employee_List [ListPos].GetComponent<laborer_script> ().hired) {
				//check if the thing is done before we change the sprite
				employee_hire_view.GetComponent<Image> ().sprite = employeeManager.instance.Employee_List [ListPos].GetComponent<laborer_script> ().characterSprite;
				employee_hire_view.GetComponent<Animator> ().Play ("employee_from_left");
				employee_Name.text = employeeManager.instance.Employee_List [ListPos].GetComponent<laborer_script> ().name;
			}
		} else {
			employeeManager.instance.hireIcon.gameObject.SetActive (false);
		}


	}


	public void CameraManager(){ //reset the camera to focus on the employees

		int counted = employeeManager.instance.Active_Employees.Count;

		if (counted == 1) {
			GUIM.instance.laborerFocus = employeeManager.instance.Active_Employees [0].transform.position + (transform.forward * -2);
		} else if (counted > 1 && counted < 7) {
			GUIM.instance.laborerFocus = employeeManager.instance.Active_Employees [1].transform.position + (transform.forward * -2);
		}

		GUIM.instance.Camera_Panel_Reset(1);
		CameraSize ();
	}

	void Employee_List_Obj(GameObject eObj){ //actually adds the employee to the factory floor
		GameObject _tmp = (GameObject)Instantiate (employee_Listing, transform.position, Quaternion.identity); //make the ui object that holds the details
		_tmp.transform.SetParent (employee_Fire_List.transform.GetChild(0).gameObject.transform); //set to the proper parent
		_tmp.transform.localScale = new Vector3 (1, 1, 1); //rescale
		_tmp.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = eObj.GetComponent<laborer_script> ().characterSprite; //set variables to the object we just made
		_tmp.transform.GetChild (0).gameObject.transform.localScale = new Vector2(0.65f, 1);
		_tmp.transform.GetChild (1).gameObject.GetComponent<Text> ().text = "-$" + eObj.GetComponent<laborer_script> ().wage;
		//_tmp.transform.GetChild (2).gameObject.GetComponent<employeeList> ().placeInActiveList = (employeeManager.instance.Active_Employees.Count - 1);
	}




	public void Update_Wage_Text(){		
		wagesObj.GetComponent<Text>().text = "-$"+employeeManager.instance.total_Daily_Cost + " /day";
	}

	public void Update_Max_Employees(){
		max_employee_Obj.GetComponent<Text> ().text = "Max Employees: " + employeeManager.instance.Active_Employees.Count + "/" + employeeManager.instance.MaxEmployees;
	}

	void CameraSize(){
		if (employeeManager.instance.Active_Employees.Count == 1) {
			Camera.main.orthographicSize = 3;
		}
		if (employeeManager.instance.Active_Employees.Count == 2) {
			Camera.main.orthographicSize = 5;
		}
		if (employeeManager.instance.Active_Employees.Count == 6) {
			Camera.main.orthographicSize = 7;
		}
	}


}
