using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "Data", menuName = "Product/List", order = 1)]
public class scriptObj : ScriptableObject {

	public string name = "Chair";
	public float raw_Work;
	public int max_Workers;
	public int current_Workers;
}
