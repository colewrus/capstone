using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GM_Bill : MonoBehaviour {

	public static GM_Bill instance = null;

	public List<Product> warehouse = new List<Product> ();

	public Product current_Hover_product; //this is what you are seeing on the panel right now
	int carousel_Pos; //your position in the carousel

	public List<Product> Queue = new List<Product>();


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
	}
	
	// Update is called once per frame
	void Update () {

	
	}


	public void GUI_Carousel(){ 
		string dir = EventSystem.current.currentSelectedGameObject.name;
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
