using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerScript2 : MonoBehaviour {
	public Transform startPoint;
	public Transform endPoint;
	LineRenderer lazerLine;
	// Use this for initialization
	void Start () {
		lazerLine = GetComponent<LineRenderer> ();
		lazerLine.startWidth = .2f;	
	}
	
	// Update is called once per frame
	void Update () {
		lazerLine.SetPosition (0, startPoint.position);
		lazerLine.SetPosition (1, endPoint.position);		
	}
	
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
