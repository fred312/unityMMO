using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class slimeAI : MonoBehaviour {
	public float health = 300.0f;
	public float damageDealt = 10.0f;
	public GameObject deathEffect;
	public float jumpStrength = 30000.0f;
	public float scanRange = 15.0f;
	public List<string> attack = new List<string>(new string[]{"Player", "Weapon"});

	float damageCounter = 0;

	GameObject player;
	float counter = 0.0f;
	// Use this for initialization
	void Start () {
		//Resolution[] resolutions = Screen.resolutions;
		//Screen.SetResolution(resolutions[resolutions.Length-1].width, resolutions[resolutions.Length-1].height, true);
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag("Player");
		this.transform.LookAt (new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
		if(Vector3.Distance(this.transform.position, player.transform.position) < scanRange && counter < 1){
			this.rigidbody.AddForce(((this.transform.forward+this.transform.up).normalized)*jumpStrength*Time.deltaTime);
			counter = 40f;
		}
		counter-=Time.deltaTime*30f;
	}
	
	void OnCollisionEnter(Collision collision){
		if (canAttack(collision.gameObject.tag) && damageCounter < 1) { 
			collision.gameObject.SendMessage ("addDamage", damageDealt, SendMessageOptions.DontRequireReceiver);
			damageCounter = 100.0f;
		}
	}

	void OnCollisionStay(Collision collision){
		if (canAttack(collision.gameObject.tag) && damageCounter < 1) { 
			collision.gameObject.SendMessage ("addDamage", damageDealt, SendMessageOptions.DontRequireReceiver);
			damageCounter = 100.0f;
		}
		damageCounter-=Time.deltaTime*120;
	}

	IEnumerator die(){
		for(int n=0;n<30;n++){
			this.transform.localScale -= new Vector3(0.01f,0.01f,0.01f);
			yield return new WaitForSeconds(0.01f);
		}
		Instantiate (deathEffect, this.transform.position, this.transform.rotation);
		Destroy (this.gameObject, 0.1f);
	}

	void addDamage(float amount){
		this.health -= amount;
		if(health <= 0.0f){
			StartCoroutine(die());
		}
	}

	bool canAttack(string tag){
		return attack.Contains (tag) ? true : false;
	}
}