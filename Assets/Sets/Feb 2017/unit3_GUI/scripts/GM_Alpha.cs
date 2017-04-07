using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GM_Alpha : MonoBehaviour {

	public static GM_Alpha instance = null;

	public GameObject employee_Fire_List; //make the list handler appear in the GUI should be named "current_Employees"
	public GameObject employee_Listing; //add the actual UI object that holds the info
	public GameObject employee_hire_view; //this is the sprite for the employee you are viewing to hire


	public float employee_Offset_x;
	public float employee_Offset_y;
	public int rowLength; //controls hoe many workers in a row, dynamically changes with a max employee upgrade

		

	GameObject wagesObj;
	GameObject max_employee_Obj;
	int ListPos;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}



	// Use this for initialization
	void Start () {
		ListPos = 0;
		wagesObj = employee_Fire_List.transform.parent.transform.GetChild (0).gameObject;
		max_employee_Obj = GameObject.Find ("max_employees");
		max_employee_Obj.GetComponent<Text> ().text = "Max Employees: " + employeeManager.instance.Active_Employees.Count + "/" + employeeManager.instance.MaxEmployees;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddEmployee(){

		if(employeeManager.instance.Active_Employees.Count < employeeManager.instance.MaxEmployees){
			
			ListPos = employeeManager.instance.Active_Employees.Count;
			GameObject tmp = (GameObject)Instantiate (employeeManager.instance.Employee_List [ListPos], new Vector3 (0, 1, 1), Quaternion.identity);

			for (int i = 0; i < ListPos; i++) {				
				for (int n = 0; n < rowLength; n++) {
					tmp.transform.position = new Vector3 (0 + (employee_Offset_x * i), 1 + (employee_Offset_y * n), 1);
				}
			}



	

			SpriteRenderer tmpSprite = tmp.GetComponent<SpriteRenderer> ();
			tmpSprite.sprite = employeeManager.instance.Employee_List [ListPos].GetComponent<laborer_script> ().characterSprite;
			tmp.gameObject.name = employeeManager.instance.Employee_List [ListPos].GetComponent<laborer_script> ().name;
			//laborer_script tmpLS = tmp.AddComponent <laborer_script>() as laborer_script;
			employeeManager.instance.Active_Employees.Add (tmp);
			employeeManager.instance.total_Daily_Cost += tmp.GetComponent<laborer_script> ().wage; //add the newest wage to the daily cost

			//employeeManager.instance.Carousel (); //this guy is causing trouble, problem with not having a new employee to pull the character sprite from

			if (employeeManager.instance.Active_Employees.Count == 1) {
				employee_Fire_List.SetActive (true);
				wagesObj.SetActive (true);//activate the text that shows the daily cost
			}


			employee_hire_view.GetComponent<Animator> ().Play ("employee_hire");
			//update the employee that we are viewing to hire



			Employee_List_Obj (tmp); //Add this peep to the list
			Update_Wage_Text ();//update the text
			Update_Max_Employees();
			CameraManager();
			CameraSize ();

		}

	}


	public void CameraManager(){

		int counted = employeeManager.instance.Active_Employees.Count;

		if (counted == 1) {
			GUIM.instance.laborerFocus = employeeManager.instance.Active_Employees [0].transform.position + (transform.forward * -2);
		} else if (counted > 1 && counted < 7) {
			GUIM.instance.laborerFocus = employeeManager.instance.Active_Employees [1].transform.position + (transform.forward * -2);
		}

		GUIM.instance.Camera_Panel_Reset(1);
		CameraSize ();
	}

	void Employee_List_Obj(GameObject eObj){
		GameObject _tmp = (GameObject)Instantiate (employee_Listing, transform.position, Quaternion.identity); //make the ui object that holds the details
		_tmp.transform.SetParent (employee_Fire_List.transform.GetChild(0).gameObject.transform); //set to the proper parent
		_tmp.transform.localScale = new Vector3 (1, 1, 1); //rescale
		_tmp.transform.GetChild (0).gameObject.GetComponent<Image> ().sprite = eObj.GetComponent<laborer_script> ().characterSprite; //set variables to the object we just made
		_tmp.transform.GetChild (1).gameObject.GetComponent<Text> ().text = "$" + eObj.GetComponent<laborer_script> ().wage;
		_tmp.transform.GetChild (2).gameObject.GetComponent<employeeList> ().placeInActiveList = (employeeManager.instance.Active_Employees.Count - 1);
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
	}


}
