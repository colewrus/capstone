using UnityEngine;
using System.Collections;

public class carousel_img : MonoBehaviour {
	public float testing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SwapSprite(){
		GM_Alpha.instance.NextEmployeeView ();
	}

	public void Bill_Carousel(){ //true is right, false is left
		GM_Bill.instance.changeSprite();
	}
}
