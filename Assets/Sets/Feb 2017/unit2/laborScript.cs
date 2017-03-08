using UnityEngine;
using System.Collections;

public class laborScript : MonoBehaviour {

	public GameObject currentProduct;
	public productClass current_Bill;
	public float energyWork;
	public float materialWork;
	public bool assignedBill;
	public string name;
	public string fireText;
	public string hireText;

	// Use this for initialization
	void Start () {
		assignedBill = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
