using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tutorial_Product : MonoBehaviour {


	public float workNeeded;
	float myWork;
	Image innerImg;
	// Use this for initialization
	void Start () {
		myWork = 0;
		innerImg = this.gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();
		innerImg.fillAmount = 0;

		//
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void MouseDown(){
		if (myWork < workNeeded) {
			myWork += tutorial.instance.workClickAmount;
			innerImg.fillAmount = myWork / workNeeded;
		
		}
	}

	public void Scale(Vector3 newV){
		this.transform.localScale = newV;
	}

	public void Reset(){
		innerImg.fillAmount = 0;
		myWork = 0;
	}

	public void MouseUp(){
		if (myWork >= workNeeded) {
			//send command to run a tutorial function
			tutorial.instance.Chair_Done();

		}
	}
}
