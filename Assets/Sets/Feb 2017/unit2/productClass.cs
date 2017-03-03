using UnityEngine;
using System.Collections;


[System.Serializable]
public class productClass {

	public float energyCost;
	public float materialCost;
	public float laborCost;
	public int laborerMax;
	public int currentLaborCount;
	bool initialized;

	public productClass(float eCost, float mCost, float lCost, int lCount, int lCurrent){
		energyCost = eCost;
		materialCost = mCost;
		float labor_difference = (materialCost / unit2_GM.instance.laborer_MaterialScore) - (energyCost / unit2_GM.instance.laborer_EnergyScore);
		if (labor_difference > 0) {
			laborCost = (materialCost / unit2_GM.instance.laborer_MaterialScore);
		} else if (labor_difference < 0) {
			laborCost = (energyCost / unit2_GM.instance.laborer_EnergyScore);
		}
		//_calculator.GetComponent<calculatorScript> ()._Calculator_GM (eCost, lCost, mCost
		unit2_GM.instance._calculator.GetComponent<calculatorScript>()._Calculator_GM(energyCost, laborCost, materialCost);

		//set the newly calculated costs
		energyCost = unit2_GM.instance._calculator.GetComponent<calculatorScript> ().mInv [0, 0];
		laborCost = unit2_GM.instance._calculator.GetComponent<calculatorScript> ().mInv [1, 0];
		materialCost = unit2_GM.instance._calculator.GetComponent<calculatorScript> ().mInv [0, 0];

		//set the total cost
		unit2_GM.instance.totalCost_Energy += energyCost;
		unit2_GM.instance.totalCost_Labor += laborCost;
		unit2_GM.instance.totalCost_Material += materialCost;


		laborerMax = lCount;
		currentLaborCount = lCurrent;
		initialized = true;
	}


}
