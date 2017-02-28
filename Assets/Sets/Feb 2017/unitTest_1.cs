using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class unitTest_1 : MonoBehaviour {


	public InputField energyInput;
	public InputField materialInput;
	public InputField laborInput;
	public Text totalBill;
	public List<float> energy_1x3 = new List<float>();
	public List<float> materials_1x3 = new List<float>();
	public List<float> labor_1x3 = new List<float>();
	public matrixClass[] identityMatrix;
	public Matrix4x4 testM;

	Matrix4x4 mInv;
	public Matrix4x4 rawBill;
	//List <float> rawBill = new List<float>(); //raw cost for the bill of goods 

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 4; j++) {
				mInv [i, j] = 1;
			}
		}
		//create the identity matrix and go ahead and multiply 
		for (int i = 0; i < identityMatrix.Length; i++) {
			for (int j = 0; j < identityMatrix.Length; j++) {
				testM[i,j] = testM[i, j] * -1;
				testM [i, j] = identityMatrix [i].Matrix [j] + testM [i, j];
			}
		}
		print (testM);
		List<float> tempList = new List<float>();
		tempList.Add ((testM [2,2] * testM [1,1]) - (testM [2,1] * testM [1,2]));//---1,1
		tempList.Add (-((testM [0,1] * testM [2,2]) - (testM [0,2] * testM [2,1])));//---1,2--
		tempList.Add ((testM [1,2] * testM [0,1]) - (testM [1,1] * testM [0,2]));//---1,3

		tempList.Add (-((testM [2,2] * testM [1,0]) - (testM [2,0] * testM [1,2])));//---2,1--
		tempList.Add ((testM [2,2] * testM [0,0]) - (testM [2,0] * testM [0,2]));//--2,2
		tempList.Add (-((testM [1,2] * testM [0,0]) - (testM [1,0] * testM [0,2])));//--2,3--

		tempList.Add ((testM [2,1] * testM [1,0]) - (testM [2,0] * testM [1,1]));//---3,1
		tempList.Add (-((testM [2,1] * testM [0,0]) - (testM [2,0] * testM [0,1])));//---3,2--
		tempList.Add ((testM [1,1] * testM [0,0]) - (testM [1,0] * testM [0,1])); //---3,3


		float det = 0;
		//det = (testM [1, 1] * ((testM [3, 3] * testM [2, 2]) - (testM [3, 2] * testM [2, 3])) - (testM [2, 1] * ((testM [3, 3] * testM [1, 2]) - (testM [3, 2] * testM [1, 3]))) + (testM [3, 1] ((testM [2, 3] * testM [1, 2]) - (testM [2, 2] * testM [1, 3]))));
		det = testM[0,0] * (testM[2,2]*testM[1,1]-testM[2,1]*testM[1,2]) - testM[1,0] * (testM[2,2]*testM[0,1] -testM [2,1] * testM [0,2]) + testM[2,0] * (testM [1,2] * testM [0,1] -testM [1,1] * testM [0,2]);
		det = 1 / det;
		print (det);
		int n = 0;

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				testM [i, j] = tempList [n];
				n++;
				testM [i, j] = testM [i, j] * det;
			}
		}


	}


	public void AddInputValue(int val){
		if (val == 0) {
			rawBill [0, 0] = (float)double.Parse (energyInput.text, System.Globalization.NumberStyles.AllowDecimalPoint);
		} else if (val == 1) {
			rawBill [1, 0] = (float)double.Parse (laborInput.text, System.Globalization.NumberStyles.AllowDecimalPoint);
		} else if (val == 2) {
			rawBill [2, 0] = (float)double.Parse (materialInput.text, System.Globalization.NumberStyles.AllowDecimalPoint);
		}
	}

	public void enterTheMatrix(){
		mInv = testM * rawBill;
		totalBill.text = "";
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 1; j++) {
				totalBill.text += "\n" + mInv [i, j];
			}
		}
	
	}
		
}
