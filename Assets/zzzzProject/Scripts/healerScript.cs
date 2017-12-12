using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healerScript : Photon.MonoBehaviour {
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			var healthManager = other.gameObject.GetComponent<HealthManager>();
			Debug.Log(string.Format("Healing {0}", other.gameObject.name));
			healthManager.heal(0.5f);
			photonView.RPC("RPCDestroy", PhotonTargets.All);
		}
	}
	
	[PunRPC]
	private void RPCDestroy()
	{
		Destroy(gameObject);
	}
}
