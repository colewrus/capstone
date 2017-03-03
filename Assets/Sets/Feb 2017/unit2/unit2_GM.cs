using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class unit2_GM : MonoBehaviour {

	public static unit2_GM instance = null;

	public GameObject _calculator;
	public List<GameObject> billObj = new List<GameObject> (); //bill of products as Gameobjects
	public List<productClass> productClass_Bill = new List<productClass>();
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
		//Assign_Total_Cost ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startTick(){
		InvokeRepeating ("Tick", 1.0f, 1f);	
	}

	public void stopTick(){
		CancelInvoke (); 
	}


	void Tick(){
		button_Materials.GetComponentInChildren<Text> ().text = "Materials:\n" + avail_Materials + " / " + max_Materials;
		button_Energy.GetComponentInChildren<Text> ().text = "Energy:\n" + avail_Energy + " / " + max_Energy;

		//_calculator.GetComponent<calculatorScript> ().enterTheMatrix ();

		for (int i = 0; i < laborerList.Count; i++) {
			//print ("labor list");
			if (laborerList [i].GetComponent<laborScript> ().current_Bill != null) {
				//grab the productClass object in 
				productClass tmpProd = laborerList[i].GetComponent<laborScript>().current_Bill;
				print (laborerList [i].name);
				float tmpEnergy = tmpProd.energyCost;
				float tmpMaterials = tmpProd.materialCost;
				print (tmpProd.energyCost);

				if (tmpEnergy > 0) {
					if (avail_Energy > laborerList [i].GetComponent<laborScript> ().energyWork) {
						
						avail_Energy -= laborerList [i].GetComponent<laborScript> ().energyWork;
						tmpProd.energyCost -= laborerList [i].GetComponent<laborScript> ().energyWork;
					}
				}
				if (tmpMaterials > 0) {
					if (avail_Materials > laborerList [i].GetComponent<laborScript> ().materialWork) {
						avail_Materials -= laborerList [i].GetComponent<laborScript> ().materialWork;
						tmpProd.materialCost -= laborerList [i].GetComponent<laborScript> ().materialWork;
					} else if (tmpEnergy <= 0 && tmpMaterials <= 0) {
						productClass_Bill.Remove (laborerList [i].GetComponent<laborScript> ().current_Bill);
						laborerList [i].GetComponent<laborScript> ().current_Bill = null;
					}
				} else if (laborerList [i].GetComponent<laborScript> ().current_Bill == null) {
					for (int j = 0; j < productClass_Bill.Count; j++) {
						if (productClass_Bill [j].currentLaborCount < productClass_Bill [j].laborerMax) {
							laborerList [i].GetComponent<laborScript> ().current_Bill = productClass_Bill [j];
							productClass_Bill [j].currentLaborCount++;
						}
					}
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
