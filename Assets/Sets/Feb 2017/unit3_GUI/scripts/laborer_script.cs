using UnityEngine;
using System.Collections;
using UnityEditor.Animations;



public class laborer_script : MonoBehaviour{

	public Sprite characterSprite;
	public string name;
	public float score_Energy;
	public float score_Mats;
	public int products_Made;
	public int days_Worked;
	public float wage;
	public AnimatorController animC;
	public bool hired;

	public float workScore; //how much work per tick the employee performs

	public Product assigned_Product;

	void Start(){
		hired = false;
		assigned_Product = null;
	}

}

