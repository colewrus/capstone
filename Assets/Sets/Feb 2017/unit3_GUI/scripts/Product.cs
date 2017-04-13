using UnityEngine;
using System.Collections;


[System.Serializable]
public class Product {

	public string name;
	public Sprite objBlock; //the workshop block
	public Sprite productIcon; //what appears when you add it to the list and what appears as their timer
	public float energyCost;
	public float materialCost;
	public float laborCost;
	public float value;
	public int maximum_workers;
	public int current_workers;

}
