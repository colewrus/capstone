using UnityEngine;
using System.Collections;


[System.Serializable]
public class productClass {

	public float energyCost;
	public float materialCost;
	public float laborCost;
	public int laborerCount;
	public int currentLaborCount;

	public productClass(float eCost, float mCost, float lCost, int lCount, int lCurrent){
		energyCost = eCost;
		materialCost = mCost;
		laborCost = lCost;
		laborerCount = lCount;
		currentLaborCount = lCurrent;
	}
}
