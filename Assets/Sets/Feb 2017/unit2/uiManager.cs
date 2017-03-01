using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class uiManager : MonoBehaviour {
	public static uiManager instance = null;

	public GameObject panel_productCost;
	public Vector3 tmpVec;

	public List<GameObject> productsList = new List<GameObject>();
	List<productClass> productClassList = new List<productClass> ();

	public List<float> coefficients_ELM = new List<float>(); //list of the multipliers for the product costs Energy-Labor-Material order

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}


	// Use this for initialization
	void Start () {
		panel_productCost.SetActive (false);	
	}


	public void Test(){
		print ("test");
	}
	public void AddGeneric(){
		unit2_GM.instance.productClass_Bill.Add(new productClass(20*coefficients_ELM[0], 50*coefficients_ELM[1], 1*coefficients_ELM[2], 2, 0));
		//productClassList.Add(new productClass(10, 20, 5, 2, 1));
		for (int i = 0; i < unit2_GM.instance.laborerList.Count; i++) {
			unit2_GM.instance.laborerList [i].GetComponent<laborScript> ().current_Bill = unit2_GM.instance.productClass_Bill [0];
		}
		print (unit2_GM.instance.productClass_Bill[0].energyCost);
	}

}
