﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour 
{
	[SerializeField] public GameObject[] playerGameObjects;
 	[SerializeField] public MonoBehaviour[] playerControlScripts;

	private PhotonView photonView;

	void Start() 
	{
		photonView = GetComponent<PhotonView>();
		initialize ();
	}

	private void initialize() 
	{
		if (!photonView.isMine) {
			foreach (MonoBehaviour m in playerControlScripts) 
			{
				m.enabled = false;
			}
			
			foreach (GameObject go in playerGameObjects)
			{
				go.SetActive(false);
			}
		}
	}
}
