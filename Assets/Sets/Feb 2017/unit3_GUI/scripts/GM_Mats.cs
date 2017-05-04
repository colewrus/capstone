using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GM_Mats : MonoBehaviour {

	public static GM_Mats instance = null;

	public float current_Mats;
	public float max_Storage;
	public float mats_Tick_float; //speed of the tick
	public float cost_Tick_per; //

	public Text panel_Total_text;
	public Text buttonText;
	public float energy_Cost;
	public int max_mats_per_time; //maximum of the slider
	public GameObject materialSlider; //the actual slider we use to control;

	public bool can_immediate_add; //controls the clicking 

	public List<GameObject> buy_Buttons = new List<GameObject>();


	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		buttonText.text = "Materials:\n" + current_Mats + "/" + max_Storage;
		panel_Total_text.text = "Materials:\n" + current_Mats + "/" + max_Storage;
		materialSlider.GetComponent<Slider> ().value = 0;
		materialSlider.transform.GetChild (0).GetComponent<Text> ().text = "" + 0;
		materialSlider.transform.GetChild (1).gameObject.SetActive (false);
        Material_Tick_Start();
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void Material_Tick_Start(){
		InvokeRepeating ("Add_Material_Per", 0.1f, mats_Tick_float);
	}


	public void Button_Buy(){
		GameObject tmp = EventSystem.current.currentSelectedGameObject.gameObject;


		if (tmp == buy_Buttons [0]) {
			
			if(max_Storage > (current_Mats+1)){ //make sure that adding this amount won't be so big that it goes over storage
				current_Mats += 1;
				GM_Alpha.instance.money -= 1;
				GM_Alpha.instance.money_Text.text = "$" + GM_Alpha.instance.money;
			}else { //but if it will go over let's split the difference and fill it up to the max	
				current_Mats += (max_Storage-current_Mats);
			}

			buttonText.text = "Materials:\n" + current_Mats + "/" + max_Storage;
			panel_Total_text.text = "Materials:\n" + current_Mats + "/" + max_Storage;
		}
		if (tmp == buy_Buttons [1]) { 
			if (max_Storage > (current_Mats + 5)) { //make sure that adding this amount won't be so big that it goes over storage
				current_Mats += 5;
				GM_Alpha.instance.money -= 5;
				GM_Alpha.instance.money_Text.text = "$" + GM_Alpha.instance.money;
			}else { //but if it will go over let's split the difference and fill it up to the max	
				current_Mats += (max_Storage-current_Mats);
			}

			buttonText.text = "Materials:\n" + current_Mats + "/" + max_Storage;
			panel_Total_text.text = "Materials:\n" + current_Mats + "/" + max_Storage;
		}
		if (tmp == buy_Buttons [2]) {
			if (max_Storage > (current_Mats + 10)) { //make sure that adding this amount won't be so big that it goes over storage
				current_Mats += 10;
				GM_Alpha.instance.money -= 10;
				GM_Alpha.instance.money_Text.text = "$" + GM_Alpha.instance.money;
			} else {
				current_Mats += (max_Storage-current_Mats);
			}

			buttonText.text = "Materials:\n" + current_Mats + "/" + max_Storage;
			panel_Total_text.text = "Materials:\n" + current_Mats + "/" + max_Storage;
		}
		if (tmp == buy_Buttons [3]) {
			if (max_Storage > (current_Mats + 25)) {  //make sure that adding this amount won't be so big that it goes over storage
				current_Mats += 25;
				GM_Alpha.instance.money -= 25;
				GM_Alpha.instance.money_Text.text = "$" + GM_Alpha.instance.money;
			} else { //but if it will go over let's split the difference and fill it up to the max				
				current_Mats += (max_Storage-current_Mats);
			}

			buttonText.text = "Materials:\n" + current_Mats + "/" + max_Storage;
			panel_Total_text.text = "Materials:\n" + current_Mats + "/" + max_Storage;
		}
	}

	public void Add_Material_Per(){
		float tmp = current_Mats + Mathf.Round (materialSlider.GetComponent<Slider> ().value);

		if (tmp < max_Storage) {			
			current_Mats += materialSlider.GetComponent<Slider> ().value;
			GM_Alpha.instance.money -= (cost_Tick_per * materialSlider.GetComponent<Slider> ().value);
			GM_Alpha.instance.money_Text.text = "$" + GM_Alpha.instance.money;
		} else if (current_Mats == max_Storage) {
			print ("niet");
		}else { //but if it will go over let's split the difference and fill it up to the max	
			
			current_Mats += (max_Storage-current_Mats);
			GM_Alpha.instance.money -= ((tmp - max_Storage) * cost_Tick_per);
			GM_Alpha.instance.money_Text.text = "$" + GM_Alpha.instance.money;

		}

		buttonText.text = "Materials:\n" + current_Mats + "/" + max_Storage;
		panel_Total_text.text = "Materials:\n" + current_Mats + "/" + max_Storage;
	}

	public void Set_Material_Max_Buy(){
		materialSlider.GetComponent<Slider> ().maxValue = max_mats_per_time;
	}

	public void materialIncome(){  //change the text above the slider - uses event trigger "update selected"
		materialSlider.transform.GetChild (1).gameObject.SetActive (true);
		EventSystem.current.currentSelectedGameObject.transform.GetComponentInChildren<Text>().text = 
			""+EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value; //set the 
		
		EventSystem.current.currentSelectedGameObject.transform.GetChild (1).GetComponent<Text> ().text = "" + EventSystem.current.currentSelectedGameObject.GetComponent<Slider> ().value +  "x" + cost_Tick_per + " = -$"
		+ cost_Tick_per * EventSystem.current.currentSelectedGameObject.GetComponent<Slider> ().value;
	}
}
