using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GM_Bill : MonoBehaviour {

	public static GM_Bill instance = null;

	public List<GameObject> queued_UI = new List<GameObject> (); //the toggle that chooses how many products will be added
	public float amount_add_Queue; //the variable for the toggle
	public List<Product> warehouse = new List<Product> (); // list of the products that can be selected

	public Product current_Hover_product; //this is what you are seeing on the panel right now
	public Text current_Product_name;
	public Text current_Product_cost_text;
	public Text current_Product_value;


	int carousel_Pos; //your position in the carousel

	public List<Product> Queue = new List<Product>(); //the items in the queue for employees to "claim" 



	//UI elements
	public Image img_Product; //image of the employee you are viewing
	Animator anim;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		current_Hover_product = warehouse [0];
		img_Product.sprite = warehouse [0].productIcon;
		carousel_Pos = 0;
		anim = img_Product.GetComponent<Animator> ();

		current_Product_cost_text.text = "--- materials";
		amount_add_Queue = 1;
		for (int i = 0; i < queued_UI.Count; i++) {
			queued_UI [i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {

	
	}


	public void GUI_Carousel(){ 
		string dir = EventSystem.current.currentSelectedGameObject.name; //get arrow direction
		if (dir == "button_Left") {
			//do left
			if (current_Hover_product != warehouse [0]) {
				anim.Play ("left_Button");
				carousel_Pos--;
				current_Hover_product = warehouse [carousel_Pos];

				//update info box
					//name
					//material cost
					//revenue

			} else {
				print ("can't even");
				return;
			}
		}
		if (dir == "button_right") {
			//do right

			if (current_Hover_product != warehouse [warehouse.Count - 1]) {
				
				//animate
				anim.Play ("right_button"); 
				//reset the hover product
				carousel_Pos++;
				current_Hover_product = warehouse [carousel_Pos]; //current hover needs to hold the product info

				//update the info box
					//name
					//material cost
					//revenue


			} else {
				return;
			}
		}		
	}

	public void setBuyAmount(){
		if (EventSystem.current.currentSelectedGameObject.name == "plusOneBox") {
			amount_add_Queue = 1;
		}
		if (EventSystem.current.currentSelectedGameObject.name == "plusFiveBox") {
			amount_add_Queue = 5;
		}
		if (EventSystem.current.currentSelectedGameObject.name == "plusTenBox") {
			amount_add_Queue = 10;
		}

	}

	public void changeSprite(){
		
		img_Product.sprite = current_Hover_product.productIcon;
	}


	public void Add_Bill(){
		Product newProd = new Product ();
		newProd.name = current_Hover_product.name;
		newProd.current_workers = 0;
		newProd.maximum_workers = current_Hover_product.maximum_workers;
		newProd.materialCost = current_Hover_product.materialCost;
		newProd.energyCost = current_Hover_product.energyCost;
		newProd.laborCost = current_Hover_product.laborCost;
		newProd.productIcon = current_Hover_product.productIcon;
		newProd.rawCost = current_Hover_product.rawCost;
		
		Queue.Add (newProd);

		//calculate the costs for the product
	
	}
}
