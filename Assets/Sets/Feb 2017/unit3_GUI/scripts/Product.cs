using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "Data", menuName = "Product/List", order = 1)]
public class Product : ScriptableObject{

	public string name;
	public Sprite objBlock; //the workshop block
	public Sprite productIcon; //what appears when you add it to the list and what appears as their timer
	public float energyCost;
	public float materialCost;
	public float laborCost;
	public float value;
	public int maximum_workers;
	public int current_workers;

	public float rawCost; //the work variable to determine how much work gets done

}
