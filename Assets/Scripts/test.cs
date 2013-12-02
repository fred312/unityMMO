using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public int testificate = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Sets the rotation so that the transform's y-axis goes along the z-axis
		testificate++;

		if(testificate % 100 == 0)
		{
			transform.rotation = Quaternion.FromToRotation (Vector3.up, transform.forward);
		}
	}
}
