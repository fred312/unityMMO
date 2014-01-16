using UnityEngine;
using System.Collections;

public class cameraFollowScript : MonoBehaviour {
	public GameObject objectToFollow;
	public float distance = 20.0f;
	public float lerpFloat = 20.0f;
	public float yDifference = 1.0f;
	public float  scrollLerp = 2.0f;
	public float minDistance = 3.0f;

	float newDistance;
	// Use this for initialization
	void Start () {
		newDistance = distance;
		this.transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y + yDifference,objectToFollow.transform.position.z - distance);
	}

	void Update(){
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
				newDistance++;
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			if(newDistance >= minDistance){
				newDistance--;
			}
		}
		if(distance != newDistance){
			distance = Mathf.Lerp (distance, newDistance, scrollLerp*Time.deltaTime);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = Vector3.Lerp (this.transform.position, new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y+yDifference,objectToFollow.transform.position.z - distance), lerpFloat*Time.deltaTime);
	}
}
