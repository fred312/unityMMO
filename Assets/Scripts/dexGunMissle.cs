using UnityEngine;
using System.Collections;

public class dexGunMissle : MonoBehaviour {

	int count=0;
	ParticleEmitter emitter;

	void Start () {
		Destroy (this.gameObject, 60.0f);
		Physics.IgnoreCollision(this.collider, GameObject.FindGameObjectWithTag("Player").collider, true);
		emitter = GetComponent<ParticleEmitter>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Weapon" && collision.gameObject.tag != "PlayerPart"){
			this.GetComponent<ParticleEmitter>().transform.parent = null;
			this.GetComponent<ParticleAnimator>().transform.parent = null;
			this.GetComponent<ParticleRenderer>().transform.parent = null;
			this.GetComponent<ParticleSystem>().transform.parent = null;
			Destroy (gameObject, 0f);
		}
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerPart") {	
			Physics.IgnoreCollision(collision.collider, collider); 
		}
		if(collision.gameObject.tag == "Enemy"){
			collision.gameObject.SendMessage("addDamage", this.rigidbody.velocity.magnitude, SendMessageOptions.DontRequireReceiver);
		}
		Debug.Log(collision.gameObject.name);
	}
}