using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class soundManager : MonoBehaviour {

	public List<AudioClip> effectsList = new List<AudioClip>();

	public AudioClip bkgSound;



	public AudioSource bkgSource;
	public AudioSource effects;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void SongManager(){
		
	}

	public void BKGsounds(){
		bkgSource.clip = bkgSound;
		bkgSource.PlayDelayed (Random.Range (2.0f, 5.5f));
	}
}
