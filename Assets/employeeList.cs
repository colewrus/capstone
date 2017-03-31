using UnityEngine;
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
		print (this.gameObject.GetComponent<employeeList> ().placeInActiveList);
	}
}
