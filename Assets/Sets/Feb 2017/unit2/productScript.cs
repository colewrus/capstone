using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	void OnMouseOver(){
		uiManager.instance.panel_productCost.GetComponentInChildren<Text> ().text = "E: " + Mathf.FloorToInt(energyCost) + "\nM: " + Mathf.FloorToInt (materialCost);
		uiManager.instance.panel_productCost.SetActive (true);		
		uiManager.instance.panel_productCost.transform.position = Camera.main.WorldToScreenPoint (this.transform.position + uiManager.instance.tmpVec);
	}

	void OnMouseExit(){
		uiManager.instance.panel_productCost.SetActive (false);
	}

}
