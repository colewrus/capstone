using UnityEngine;
using System.Collections;

public class productScript : MonoBehaviour {

	public float energyCost;
	public float materialCost;
	public float laborCost; 
	public int laborerMax;
	public int currentLaborCount; //current number of laborers assigned
	//public float laborCost;

	// Use this for initialization
	void Start () {
		currentLaborCount = 0;
	}	

}
