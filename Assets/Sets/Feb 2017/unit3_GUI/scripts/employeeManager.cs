using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class employeeManager : MonoBehaviour {

	public static employeeManager instance = null;
	public List<GameObject> Employee_List = new List<GameObject> ();
	public List<GameObject> Active_Employees = new List<GameObject>();
	public List<GameObject> Employee_UI_Fire_List = new List<GameObject> (); //this is holding the listed employees 


	public float total_Daily_Cost;
	public int MaxEmployees;

    public float employee_Tick_delay;

	public Image hireIcon;

	public int placeInActiveList;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}


	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < Employee_List.Count; i++) {
			if (Employee_List [i] != null) {				
				Employee_List [i].GetComponent<laborer_script> ().hired = false;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void startTick(){
		InvokeRepeating ("Employee_Tick", 1.0f, employee_Tick_delay);
       
	}

	public void stopTick(){
		CancelInvoke ();
	}


	public void Carousel(){
		hireIcon.GetComponent<Image>().sprite = Employee_List [Active_Employees.Count].GetComponent<laborer_script> ().characterSprite;
	}


	public void Employee_Tick(){ 	
		//do I have employee
		Assign_Product();
		employee_Work ();

	}





	void employee_Work(){ //increment the currently held product's raw work value down by the employee work_score
		for (int i = 0; i < Active_Employees.Count; i++) {
			if (Active_Employees [i].GetComponent<laborer_script> ().assigned_Product != null) {
				if (Active_Employees [i].GetComponent<laborer_script> ().assigned_Product.rawCost > 0) {
                    //do a material check
                    if(GM_Mats.instance.current_Mats > Active_Employees[i].GetComponent<laborer_script>().workScore)
                    {
                        GM_Mats.instance.current_Mats -= Active_Employees[i].GetComponent<laborer_script>().workScore;
                        Active_Employees[i].GetComponent<laborer_script>().assigned_Product.rawCost -= Active_Employees[i].GetComponent<laborer_script>().workScore;
                    }else //more mats needed
                    {
                        print("mats needed");
                    }               
				}

				if(Active_Employees [i].GetComponent<laborer_script> ().assigned_Product.rawCost <= 0) {
                    GM_Alpha.instance.money += Active_Employees[i].GetComponent<laborer_script>().assigned_Product.value;
                    Active_Employees [i].GetComponent<laborer_script> ().assigned_Product = null;
					Active_Employees [i].GetComponent<Animator> ().Play ("work_idle");
					Active_Employees [i].GetComponent<laborer_script> ().products_Made++;
                    
				}
			} 
		}
	
	}

	public void Assign_Product(){ //assign products
				
		for (int i = 0; i < Active_Employees.Count; i++) { //Go through employees list          
			
			if (Active_Employees [i].GetComponent<laborer_script> ().assigned_Product == null) {				
				if(GM_Bill.instance.Queue.Count > 0)
                {
                    for (int j = 0; j < GM_Bill.instance.Queue.Count; j++)
                    { //the queue list                    

                        Product tmpProd = GM_Bill.instance.Queue[j];
                        Active_Employees[i].GetComponent<laborer_script>().assigned_Product = tmpProd; //assign the product we are checking to the employee
                        tmpProd.current_workers++;
                        Active_Employees[i].GetComponent<Animator>().Play("jim_work");

                    } //------End of Queue loop
                    GM_Bill.instance.Queue.Remove(Active_Employees[i].GetComponent<laborer_script>().assigned_Product);
                }else
                {
                    print("nothing in the queue");
                }

                
            }
           
		} //--------End of Active employee loop

	}



}


