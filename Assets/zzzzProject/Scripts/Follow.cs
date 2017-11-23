using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject loc;
	private const float LIGHT_HEIGHT = 2f;

	void Update () {
		Vector3 target = loc.transform.position;
		target.y = LIGHT_HEIGHT;
		transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime);	
	}
}
