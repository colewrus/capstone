using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testScriptable : MonoBehaviour {

	public scriptObj my_Products;

	public List<scriptObj> warehouse = new List<scriptObj> ();

	public int maxAdds;
	// Use this for initialization
	void Start () {
		maxAdds = 0;

		if (my_Products.current_Workers < my_Products.max_Workers)
			my_Products.current_Workers++;

		print (my_Products.current_Workers + "/" + my_Products.max_Workers);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			my_Products = new scriptObj ();
		}

		if (Input.GetMouseButtonDown (1)) {
			my_Products = null;
		}
	
	}


	void AddProduct(){
		scriptObj newProduct = new scriptObj ();
		newProduct.name = "Obj+" + maxAdds;
		//warehouse.Add (newProduct);
		maxAdds++;

	}


}
