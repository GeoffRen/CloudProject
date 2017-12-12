using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : Photon.MonoBehaviour {

	public GameObject spawnLocation;
	public GameObject lightning;
	[SerializeField] public GameObject[] pickups;

	void Start()
	{
		InvokeRepeating("SpawnPickup", 5.0f, Random.Range(20f, 40f));
	}
	
	private void SpawnPickup()
	{
		photonView.RPC("RPCSpawnPickup", PhotonTargets.All);
	}
    
	[PunRPC]
	private void RPCSpawnPickup()
	{
		var newLoc = new Vector3(Random.Range(-60f, 60f), 1f, Random.Range(-60f, 60f));
		spawnLocation.transform.position = newLoc;
		
		var pickup = pickups[Random.Range(0, pickups.Length - 1)];
		PhotonNetwork.Instantiate(pickup.name, spawnLocation.transform.position, spawnLocation.transform.rotation, 0);
		
		StartCoroutine(CreateLightning());
		Debug.Log(String.Format("SPAWNING PICKUP {0} AT {1}", pickup.name, newLoc));
	}
	
	IEnumerator CreateLightning()
	{
		lightning.SetActive(true);
		yield return new WaitForSeconds(.5f);
		lightning.SetActive(false);
	}

	public void cancelPickupSpawner()
	{
		CancelInvoke();
	}
}
