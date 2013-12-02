using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	#region Variables ( private )

	[SerializeField]
	private float distanceFromCamera;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform followXFrom;
	[SerializeField]
	private Vector3 offset = new Vector3(0f, 1.5f, 0f);

	private Vector3 lookDir;
	private Vector3 targetPosition;

	// smoothing variables and damping variables ...
	private Vector3 velocityCamSmooth = Vector3.zero;
	[SerializeField]
	private float camSmoothDampTime = 0.1f;

	#endregion

	#region Variables / Properties ( public )
	
	#endregion

	// Use this for initialization
	void Start () {
		followXFrom = GameObject.FindWithTag("Player").transform;
	}

	void LateUpdate()
	{
		Vector3 characterOffset = followXFrom.position + offset;

		// berechnung der richtung der kamera zum spieler x eleminieren und normalisieren
		lookDir = characterOffset - this.transform.position;
		lookDir.y = 0.0f;
		lookDir.Normalize();
		Debug.DrawRay(this.transform.position, lookDir, Color.green);


		// kamera an die gewuenschte position setzen ...
		targetPosition = characterOffset + followXFrom.up * distanceUp - lookDir * distanceFromCamera;
		Debug.DrawRay(followXFrom.position, Vector3.up * distanceFromCamera, Color.red);
		Debug.DrawRay(followXFrom.position, -1f * followXFrom.forward * distanceFromCamera, Color.blue);
		Debug.DrawLine(followXFrom.position, targetPosition, Color.magenta);

		smoothPosition(this.transform.position, targetPosition);

		// die kamera auf den player richten
		transform.LookAt (followXFrom);
	}

	// Update is called once per frame
	void Update () {
	
	}

	#region Methods

	private void smoothPosition(Vector3 fromPos, Vector3 toPos)
	{
		// ein weicher uebergang zwichen der momentanen und der folgenden position
		this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	#endregion
}
