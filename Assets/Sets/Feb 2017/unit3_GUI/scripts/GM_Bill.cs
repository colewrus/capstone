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
    public List<float> queued_amount = new List<float>(); 

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

        current_Product_name.text = current_Hover_product.name; //set the UI for the current hover product
        current_Product_cost_text.text = "-"+current_Hover_product.rawCost+"m";
        current_Product_value.text = "+$"+current_Hover_product.value;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            sortProduct_UI(warehouse[0]);   
        }
	
	}


	public void GUI_Carousel(){ 
		string dir = EventSystem.current.currentSelectedGameObject.name; //get arrow direction
		if (dir == "button_Left") {
			//do left
			if (current_Hover_product != warehouse [0]) {
				anim.Play ("left_Button");
				carousel_Pos--;
				current_Hover_product = warehouse [carousel_Pos];

                current_Product_name.text = current_Hover_product.name; //set the UI for the current hover product
                current_Product_cost_text.text = "-" + current_Hover_product.rawCost + "m";
                current_Product_value.text = "+$" + current_Hover_product.value;

            } else {				
				return;
			}
		}
		if (dir == "button_right") {
			//do right
			if (current_Hover_product != warehouse [warehouse.Count - 1]) {				

				//reset the hover product
				carousel_Pos++;
				current_Hover_product = warehouse [carousel_Pos]; //current hover needs to hold the product info
                                                                  //animate
                anim.Play("right_button");
                current_Product_name.text = current_Hover_product.name; //set the UI for the current hover product
                current_Product_cost_text.text = "-" + current_Hover_product.rawCost +"m";
                current_Product_value.text = "+$" + current_Hover_product.value;
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

        /*
        for(int i=0; i<queued_UI.Count; i++)
        {
            queued_UI[i].transform.GetChild(4).GetComponentInChildren<Text>().text = "-" + amount_add_Queue;
            
        }
        */
	}

	public void changeSprite(){ //animation event
		
		img_Product.sprite = current_Hover_product.productIcon;
	}


	public void Add_Bill(){
        for(int i = 0; i < amount_add_Queue; i++)
        {
            Product newProd = new Product();
            newProd.name = current_Hover_product.name;
            newProd.current_workers = 0;
            newProd.maximum_workers = current_Hover_product.maximum_workers;
            newProd.materialCost = current_Hover_product.materialCost;
            newProd.energyCost = current_Hover_product.energyCost;
            newProd.laborCost = current_Hover_product.laborCost;
            newProd.productIcon = current_Hover_product.productIcon;
            newProd.rawCost = current_Hover_product.rawCost;
            newProd.value = current_Hover_product.value;

            sortProduct_UI(newProd);
            
            Queue.Add(newProd);
        }

    }

    void sortProduct_UI(Product prod) //updates all the UI elements on the bill of goods panel 
    {

        if (prod.name == "Chair")
        {
            queued_amount[0]++;
            queued_UI[0].transform.GetChild(1).GetComponent<Text>().text = "x" + queued_amount[0]; //set the text that shows how many are queued
            queued_UI[0].transform.GetChild(2).GetComponent<Text>().text = "-" + (queued_amount[0] * prod.rawCost)+"m"; //change the total material cost text
            queued_UI[0].transform.GetChild(3).GetComponent<Text>().text = "+$" + (queued_amount[0] * prod.value);

            if (!queued_UI[0].activeSelf) //if you're inactive, activate
            {
                queued_UI[0].SetActive(true);
            }
        }

        if (prod.name == "stool")
        {
            queued_amount[1] ++;
            queued_UI[1].transform.GetChild(1).GetComponent<Text>().text = "x" + queued_amount[1];
            queued_UI[1].transform.GetChild(2).GetComponent<Text>().text = "-" + (queued_amount[1] * prod.rawCost)+"m"; //change the total material cost text
            queued_UI[1].transform.GetChild(3).GetComponent<Text>().text = "+$" + (queued_amount[1] * prod.value);

            if (!queued_UI[1].activeSelf)
            {
                queued_UI[1].SetActive(true);
            }
        }

        if (prod.name == "dresser")
        {
            queued_amount[2] ++;
            queued_UI[2].transform.GetChild(1).GetComponent<Text>().text = "x" + queued_amount[2];
            queued_UI[2].transform.GetChild(2).GetComponent<Text>().text = "-" + (queued_amount[2] * prod.rawCost) + "m"; //change the total material cost text
            queued_UI[2].transform.GetChild(3).GetComponent<Text>().text = "+$" + (queued_amount[2] * prod.value);

            if (!queued_UI[2].activeSelf)
            {
                queued_UI[2].SetActive(true);
            }
        }

        if (prod.name == "wardrobe")
        {
            queued_amount[3] ++;
            queued_UI[3].transform.GetChild(1).GetComponent<Text>().text = "x" + queued_amount[3];
            queued_UI[3].transform.GetChild(2).GetComponent<Text>().text = "-" + (queued_amount[3] * prod.rawCost) + "m"; //change the total material cost text
            queued_UI[3].transform.GetChild(3).GetComponent<Text>().text = "+$" + (queued_amount[3] * prod.value);
            if (!queued_UI[3].activeSelf)
            {
                queued_UI[3].SetActive(true);
            }
        }

        if (prod.name == "bed")
        {
            queued_amount[4] ++;
            queued_UI[4].transform.GetChild(1).GetComponent<Text>().text = "x" + queued_amount[4];
            queued_UI[4].transform.GetChild(2).GetComponent<Text>().text = "-" + (queued_amount[4] * prod.rawCost) + "m"; //change the total material cost text
            queued_UI[4].transform.GetChild(3).GetComponent<Text>().text = "+$" + (queued_amount[4] * prod.value);
            if (!queued_UI[4].activeSelf)
            {
                queued_UI[4].SetActive(true);
            }
        }

    }
}
