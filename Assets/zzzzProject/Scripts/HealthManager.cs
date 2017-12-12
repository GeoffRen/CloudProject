using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : Photon.MonoBehaviour, IPunObservable
{
    public const float MaxHealth = 1;
    
    public Slider healthBar;

    private bool didDie;
    private float health;
    public float Health
    {
        get { return health; }
    }

    void Start()
    {
        healthBar.value = MaxHealth;
        health = MaxHealth;
        didDie = false;
    }
    
    void Update()
    {
        healthBar.value = health;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !didDie)
        {
            Debug.Log("~~~DEAD~~~");
            didDie = true;
            photonView.RPC ("RPCDie", PhotonTargets.All);
            GetComponent<vThirdPersonInput>().enabled = false;
            GetComponent<vThirdPersonController>().enabled = false;
            GetComponent<Beam>().enabled = false;
            StartCoroutine(EnableGameOverScreen());
        }
    }

    [PunRPC]
    private void RPCDie() 
    {
        GetComponent<Animator>().CrossFadeInFixedTime("death", 0.2f);
        GetComponent<vThirdPersonInput>().enabled = false;
        GetComponent<vThirdPersonController>().enabled = false;
        GetComponent<Beam>().enabled = false;
        if (photonView.isMine)
        {
            StartCoroutine(EnableGameOverScreen());
        }
    }
    
    IEnumerator EnableGameOverScreen()
    {
        yield return new WaitForSeconds(2.0f);
        transform.parent.Find("3rdPersonCamera").gameObject.SetActive(false);
        GameObject.Find("GameOver").transform.Find("GameOverCamera").gameObject.SetActive(true);
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
