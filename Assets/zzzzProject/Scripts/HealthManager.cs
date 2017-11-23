using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : Photon.MonoBehaviour, IPunObservable
{
    public Slider healthBar;
    private float health;
    
    void Start()
    {
        healthBar.value = 1;
        health = 1;
        print(healthBar.value);
    }
    
    void Update()
    {
        healthBar.value = health;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("Beam"))
        {
            print("TRIGGERSTAY: " + health);
            health -= .01f;
        }
    }

    void IPunObservable.OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) 
    {
        if (stream.isWriting) {
            stream.SendNext (health);
        } else {
            health = (float)stream.ReceiveNext ();
        }
    }    
}
