using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour {
	public GameObject playerCamera;
	[SerializeField] public MonoBehaviour[] playerControlScripts;

	private PhotonView photonView;

	void Start() {
		photonView = GetComponent<PhotonView>();
		initialize ();
	}

	private void initialize() {
		if (!photonView.isMine) {
			playerCamera.SetActive (false);
			foreach (MonoBehaviour m in playerControlScripts) {
				m.enabled = false;
			}
		}
	}
}
