using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerScript2 : MonoBehaviour
{
	private static Vector3[] rotationAxises = new Vector3[]
	{
		Vector3.up, 
		Vector3.back, 
		Vector3.down, 
		Vector3.forward, 
		Vector3.left,
		Vector3.right
	};
	
	public Transform startPoint;
	public Transform endPoint;
	private LineRenderer lazerLine;
	private Vector3 midPoint;
	private float rotationDegrees;
	private Vector3 rotationAxis;

	void Start () 
	{
		lazerLine = GetComponent<LineRenderer> ();
		lazerLine.startWidth = .2f;
		midPoint = (startPoint.position + endPoint.position) / 2f;
		rotationDegrees = Random.Range(10f, 100f);
		rotationAxis = rotationAxises[Random.Range(0, rotationAxises.Length - 1)];
	}
	
	void Update () 
	{
		startPoint.RotateAround(midPoint, rotationAxis, rotationDegrees * Time.deltaTime);
		endPoint.RotateAround(midPoint, rotationAxis, rotationDegrees * Time.deltaTime);
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
