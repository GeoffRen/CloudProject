using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			var healthManager = other.gameObject.GetComponent<HealthManager>();
			Debug.Log(string.Format("TRIGGERSTAY: {0} Health at: {1}", other.gameObject.name, healthManager.Health));
			healthManager.takeDamage(.01f);
		}
	}
}
