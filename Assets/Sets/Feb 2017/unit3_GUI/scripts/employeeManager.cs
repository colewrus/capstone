using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class employeeManager : MonoBehaviour {

	public static employeeManager instance = null;
	public List<GameObject> Employee_List = new List<GameObject> ();
	public List<GameObject> Active_Employees = new List<GameObject>();
	public List<GameObject> Employee_UI_Fire_List = new List<GameObject> (); //this is holding the listed employees 


	public float total_Daily_Cost;
	public int MaxEmployees;


	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}


	// Use this for initialization
	void Start () {
		MaxEmployees = 2;

	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
