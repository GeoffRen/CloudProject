using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			PhotonNetwork.Destroy (this.gameObject);
			var healthManager = other.gameObject.GetComponent<HealthManager>();
			Debug.Log(string.Format("TRIGGERSTAY: {0} Health at: {1}", other.gameObject.name, healthManager.Health));
			healthManager.heal(0.5f);

		}
	}
}
