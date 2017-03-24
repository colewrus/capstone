using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class tutorial : MonoBehaviour {

	public static tutorial instance = null;

	public GameObject panel_ButtonBKG;
	public List<GameObject> buttons = new List<GameObject>();
	public List<bool> tutorial_Stage = new List<bool> ();
	//public int tutorial_Stage; 
	public List<AudioClip> VO = new List<AudioClip>();
	public List<Sprite> iconList = new List<Sprite> ();
	public GameObject icon;
	public bool camStart;

	AudioSource tut_Audio;

	public float first_Click_delay;
	bool click_Check;
	float timer;

	public float workClickAmount;

	public List<GameObject> employeeList = new List<GameObject> ();
	public List<GameObject> employee_ActiveList = new List<GameObject> ();
	public GameObject prefab_Employee;

	//the actual game variables
	public float currentCash;
	public List<GameObject> list_Employees = new List<GameObject>();

	public GameObject table;
	public GameObject panel_CompanyName;

	public int chair_count;
	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}


	// Use this for initialization
	void Start () {
		camStart = true;
		panel_ButtonBKG.SetActive (false);
		GUIM.instance.infoPanel.SetActive (false);
		//tutorial_Stage = 0;
		tut_Audio = this.GetComponent<AudioSource> ();
		//tut_Audio.PlayOneShot (VO [0], 0.8f);
		click_Check = false; //did the player get impatient?
		for (int i = 0; i < tutorial_Stage.Count; i++) { //set all our stage booleans to false
			tutorial_Stage [i] = false;
		}

		GUIM.instance.clear_Buttons ();
		GUIM.instance.clear_Buttons_Actual ();
		//GameObject e1 = (GameObject)Instantiate (prefab_Employee, new Vector3 (-193, 8, 0), Quaternion.identity);
		//AddEmployee(0);
		currentCash = 100.00f;
		Camera.main.orthographicSize = 1.83f;
	}

	public void AddEmployee(int ListPos){
		GameObject tmp = (GameObject)Instantiate (employeeList [ListPos], new Vector3 (-193, 9, 0), Quaternion.identity);
		SpriteRenderer tmpSprite = tmp.GetComponent<SpriteRenderer> ();
		tmpSprite.sprite = employeeList [ListPos].GetComponent<laborer_script> ().characterSprite;
		tmp.gameObject.name = employeeList [ListPos].GetComponent<laborer_script> ().name;
		employee_ActiveList.Add (tmp);
		InvokeRepeating ("Bob_Tick", 1, 1);
		GameObject.Find ("img_Employee").SetActive (false);
		GameObject.Find ("button_Hire").GetComponent<Button> ().interactable = false;
		GUIM.instance.infoPanel.GetComponent<UIController> ().Hide ();

	}


	void Step_0(){		
		timer += Time.deltaTime;
		//create the box
		//Vector3 tmpIcon = icon.gameObject.transform.localScale;

		icon.gameObject.GetComponent<tutorial_Product> ().Scale (new Vector3 (2, 2, 1));
		icon.transform.SetParent ((GameObject.Find ("Canvas").transform));
		icon.transform.position = Camera.main.WorldToScreenPoint (table.transform.position + (transform.up * 0.75f));

		if (timer < first_Click_delay) {
			if (Input.GetMouseButtonDown (0)) {
				tutorial_Stage [0] = true;
				tut_Audio.PlayOneShot (VO [0], 0.8f);
				Invoke ("Play_Invoke", VO [0].length + 1);
			}
		} else {
			tutorial_Stage [0] = true;
			tut_Audio.PlayOneShot (VO [1], 0.8f); //actually play VO[1]
		}

	}

	void Play_Invoke(){
		tut_Audio.PlayOneShot (VO [1], 0.8f);
	}

	void Step_1(){
		print ("step1");
		icon.SetActive (false);
		ResetIcon ();
		GUIM.instance.button_Obj [0].SetActive (true);
		GUIM.instance.button_Obj [1].SetActive (true);
		GUIM.instance.button_Obj [1].GetComponentInChildren<Text> ().text = "$" + currentCash;
		tut_Audio.PlayOneShot (VO [2], 0.8f); //VO[2]
		Invoke("NameCompany", VO[2].length);//VO[2]
	}

	void Step_2(){
		float tmpTimer = 0;
		Camera.main.orthographicSize = 4;
		tmpTimer += Time.deltaTime;
		tut_Audio.PlayOneShot (VO [4], 0.8f);//VO[4]
		GUIM.instance.button_Obj[4].SetActive(true); //employee button
		//GUIM.instance.infoPanel.SetActive(true);
		camStart = false;
		GUIM.instance.infoPanel.GetComponent<UIController>().Show();
		GUIM.instance.buttons_info [3].SetActive (true);
		icon.GetComponent<tutorial_Product> ().Scale (new Vector3 (1, 1, 1));
		icon.transform.position = Camera.main.WorldToScreenPoint (table.transform.position + (transform.up * 0.5f));

		ResetIcon ();
		if (employee_ActiveList != null) {
			InvokeRepeating ("Bob_Tick", 1, 1);
		}
	}

	void Step_3(){
		icon.GetComponent<Image> ().sprite = iconList [1];
	}

	public void Bill_First(){
		
	}

	void Bob_Tick(){
		print ("tick");
		icon.GetComponent<tutorial_Product> ().Employee_Work ();
		employee_ActiveList [0].GetComponent<Animator> ().Play ("bob_work");
	}




	void ResetIcon(){
		icon.GetComponent<tutorial_Product> ().Reset ();
	}

	void NameCompany(){
		if (!panel_CompanyName.activeSelf) {
			panel_CompanyName.SetActive (true);
		} else {
			panel_CompanyName.SetActive (false);
			ResetIcon ();
			icon.SetActive (true);
			tut_Audio.PlayOneShot (VO [3], 0.8f);//VO[3]
		}
	}

	public void CompanySubmit(){
		print (panel_CompanyName.GetComponentInChildren<InputField> ().text);
		GUIM.instance.button_Obj [7].GetComponent<Text> ().text = panel_CompanyName.GetComponentInChildren<InputField> ().text;
		GUIM.instance.button_Obj [7].SetActive (true);
		NameCompany ();
	}

	void ShowIcon(){
		icon.SetActive (true);
	}

	public void Chair_Done(){
		chair_count++;
		currentCash += 25;
		if (chair_count == 1) {
			Step_1 ();
		} else if (chair_count == 2) {			
			Step_2 ();
		} else if (chair_count == 3) {
			tut_Audio.PlayOneShot (VO [5], 0.8f);	
		} else if (chair_count == 4) {
			tut_Audio.PlayOneShot (VO [6], 0.8f);
		} else if (chair_count == 5) {
			tut_Audio.PlayOneShot (VO [7], 0.8f);
		} else if (chair_count == 6) {
			tut_Audio.PlayOneShot (VO [9], 0.8f);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (!tutorial_Stage [0]) {
			Step_0 ();
		} else {
			GUIM.instance.button_Obj [1].GetComponentInChildren<Text> ().text = "$" + currentCash;
		}
		
	}
}
