using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healerSetScript : MonoBehaviour {

	public Transform healer;
	public int timer = 500; 


	// Use this for initialization
	void Start () {
		for (int i = 0; i < 5; ++i) 
		{
			float x = Random.Range (-30,30);
			float y = 1;
			float z = Random.Range(-30,30);
			Instantiate (healer, new Vector3(x,y,z), Quaternion.identity);
		}
		
	}

	// Update is called once per frame
	void Update () {
		timer -= 1;
		if (timer == 0) 
		{
			GameObject[] hs = GameObject.FindGameObjectsWithTag ("Healers");
			foreach (GameObject h in hs) 
			{
				Destroy (h.gameObject);
			}
			for (int i = 0; i < 5; ++i) {
				float x = Random.Range (-30, 30);
				float y = 1;
				float z = Random.Range (-30, 30);
				Instantiate (healer, new Vector3 (x, y, z), Quaternion.identity);
			}
			timer = 500;
		}

	}
}
