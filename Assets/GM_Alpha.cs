using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GM_Alpha : MonoBehaviour {

	public static GM_Alpha instance = null;

	public GameObject employee_Fire_List; //make the list handler appear in the GUI should be named "current_Employees"
	public GameObject employee_Listing; //add the actual UI object that holds the info


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
			//figure out placement grid

			SpriteRenderer tmpSprite = tmp.GetComponent<SpriteRenderer> ();
			tmpSprite.sprite = employeeManager.instance.Employee_List [ListPos].GetComponent<laborer_script> ().characterSprite;
			tmp.gameObject.name = employeeManager.instance.Employee_List [ListPos].GetComponent<laborer_script> ().name;
			//laborer_script tmpLS = tmp.AddComponent <laborer_script>() as laborer_script;
			employeeManager.instance.Active_Employees.Add (tmp);
			employeeManager.instance.total_Daily_Cost += tmp.GetComponent<laborer_script> ().wage; //add the newest wage to the daily cost
			; //this gots to go

			if (employeeManager.instance.Active_Employees.Count == 1) {
				employee_Fire_List.SetActive (true);
				wagesObj.SetActive (true);//activate the text that shows the daily cost
			}

			Employee_List_Obj (tmp); //Add this peep to the list
			Update_Wage_Text ();//update the text
			Update_Max_Employees();
		}

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
		wagesObj.GetComponent<Text>().text = "-$"+employeeManager.instance.total_Daily_Cost;
	}

	public void Update_Max_Employees(){
		max_employee_Obj.GetComponent<Text> ().text = "Max Employees: " + employeeManager.instance.Active_Employees.Count + "/" + employeeManager.instance.MaxEmployees;
	}



}
