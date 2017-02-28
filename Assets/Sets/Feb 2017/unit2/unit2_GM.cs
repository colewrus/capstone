using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class unit2_GM : MonoBehaviour {

	public List<GameObject> billObj = new List<GameObject> ();
	public List<GameObject> laborerList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Tick", 1.0f, 0.7f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Tick(){
		for (int i = 0; i < laborerList.Count; i++) {

			if (laborerList [i].GetComponent<laborScript> ().currentProduct != null) {
				GameObject tmpObj = laborerList [i].GetComponent<laborScript> ().currentProduct;//temporary strore 
				print (tmpObj.name);
				float tmpEnergy = tmpObj.GetComponent<productScript> ().energyCost;
				float tmpMaterials = tmpObj.GetComponent<productScript> ().materialCost;
				if (tmpEnergy > 0 || tmpMaterials > 0) {					
					tmpObj.GetComponent<productScript> ().energyCost -= laborerList [i].GetComponent<laborScript> ().energyWork;
					tmpObj.GetComponent<productScript> ().materialCost -= laborerList [i].GetComponent<laborScript> ().materialWork;			
				} else {
					billObj.Remove (laborerList [i].GetComponent<laborScript> ().currentProduct);
					laborerList [i].GetComponent<laborScript> ().currentProduct = null;
				}

			} else if(laborerList[i].GetComponent<laborScript>().currentProduct == null) {	//laborer doesn't have an object			
				for (int j = 0; j < billObj.Count; j++) { //go through the list of objects
					if (billObj [j].GetComponent<productScript> ().currentLaborCount < billObj [j].GetComponent<productScript> ().laborerMax) {//does next object have space for another worker?
						if (laborerList [i].GetComponent<laborScript> ().currentProduct == null) {//next laborer with an open slot
							laborerList [i].GetComponent<laborScript> ().currentProduct = billObj [i]; //set the product for the laborer
							billObj [i].GetComponent<productScript> ().currentLaborCount++; //mark that 
						}
					}
				}
			}//----End of else if
		}//----End of initial for loop

	}


}
