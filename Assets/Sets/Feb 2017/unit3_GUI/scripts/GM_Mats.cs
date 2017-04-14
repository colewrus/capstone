using UnityEngine;
using System.Collections;

public class GM_Mats : MonoBehaviour {

	public static GM_Mats instance = null;

	public float current_Mats;
	public float max_Storage;

	public float energy_Cost;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
