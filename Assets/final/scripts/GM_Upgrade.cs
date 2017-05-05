using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GM_Upgrade : MonoBehaviour {

    public List<Product> new_Product_Plans = new List<Product>();

	public List<GameObject> upgrade_Buttons = new List<GameObject>();

	public float storage_Base_cost;
	public float storage_growth_rate;
	public float storage_count;

	public float new_Prod_base_Cost;
	public float new_Prod_growth_rate;
	public float new_Prod_count;

	public float max_workers_base_cost;
	public float max_workers_growth_rate;
	public float max_workers_count;


	// Use this for initialization
	void Start () {
		storage_count = 1;
		new_Prod_count = 1;
		max_workers_count = 1;
		upgrade_Buttons[0].transform.GetChild(0).GetComponentInChildren<Text>().text = "$" + max_workers_base_cost * Mathf.Pow (max_workers_growth_rate, max_workers_count);
		upgrade_Buttons[1].transform.GetChild(0).GetComponentInChildren<Text>().text = "$" + new_Prod_base_Cost * new_Prod_growth_rate * new_Prod_count;
		upgrade_Buttons[2].transform.GetChild(0).GetComponentInChildren<Text>().text = "$" + storage_Base_cost * (storage_growth_rate) * storage_count;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Max_Workers(){

		float nextCost = max_workers_base_cost * Mathf.Pow (max_workers_growth_rate, max_workers_count);
		if (GM_Alpha.instance.money > nextCost) {
			GM_Alpha.instance.money -= nextCost;
			employeeManager.instance.MaxEmployees++;
			max_workers_count++;
			GM_Alpha.instance.Update_Max_Employees();

			nextCost = max_workers_base_cost * Mathf.Pow (max_workers_growth_rate, max_workers_count);
			upgrade_Buttons [0].transform.GetChild (0).GetComponentInChildren<Text> ().text = "$" + nextCost;
		}



	}

	public void New_Product(){
		
		if (new_Product_Plans [0] != null) {
			float nextCost = new_Prod_base_Cost * Mathf.Pow(new_Prod_growth_rate, new_Prod_count);
			if (GM_Alpha.instance.money > nextCost) {
				Product tmp = new_Product_Plans [0];
				GM_Bill.instance.warehouse.Add (tmp);
				new_Product_Plans.Remove (tmp);

				GM_Alpha.instance.money -= nextCost;

				new_Prod_count++;
				nextCost = new_Prod_base_Cost * Mathf.Pow(new_Prod_growth_rate, new_Prod_count);
				upgrade_Buttons [1].transform.GetChild (0).GetComponentInChildren<Text> ().text = "$" + nextCost;
			}
		}

	}

	public void Increase_Storage(){
		float nextCost = storage_Base_cost * (storage_growth_rate) * storage_count;
		if (GM_Alpha.instance.money > nextCost) {
			GM_Alpha.instance.money -= nextCost; //remove money
			//GM_Alpha.instance.money_Text.text = "$"+GM_Alpha.instance.money; //reset money UI

			GM_Mats.instance.max_Storage += 50;

			storage_count++;
			nextCost = storage_Base_cost * (storage_growth_rate) * storage_count;
			upgrade_Buttons [2].transform.GetChild (0).GetComponentInChildren<Text> ().text = "$" + nextCost;
		}
	}

}
