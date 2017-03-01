using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class unit2_GM : MonoBehaviour {

	public static unit2_GM instance = null;

	public GameObject _calculator;
	public List<GameObject> billObj = new List<GameObject> ();
	public List<GameObject> laborerList = new List<GameObject>();
	public Button button_Materials;
	public Button button_Energy;
	public float avail_Materials;
	public float avail_Energy;
	public float click_EnergyAdd;
	public float click_MaterialAdd;

	public float laborer_MaterialScore;
	public float laborer_EnergyScore;


	float totalCost_Energy;
	float totalCost_Material;
	float totalCost_Labor;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		Assign_Total_Cost ();
		InvokeRepeating ("Tick", 1.0f, 0.7f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Tick(){

		button_Materials.GetComponentInChildren<Text> ().text = "Materials: " + avail_Materials;
		button_Energy.GetComponentInChildren<Text> ().text = "Energy: " + avail_Energy;



		//_calculator.GetComponent<calculatorScript> ().enterTheMatrix ();

		for (int i = 0; i < laborerList.Count; i++) {
			
			if (laborerList [i].GetComponent<laborScript> ().currentProduct != null) {
				GameObject tmpObj = laborerList [i].GetComponent<laborScript> ().currentProduct;//temporary strore the current product
				float tmpEnergy = tmpObj.GetComponent<productScript> ().energyCost;
				float tmpMaterials = tmpObj.GetComponent<productScript> ().materialCost;


				if (tmpEnergy > 0 || tmpMaterials > 0) {	//make sure work needs doing					
					if(avail_Energy > laborerList[i].GetComponent<laborScript>().energyWork){
						avail_Energy -= laborerList [i].GetComponent<laborScript> ().energyWork; //remove cost from the pool
						tmpObj.GetComponent<productScript> ().energyCost -= laborerList [i].GetComponent<laborScript> ().energyWork; //Reduce the remaining cost on the product
					}
					if(avail_Materials > laborerList[i].GetComponent<laborScript>().materialWork){
						avail_Materials -= laborerList [i].GetComponent<laborScript> ().materialWork; //remove cost from the pool
						tmpObj.GetComponent<productScript> ().materialCost -= laborerList [i].GetComponent<laborScript> ().materialWork; //Reduce the remaining cost on the product			
					}
				} else {
					billObj.Remove (laborerList [i].GetComponent<laborScript> ().currentProduct); //remove the product from the bill
					laborerList [i].GetComponent<laborScript> ().currentProduct = null; //remove the product from the laborer
				}

			} else if(laborerList[i].GetComponent<laborScript>().currentProduct == null) {	//laborer doesn't have an object			
				for (int j = 0; j < billObj.Count; j++) { //go through the list of objects
					if (billObj [j].GetComponent<productScript> ().currentLaborCount < billObj [j].GetComponent<productScript> ().laborerMax) {//does next object have space for another worker?
						if (laborerList [i].GetComponent<laborScript> ().currentProduct == null) {//next laborer with an open slot
							laborerList [i].GetComponent<laborScript> ().currentProduct = billObj [i]; //set the product for the laborer
							billObj [i].GetComponent<productScript> ().currentLaborCount++; //add to the product's laborer count
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

	public void Assign_Total_Cost(){
		//first assign the laborer costs
		for(int i = 0; i < billObj.Count; i++){
			float T1 = laborer_EnergyScore - laborer_MaterialScore;
			float T2 = laborer_MaterialScore - laborer_EnergyScore;
			if (T1 > T2) {
				billObj [i].GetComponent<productScript> ().laborCost = billObj [i].GetComponent<productScript> ().materialCost / laborer_MaterialScore;

			} else if (T2 > T1) {
				billObj [i].GetComponent<productScript> ().laborCost = billObj [i].GetComponent<productScript> ().energyCost / laborer_EnergyScore;
			}




			//------VERY VERY END -----//
			totalCost_Labor += billObj [i].GetComponent<productScript> ().laborCost;
			totalCost_Energy += billObj [i].GetComponent<productScript> ().energyCost;
			totalCost_Material += billObj [i].GetComponent<productScript> ().materialCost;
			print (totalCost_Energy + " " + totalCost_Material + " " + totalCost_Labor);

			//go through the products, divide by the materialScore assign the laborer score. 
				//calculate total cost using the collective costs of the objects, plug into the calculator, for each object to return the value
		}
	}

}
