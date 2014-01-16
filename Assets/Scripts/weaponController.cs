using UnityEngine;
using System.Collections;

public class weaponController : MonoBehaviour {
	public AudioClip[] clip;
	public GameObject bullet;
	GameObject bulletReference;
	float count=0;
	Vector3 mouse;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1") && count < 1){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane hPlane = new Plane(-Vector3.forward, Vector3.zero);
			float distance = 0; 
			audio.PlayOneShot(clip[(int)Random.Range(0, clip.Length)]);
			bulletReference = (GameObject)Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 0.0f), this.transform.rotation);
			if (hPlane.Raycast(ray, out distance)){
				bulletReference.transform.LookAt (new Vector3(ray.GetPoint(distance).x, ray.GetPoint(distance).y, 0.0f));
			}
			bulletReference.rigidbody.velocity = bulletReference.transform.forward * 100.0f;

			count = 40f;
		} 
		count-=Time.deltaTime*120;
	}
}
