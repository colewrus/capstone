using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class unit2_GM : MonoBehaviour {

	public static unit2_GM instance = null;

	public GameObject workStation;
	public List<GameObject> workstationList = new List<GameObject>();

	public productClass[] master_class;

	public GameObject icon;
	public Sprite icon_Sprite;
	[HideInInspector]
	public List<GameObject> iconList = new List<GameObject>();

	public GameObject _calculator;
	public List<GameObject> billObj = new List<GameObject> (); //bill of products as Gameobjects
	public List<productClass> productClass_Bill = new List<productClass>();

	public GameObject employee;
	public List<GameObject> laborerList = new List<GameObject>();
	public Button button_Materials;
	public Button button_Energy;

	public float avail_Materials;
	public float avail_Energy;
	public float max_Materials;


	public float click_EnergyAdd;
	public float click_MaterialAdd;
	public float max_Energy;

	public float laborer_MaterialScore;
	public float laborer_EnergyScore;

	public float currentMoney;


	public float totalCost_Energy;
	public float totalCost_Material;
	public float totalCost_Labor;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		print (transform.up);
		//setup the work stations
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 3; j++) {
				workstationList.Add ((GameObject)Instantiate (workStation, new Vector3 (i * 4, j * 4, 0), Quaternion.identity));
			}
		}
		for (int i = 0; i < workstationList.Count; i++) {
			workstationList [i].SetActive (false);
		}
			

		//setup the employees
		Setup_Laborers();

	}

	public void Setup_Laborers(){
		for (int i = 0; i < laborerList.Count; i++) {
			workstationList [i].SetActive (true);
			laborerList[i] = (GameObject)Instantiate (employee, workstationList [i].transform.position + (transform.up * 1.5f), Quaternion.identity);

			iconList.Add ((GameObject)Instantiate (icon, transform.position, Quaternion.identity));
			Vector3 tmpIconVec = Camera.main.WorldToScreenPoint (laborerList [i].transform.position + (transform.up * -1.25f));
			iconList[i].transform.SetParent((GameObject.Find("Canvas").transform));
			iconList[i].transform.position = tmpIconVec;		
			iconList [i].transform.SetAsFirstSibling ();
		}	
	}
		


	public void ResetWorkStation(){

		for (int i = 0; i < workstationList.Count; i++) {
			workstationList [i].SetActive (false);
		}
		for (int i = 0; i < laborerList.Count; i++) {
			print ("ran");
			workstationList [i].SetActive (true);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			_calculator.GetComponent<calculatorScript> ()._Calculator_GM (20, 5, 50);

		}
	}

	public void startTick(){
		InvokeRepeating ("Tick", 1.0f, 1f);	
	}

	public void stopTick(){
		CancelInvoke (); 
	}


	public void Add_Icon(){
		
	}

	void Tick(){
		//TextUpdates
		button_Materials.GetComponentInChildren<Text> ().text = "Materials:\n" + avail_Materials + " / " + max_Materials;
		button_Energy.GetComponentInChildren<Text> ().text = "Energy:\n" + avail_Energy + " / " + max_Energy;
		uiManager.instance.text_current_Money.text = "$"+currentMoney;

		for (int i = 0; i < laborerList.Count; i++) {
			if (laborerList [i].GetComponent<laborScript> ().assignedBill == false) {

				for (int j = 0; j < productClass_Bill.Count; j++) {
					if (productClass_Bill [j].currentLaborCount < productClass_Bill [j].laborerMax) {
						
						laborerList [i].GetComponent<laborScript> ().current_Bill = productClass_Bill [j];
						productClass_Bill [j].currentLaborCount++;
						laborerList [i].GetComponent<laborScript> ().assignedBill = true;


						iconList [i].GetComponent<Image> ().sprite = productClass_Bill [j].sprite_Working;
						Transform tInnerIcon = iconList [i].gameObject.transform.GetChild(0);
						tInnerIcon.gameObject.GetComponent<Image> ().sprite = productClass_Bill [j].sprite_Working;
						break;
					}
				}
			}
			if (laborerList [i].GetComponent<laborScript> ().assignedBill) {
				//grab the productClass object in 
				productClass tmpProd = laborerList[i].GetComponent<laborScript>().current_Bill;

				float tmpEnergy = tmpProd.energyCost;
				float tmpMaterials = tmpProd.materialCost;
				Transform tInnerIcon = iconList [i].gameObject.transform.GetChild(0);

				if (tmpEnergy > 0) {
					if (avail_Energy > laborerList [i].GetComponent<laborScript> ().energyWork) {						
						avail_Energy -= laborerList [i].GetComponent<laborScript> ().energyWork;
						tmpProd.energyCost -= laborerList [i].GetComponent<laborScript> ().energyWork;

						tInnerIcon.GetComponent<Image> ().fillAmount = (tmpProd.energyCost*tmpProd.progressPerTick)/100;
					}
				}
				if (tmpMaterials > 0) {
					if (avail_Materials > laborerList [i].GetComponent<laborScript> ().materialWork) {
						avail_Materials -= laborerList [i].GetComponent<laborScript> ().materialWork;
						tmpProd.materialCost -= laborerList [i].GetComponent<laborScript> ().materialWork;		


					} 
				}

				laborerList [i].GetComponent<Animator> ().Play ("bob_work");

				if (tmpEnergy <= 0 && tmpMaterials <= 0) {
					tInnerIcon.GetComponent<Image> ().sprite = icon_Sprite;
					tInnerIcon.GetComponent<Image> ().fillAmount = 1;
					tInnerIcon.GetComponentInParent<Image> ().sprite = icon_Sprite;
					currentMoney += laborerList [i].GetComponent<laborScript> ().current_Bill.salePrice;
					productClass_Bill.Remove (laborerList [i].GetComponent<laborScript> ().current_Bill);



					laborerList [i].GetComponent<laborScript> ().current_Bill = null;
					laborerList [i].GetComponent<laborScript> ().assignedBill = false;
					laborerList [i].GetComponent<Animator> ().Play ("bob_idle");
					//stop the work animation
				}


			}//----End of else if
		}//----End of initial for loop


	}


	public void EnergyClick(){
		avail_Energy += click_EnergyAdd;
	}


	public void MaterialClick(){
		avail_Materials += click_MaterialAdd;
	}

}
