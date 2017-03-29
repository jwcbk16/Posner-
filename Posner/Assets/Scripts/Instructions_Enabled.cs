using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions_Enabled : MonoBehaviour {
	//introducton script to begin the experiment 
	public GameObject first_text; 
	public GameObject last_text; 
	void Start()
	{
		last_text.transform.position = new Vector3 (-70, -70, -50);
	} 

	void Update() 
	{
		if (Input.GetKey (KeyCode.Space)) {
			bool stat = false;
			first_text.SetActive (stat); 
		
			if (stat == false) {

				last_text.transform.position = new Vector3 (375, 100, 20);
			}
		}
			if (last_text.transform.position == new Vector3 (375, 100, 20)) {
			if (Input.GetKey (KeyCode.Return)) {	
					SceneManager.LoadScene ("Posner_Experiment"); 
				}
			}
		}


}