using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class unitTest_1 : MonoBehaviour {


	public List<float> energy_1x3 = new List<float>();
	public List<float> materials_1x3 = new List<float>();
	public List<float> labor_1x3 = new List<float>();
	public matrixClass[] identityMatrix;



	// Use this for initialization
	void Start () {
		for (int i = 0; i < identityMatrix.Length; i++) {
			for (int j = 0; j < identityMatrix.Length; j++) {
				print (identityMatrix [i].Matrix[j]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		
}
