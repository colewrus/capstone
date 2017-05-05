using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class employeeManager : MonoBehaviour {

	public static employeeManager instance = null;
	public List<GameObject> Employee_List = new List<GameObject> ();
	public List<GameObject> Active_Employees = new List<GameObject>();
	public List<GameObject> Employee_UI_Fire_List = new List<GameObject> (); //this is holding the listed employees 

    public List<Material> particle_Sprites = new List<Material>();
    public GameObject progress_Bar;

	public float total_Daily_Cost;
	public int MaxEmployees;

	public float energy_Coefficient; //energy upgrade modifier

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
		energy_Coefficient = 1;
		
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
						GM_Mats.instance.current_Mats -= Mathf.Round(Active_Employees[i].GetComponent<laborer_script>().workScore * energy_Coefficient);
						Active_Employees[i].GetComponent<laborer_script>().assigned_Product.rawCost -= (Active_Employees[i].GetComponent<laborer_script>().workScore * energy_Coefficient);
                        Active_Employees[i].GetComponent<laborer_script>().progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = 1-(Active_Employees[i].GetComponent<laborer_script>().assigned_Product.rawCost / Active_Employees[i].GetComponent<laborer_script>().assigned_Product.materialCost);

                    }else //more mats needed
                    {                        
                        //run the particle system for mats
                        Active_Employees[i].transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = particle_Sprites[1];
                        Active_Employees[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    }               
				}

				if(Active_Employees [i].GetComponent<laborer_script> ().assigned_Product.rawCost <= 0) {
                    //Play the particles for money
                    Active_Employees[i].transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = particle_Sprites[0];
                    Active_Employees[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play();

                    Active_Employees[i].GetComponent<laborer_script>().progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;

                    GM_Alpha.instance.money += Active_Employees[i].GetComponent<laborer_script>().assigned_Product.value;
                    Active_Employees [i].GetComponent<laborer_script> ().assigned_Product = null;
					Active_Employees [i].GetComponent<Animator> ().Play ("work_idle");
					Active_Employees [i].GetComponent<laborer_script> ().products_Made++;

                    lvl_Check(Active_Employees[i]);
                    
				}
			} 
		}
	
	}

    void lvl_Check(GameObject obj)
    {

        if (obj.GetComponent<laborer_script>().products_Made == 5)
        {
            obj.GetComponent<laborer_script>().level++;
            obj.GetComponent<laborer_script>().workScore = 1.1f;
            obj.GetComponent<laborer_script>().wage += 25;
            total_Daily_Cost += 25;
            //reset the gui
            GM_Alpha.instance.Update_Wage_Text();
            obj.GetComponent<laborer_script>().ui_element.transform.GetChild(1).GetComponent<Text>().text = "-$" + obj.GetComponent<laborer_script>().wage;
            //Play particle system
            obj.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = particle_Sprites[3];
            obj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }

        if (obj.GetComponent<laborer_script>().products_Made == 10)
        {
            obj.GetComponent<laborer_script>().level++;
            obj.GetComponent<laborer_script>().workScore = 1.15f;
            obj.GetComponent<laborer_script>().wage += 25;
            total_Daily_Cost += 25;
            //reset the gui
            GM_Alpha.instance.Update_Wage_Text();
            obj.GetComponent<laborer_script>().ui_element.transform.GetChild(1).GetComponent<Text>().text = "-$" + obj.GetComponent<laborer_script>().wage;
            //Play particle system
            obj.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = particle_Sprites[3];
            obj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }

        if (obj.GetComponent<laborer_script>().products_Made == 15)
        {
            obj.GetComponent<laborer_script>().level++;
            obj.GetComponent<laborer_script>().workScore = 1.2f;
            obj.GetComponent<laborer_script>().wage += 50;
            total_Daily_Cost += 50;
            //reset the gui
            GM_Alpha.instance.Update_Wage_Text();
            obj.GetComponent<laborer_script>().ui_element.transform.GetChild(1).GetComponent<Text>().text = "-$" + obj.GetComponent<laborer_script>().wage;
            //Play particle system
            obj.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = particle_Sprites[3];
            obj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }

        if (obj.GetComponent<laborer_script>().products_Made == 20)
        {
            obj.GetComponent<laborer_script>().level++;
            obj.GetComponent<laborer_script>().workScore = 1.25f;
            obj.GetComponent<laborer_script>().wage += 50;
            total_Daily_Cost += 50;
            //reset the gui
            GM_Alpha.instance.Update_Wage_Text();
            obj.GetComponent<laborer_script>().ui_element.transform.GetChild(1).GetComponent<Text>().text = "-$" + obj.GetComponent<laborer_script>().wage;
            //Play particle system
            obj.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = particle_Sprites[3];
            obj.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
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
                        if(tmpProd.name == "Chair")
                        {
                            if(GM_Bill.instance.queued_amount[0] >= 0)
                            {
                                GM_Bill.instance.queued_amount[0]--;
                                GM_Bill.instance.queued_UI[0].transform.GetChild(1).GetComponent<Text>().text = "x" + GM_Bill.instance.queued_amount[0];
                                GM_Bill.instance.queued_UI[0].transform.GetChild(2).GetComponent<Text>().text = "-" + (GM_Bill.instance.queued_amount[0] * tmpProd.rawCost) + "m";
                                GM_Bill.instance.queued_UI[0].transform.GetChild(3).GetComponent<Text>().text = "$" + (GM_Bill.instance.queued_amount[0] * tmpProd.value); 
                            }                            
                        }

                        if (tmpProd.name == "stool")
                        {
                            if (GM_Bill.instance.queued_amount[1] >= 0)
                            {
                                GM_Bill.instance.queued_amount[1]--;
                                GM_Bill.instance.queued_UI[1].transform.GetChild(1).GetComponent<Text>().text = "x" + GM_Bill.instance.queued_amount[1];
                                GM_Bill.instance.queued_UI[1].transform.GetChild(2).GetComponent<Text>().text = "-" + (GM_Bill.instance.queued_amount[1] * tmpProd.rawCost) + "m";
                                GM_Bill.instance.queued_UI[1].transform.GetChild(3).GetComponent<Text>().text = "$" + (GM_Bill.instance.queued_amount[1] * tmpProd.value);
                            }
                        }

                        Active_Employees[i].GetComponent<laborer_script>().assigned_Product = tmpProd; //assign the product we are checking to the employee
                        tmpProd.current_workers++;
                        Active_Employees[i].GetComponent<Animator>().Play("jim_work");
                       
                    } //------End of Queue loop
                    GM_Bill.instance.Queue.Remove(Active_Employees[i].GetComponent<laborer_script>().assigned_Product);
                }else
                {                   
                    Active_Employees[i].transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material = particle_Sprites[2];
                    Active_Employees[i].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                }
                                
            }                       
		} //--------End of Active employee loop
	}



}


