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

	public List<GameObject> laborerPool = new List<GameObject>();

	public Text employee_Current_Max;

	public Text text_ELM_Cost;
	public Text text_Products_Count;
	public Text text_Expected_Revenue;

	public Text text_current_Money;

	//ints to keep track of all the products we've added to our list
	int chairX;
	int tableX;

	float eCost;
	float lCost;
	float mCost;

	bool fire;

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
		chairX = 0;
		tableX = 0;
	}


	public void Test(){
		print ("test");
	}

	public void AddGeneric(){
		float eCost = unit2_GM.instance.master_class [0].energyCost;
		float lCost = unit2_GM.instance.master_class [0].laborCost;
		float mCost = unit2_GM.instance.master_class [0].materialCost;
		int lMax = unit2_GM.instance.master_class [0].laborerMax;
		string name = unit2_GM.instance.master_class [0].name;
		Sprite tSprite = unit2_GM.instance.master_class [0].sprite_Working;
		float sale = unit2_GM.instance.master_class [0].salePrice;
			
		unit2_GM.instance.productClass_Bill.Add(new productClass(eCost*coefficients_ELM[0], mCost*coefficients_ELM[1], lCost*coefficients_ELM[2], lMax, 0, name, tSprite, sale));
		//productClassList.Add(new productClass(10, 20, 5, 2, 1));
		chairX++;
		text_ELM_Cost.text = "Energy Cost:\n" + Mathf.FloorToInt(list_ELM_Bill_View[0]) + "\nLabor Cost:\n" + Mathf.FloorToInt(list_ELM_Bill_View[1]) + "\nMaterial Cost:\n" + Mathf.FloorToInt(list_ELM_Bill_View[2]);
		if (chairX == 0) {
			text_Products_Count.text += name + ": x" + chairX + "\n";
		} else if (chairX > 0) {
			text_Products_Count.text = name + ": x" + chairX ;
		}
		//print ("Energy: " + unit2_GM.instance.totalCost_Energy + " Labor: " + unit2_GM.instance.totalCost_Labor + " Material: " + unit2_GM.instance.totalCost_Material);
	}

	public void AcceptBill(){
		chairX = 0;
		tableX = 0;
		unit2_GM.instance.totalCost_Energy = list_ELM_Bill_View [0];
		unit2_GM.instance.totalCost_Labor = list_ELM_Bill_View [1];
		unit2_GM.instance.totalCost_Material = list_ELM_Bill_View [2];
		chairX = 0;
		text_Products_Count.text = "";
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
		chairX = 0;
		tableX = 0;
	}




	public void FireWorker(){
		fire = true;
		unit2_GM.instance.stopTick ();
		panel_Hire_Fire.SetActive (true);
		panel_Hire_Fire.GetComponentInChildren<Button> ().GetComponentInChildren<Text> ().text = "fire";
		GameObject temp = panel_Hire_Fire.transform.GetChild (1).gameObject;
		GameObject tempText = panel_Hire_Fire.transform.GetChild (2).gameObject;
		tempText.gameObject.GetComponent<Text>().text = unit2_GM.instance.laborerList [unit2_GM.instance.laborerList.Count - 1].gameObject.GetComponent<laborScript> ().fireText;
		temp.GetComponent<Image> ().sprite = unit2_GM.instance.laborerList [unit2_GM.instance.laborerList.Count-1].gameObject.GetComponent<SpriteRenderer> ().sprite;
	}

	public void FireWorker_Submit(){
		if (fire) {
			Destroy (unit2_GM.instance.laborerList [unit2_GM.instance.laborerList.Count - 1]);
			unit2_GM.instance.laborerList.Remove(unit2_GM.instance.laborerList[unit2_GM.instance.laborerList.Count-1]);
			unit2_GM.instance.ResetWorkStation ();
		}
		if (!fire) {
			unit2_GM.instance.laborerList.Add (laborerPool [0]);
		}

		employee_Current_Max.text = unit2_GM.instance.laborerList.Count + " / max"; //reset the employee cunt Text
	}

	public void HireWorker(){
		fire = false;
		unit2_GM.instance.stopTick ();
		panel_Hire_Fire.SetActive (true);
		panel_Hire_Fire.GetComponentInChildren<Button> ().GetComponentInChildren<Text> ().text = "hire";
		GameObject tempText = panel_Hire_Fire.transform.GetChild (2).gameObject;
		tempText.gameObject.GetComponent<Text> ().text = laborerPool [0].GetComponent<laborScript>().hireText;
		//tempText.gameObject.GetComponent<Text> ().text = unit2_GM.instance.laborerList [unit2_GM.instance.laborerList.Count - 1].gameObject.GetComponent<laborScript> ().hireText;
	
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
