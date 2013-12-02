using UnityEngine;
using System.Collections;

public class CharacterConstollerLogic : MonoBehaviour {

	#region Variables ( private )
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float directionDampTime = .05f;
	[SerializeField]
	private ThirdPersonCamera gamecam;
	[SerializeField]
	private float directionSpeed = 3.0f;

	//private global variables
	private float speed = 0.0f;
	private float direction = 0.0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	#endregion

	#region Variables / Properties ( public )

	#endregion

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		if(animator.layerCount >= 2)
		{
			animator.SetLayerWeight(1, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(animator)
		{
			// variablen durch keyboard / controller input setzen
			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");

			StickToWorldspace(this.transform, gamecam.transform, ref direction, ref speed);

			animator.SetFloat("Speed", speed);
			animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);

		}
	}

	void FixedUpdate()
	{
		//if( isInLocomotion() && (( direction >= 0 && horizontal >= 0 ) || ( direction < 0 && horizontal < 0 )))
		//{
		//	Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * (horizontal < 0f ? -1f : 1f), 0f) Mathf.));
		//}
	}

	#region Methods

	public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
	{
		Vector3 rootDirection = root.forward;
		Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

		speedOut = stickDirection.sqrMagnitude;

		// Kamera rotation
		Vector3 cameraDirection = camera.forward;
		cameraDirection.y = 0.0f; // y eliminieren
		Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);

		// input in koordinaten konvertieren
		Vector3 moveDirection = referentialShift * stickDirection;
		Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

		Debug.DrawRay(new Vector3(root.position.x, root.position.y +2f, root.position.z), moveDirection, Color.green);
		Debug.DrawRay(new Vector3(root.position.x, root.position.y +2f, root.position.z), axisSign, Color.red);
		Debug.DrawRay(new Vector3(root.position.x, root.position.y +2f, root.position.z), rootDirection, Color.magenta);
		Debug.DrawRay(new Vector3(root.position.x, root.position.y +2f, root.position.z), stickDirection, Color.blue);

		float angleRootToMove = Vector3.Angle (rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

		angleRootToMove /= 180f;

		directionOut = angleRootToMove * directionSpeed;
	}

	#endregion
}