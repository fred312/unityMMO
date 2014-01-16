using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	private Animator animator;
	public float speedDamp = 10f;
	public float turningDamp = 25f;
	public float startHealth = 100.0f;
	public float speedMul = 1f;

	float maxSpeed = 5.667749f;
	float health;
	Vector3 mouse;
	GameObject arm;
	Quaternion before;
	Quaternion oldRot;
	RaycastHit hit;
	float speedRot = 1.0f;
	GameObject hPlane;
	bool dead = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		arm = GameObject.Find ("RightArmPlaceholder");
		health = startHealth;
		Camera.main.SendMessage("health", health, SendMessageOptions.DontRequireReceiver);
		hPlane = GameObject.Find ("aimCastPlane");
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics.Raycast (this.transform.position, Vector3.left, 0.2f) || Physics.Raycast (this.transform.position, -Vector3.left, 0.2f)){
			this.rigidbody.velocity = new Vector3(0.0f, this.rigidbody.velocity.y, 0.0f);
		}
	}

	void LateUpdate(){
		if(Input.GetKey (KeyCode.A)){
			this.transform.forward =  Vector3.left;
		}
		if(Input.GetKey (KeyCode.D)){
			this.transform.forward = -Vector3.left;
		}
		if(Input.GetKey (KeyCode.Space) && isGrounded()){
			this.rigidbody.velocity = new Vector3(this.rigidbody.velocity.x, 10.0f, this.rigidbody.velocity.z);
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane hPlane = new Plane(-Vector3.forward, Vector3.zero);
		float distance = 0; 
		if (hPlane.Raycast(ray, out distance)){
			arm.transform.LookAt (new Vector3(ray.GetPoint(distance).x, ray.GetPoint(distance).y, arm.transform.position.z));
		}
	}

	void FixedUpdate(){
		if(Input.GetKey (KeyCode.LeftShift)){
			speedMul = 1.0f;
		}
		else{
			speedMul = 0.7f;
		}
		animator.SetFloat("Speed", (float)maxSpeed*speedMul*Mathf.Sqrt(Mathf.Pow (Input.GetAxis("Horizontal"), 2.0f)));
	}
	
	IEnumerator die(){
		yield return new WaitForSeconds(0.001f);
		this.animator.SetBool("Die", false);
		yield return new WaitForSeconds(0.86f);
		this.GetComponent<CapsuleCollider>().direction = 2;
		this.GetComponent<CapsuleCollider>().center = new Vector3(0.0f, 0.16f, 0.0f);

		Destroy (GameObject.FindGameObjectWithTag("Weapon").GetComponent<MonoBehaviour>());
		Destroy (this);
	}

	void addDamage(float amount){
		this.health -= amount;
		Camera.main.SendMessage("health", health, SendMessageOptions.DontRequireReceiver);
		if(this.health <= 0 && dead != true){
			GameObject.Find ("Beta:RightArm").transform.parent = GameObject.Find ("Beta:RightShoulder").transform;
			this.animator.SetBool("Die", true);
			dead = true;
			StartCoroutine(die());
		}
	}

	bool isGrounded ()
	{
		return (Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y +0.05f, this.transform.position.z), -Vector3.up,out hit, 0.1f) == true && hit.collider.tag != "Bullet") ? true : false;
	}
}
