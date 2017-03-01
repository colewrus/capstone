using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class calculatorScript : MonoBehaviour {
	
	public InputField energyInput;
	public InputField materialInput;
	public InputField laborInput;
	public Text totalBill;
	public List<float> energy_1x3 = new List<float>();
	public List<float> materials_1x3 = new List<float>();
	public List<float> labor_1x3 = new List<float>();
	public matrixClass[] identityMatrix;
	public Matrix4x4 testM;

	public float energyCosts;
	public float materialCosts;
	public float laborCosts;

	public Matrix4x4 mInv;
	public Matrix4x4 rawBill;


	// Use this for initialization
	void Start () {
	}

	public void _Calculator_GM(float eCost, float lCost, float mCost){
		for (int i = 0; i < 4; i++) {//reset mInv[] and testM[] to all 1
			for (int j = 0; j < 4; j++) {
				mInv [i, j] = 1;
				testM [i, j] = 1;
			}
		}

		for (int i = 0; i < 3; i++) { //reset the testM Matrix so we don't bugger the calculations
			for (int j = 0; j < 3; j++) {
				if (i == 0) {
					testM [i, j] = energy_1x3 [j];
				} else if (i == 1) {
					testM [i, j] = materials_1x3 [j];
				} else if (i == 2) {
					testM [i, j] = labor_1x3 [j];
				}
			}
		}

		//create the identity matrix and go ahead and multiply 
		for (int i = 0; i < identityMatrix.Length; i++) {
			for (int j = 0; j < identityMatrix.Length; j++) {
				testM[i,j] = testM[i, j] * -1;
				testM [i, j] = identityMatrix [i].Matrix [j] + testM [i, j];
			}
		}

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
		det = testM[0,0] * (testM[2,2]*testM[1,1]-testM[2,1]*testM[1,2]) - testM[1,0] * (testM[2,2]*testM[0,1] -testM [2,1] * testM [0,2]) + testM[2,0] * (testM [1,2] * testM [0,1] -testM [1,1] * testM [0,2]);
		det = 1 / det;
		int n = 0;

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				testM [i, j] = tempList [n];
				n++;
				testM [i, j] = testM [i, j] * det;
			}
		}
		rawBill [0, 0] = eCost;
		rawBill [1, 0] = lCost;
		rawBill [2, 0] = mCost;
		enterTheMatrix ();

	}

	public void enterTheMatrix(){
		
		mInv = testM * rawBill;

	}
}
