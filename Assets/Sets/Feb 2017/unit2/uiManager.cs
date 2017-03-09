using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class uiManager : MonoBehaviour {
	public static uiManager instance = null;

	public GameObject panel_productCost;
	public GameObject panel_Bill;
	public GameObject panel_Hire_Fire;
	public Vector3 tmpVec; //how much to offest the product remaining cost panel

	public Text employee_Current_Max;

	public Text text_ELM_Cost;
	public Text text_Products_Count;
	public Text text_Expected_Revenue;

	float eCost;
	float lCost;
	float mCost;

	public List<float> list_ELM_Bill_View = new List<float> ();
	public List<GameObject> productsList = new List<GameObject>();
	List<productClass> productClassList = new List<productClass> ();

	public List<float> coefficients_ELM = new List<float>(); //list of the multipliers for the product costs Energy-Labor-Material order

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}


	// Use this for initialization
	void Start () {
		panel_productCost.SetActive (false);	
		panel_Bill.SetActive (true); //bring up the bill of goods panel
		panel_Hire_Fire.SetActive(false);
		employee_Current_Max.text = unit2_GM.instance.laborerList.Count + " / max";
	}


	public void Test(){
		print ("test");
	}

	public void AddGeneric(){
		unit2_GM.instance.productClass_Bill.Add(new productClass(20*coefficients_ELM[0], 50*coefficients_ELM[1], 1*coefficients_ELM[2], 1, 0, "chair"));
		//productClassList.Add(new productClass(10, 20, 5, 2, 1));
		
		text_ELM_Cost.text = "Energy: " + Mathf.FloorToInt(list_ELM_Bill_View[0]) + "\nLabor: " + Mathf.FloorToInt(list_ELM_Bill_View[1]) + "\nMaterial: " + Mathf.FloorToInt(list_ELM_Bill_View[2]);
		//print ("Energy: " + unit2_GM.instance.totalCost_Energy + " Labor: " + unit2_GM.instance.totalCost_Labor + " Material: " + unit2_GM.instance.totalCost_Material);
	}

	public void AcceptBill(){
		unit2_GM.instance.totalCost_Energy = list_ELM_Bill_View [0];
		unit2_GM.instance.totalCost_Labor = list_ELM_Bill_View [1];
		unit2_GM.instance.totalCost_Material = list_ELM_Bill_View [2];
		unit2_GM.instance.startTick ();
		panel_Bill.SetActive (false);
	}

	public void EditBill(){ //reopens the bill panel
		unit2_GM.instance.stopTick ();
		panel_Bill.SetActive (true);
	}

	public void ClearBill(){
		list_ELM_Bill_View [0] = 0;
		list_ELM_Bill_View [1] = 0;
		list_ELM_Bill_View [2] = 0;
		text_ELM_Cost.text = "Energy: " + Mathf.FloorToInt(list_ELM_Bill_View[0]) + "\nLabor: " + Mathf.FloorToInt(list_ELM_Bill_View[1]) + "\nMaterial: " + Mathf.FloorToInt(list_ELM_Bill_View[2]);
	}


	public void FireWorker(){
		unit2_GM.instance.stopTick ();
		panel_Hire_Fire.SetActive (true);
		panel_Hire_Fire.GetComponentInChildren<Button> ().GetComponentInChildren<Text> ().text = "fire";
		GameObject temp = panel_Hire_Fire.transform.GetChild (1).gameObject;
		GameObject tempText = panel_Hire_Fire.transform.GetChild (2).gameObject;
		tempText.gameObject.GetComponent<Text>().text = unit2_GM.instance.laborerList [unit2_GM.instance.laborerList.Count - 1].gameObject.GetComponent<laborScript> ().fireText;
		temp.GetComponent<Image> ().sprite = unit2_GM.instance.laborerList [unit2_GM.instance.laborerList.Count-1].gameObject.GetComponent<SpriteRenderer> ().sprite;
	}

	public void FireWorker_Submit(bool fire){
		if (fire) {
			Destroy (unit2_GM.instance.laborerList [unit2_GM.instance.laborerList.Count - 1]);
			unit2_GM.instance.laborerList.Remove(unit2_GM.instance.laborerList[unit2_GM.instance.laborerList.Count-1]);
			unit2_GM.instance.ResetWorkStation ();
		}

		employee_Current_Max.text = unit2_GM.instance.laborerList.Count + " / max"; //reset the employee cunt Text
	}

	public void HireWorker(){
		unit2_GM.instance.stopTick ();
		panel_Hire_Fire.SetActive (true);
		panel_Hire_Fire.GetComponentInChildren<Button> ().GetComponentInChildren<Text> ().text = "hire";
	
	
	}

	public void Close_Hire_Fire_Panel(){
		panel_Hire_Fire.SetActive (false);
		unit2_GM.instance.startTick ();
	}

	/*
	public void Button_Hover(GameObject go){
		print ("hover");
		if(go.name == "Chair"){
			print("test");
		}
	}
	*/
}
