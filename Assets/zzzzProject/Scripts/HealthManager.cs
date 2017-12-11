using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : Photon.MonoBehaviour, IPunObservable
{
    public const float MaxHealth = 1;
    
    public Slider healthBar;

    private float health;
    public float Health
    {
        get { return health; }
    }

    void Start()
    {
        healthBar.value = MaxHealth;
        health = MaxHealth;
    }
    
    void Update()
    {
        healthBar.value = health;
    }

//    private void FixedUpdate()
//    {
//        if (health < MaxHealth)
//        {
//            health += .0005f;
//        }
//    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

	public void heal(float heal)
	{
		health += heal;
		if (health > MaxHealth) {
			health = MaxHealth;
		}
	}


    void IPunObservable.OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) 
    {
        if (stream.isWriting) 
        {
            stream.SendNext (health);
        } 
        else 
        {
            health = (float)stream.ReceiveNext ();
        }
    }    
}
