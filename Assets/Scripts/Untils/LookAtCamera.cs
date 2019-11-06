using UnityEngine;

[AddComponentMenu("Utility/Look At Camera")]

public class LookAtCamera : ExtendedCustomMonoBehaviour {
	[Header("Settings")]
	[SerializeField]
	private Transform followTarget;
	[SerializeField]
	private Vector3 targetOffset = Vector3.zero;
	[SerializeField]
	private float moveSpeed = 0f;
	
	void Update() {
		Quaternion turgRotate = Quaternion.LookRotation ((followTarget.position + targetOffset) - myTransform.position);
		Quaternion myRotate = myTransform.rotation;

		if ((myRotate.eulerAngles - turgRotate.eulerAngles).magnitude > 0.1f) {
			if (moveSpeed == 0) {
				myTransform.LookAt (followTarget.position + targetOffset);
			} else {
				myTransform.rotation = Quaternion.Slerp (myRotate, turgRotate, moveSpeed * Time.deltaTime);
			}
		}
	}
	
	public override void Init ()
	{
		base.Init ();

		followTarget = Camera.main.transform;
		
		canControl = true;
	}

	public Transform FollowTarget {
		get { return followTarget; }
		set { followTarget = value; }
	}

	public Vector3 TargetOffset {
		get { return targetOffset; }
		set { targetOffset = value; }
	}

	public float MoveSpeed {
		get { return moveSpeed; }
		set { moveSpeed = value; }
	}
}
