﻿using UnityEngine;
using System.Collections;

public class employeeList : MonoBehaviour {

	public int placeInActiveList; //reference to the gameObject in the active employee list

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire_Employee(){
		//print (this.gameObject.GetComponent<employeeList> ().placeInActiveList);
		GM_Alpha.instance.CameraManager();

		GameObject tmp = employeeManager.instance.Active_Employees[placeInActiveList];//set a tepmorary variable to hold the employee gameobject associated with this fire buton
		employeeManager.instance.total_Daily_Cost -= tmp.GetComponent<laborer_script>().wage; //extract from the total wage pool
		GM_Alpha.instance.Update_Wage_Text (); //update the text element for the wages 




		employeeManager.instance.Active_Employees.Remove (tmp);
		GM_Alpha.instance.Update_Max_Employees(); //update the text for the current/maximum employees

		Destroy (this.transform.parent.gameObject);
		Destroy (tmp);


		//print (tmp.GetComponent<laborer_script> ().name);
	}
}
