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
		List<List<float>> aBase = new List<List<float>> ();
		List<List<float>> aInverse = new List<List<float>> ();
		aBase.Add (energy_1x3);
		aBase.Add (labor_1x3);
		aBase.Add (materials_1x3);
		//create the identity matrix and go ahead and multiply 
		for (int i = 0; i < identityMatrix.Length; i++) {
			for (int j = 0; j < identityMatrix.Length; j++) {
				//print (identityMatrix [i].Matrix[j]);
				aBase [i] [j] = aBase [i] [j] * -1; //multiply A by -1 to setup subtraction
				aBase[i] [j] =  identityMatrix[i].Matrix[j] + aBase[i][j]; //add the identity matrix to -A
				//aBase[i][j] = 1 - aBase[i][j];
				energy_1x3[j] = aBase[i][j]-1;
				aInverse.Add (energy_1x3);
				print (aBase[i][j]);
			}
		}




	}
	
	// Update is called once per frame
	void Update () {
	
	}

		
}
