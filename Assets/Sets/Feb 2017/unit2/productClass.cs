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
	public string name;
	public Sprite sprite_Working;
	Sprite sprite_Finished;
	public GameObject product_GameObject;
	public float salePrice;
	public float progressPerTick;



	public productClass(float eCost, float mCost, float lCost, int lCount, int lCurrent, string n, Sprite workIcon, float sale){
		name = n;
		energyCost = eCost;
		materialCost = mCost;
		salePrice = sale;

		sprite_Working = workIcon;

		laborerMax = lCount;
		currentLaborCount = lCurrent;
		initialized = true;	
		progressPerTick = 100 / energyCost;
	}


}
