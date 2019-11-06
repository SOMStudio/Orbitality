using UnityEngine;

public class ExtendedCustomMonoBehaviour : MonoBehaviour 
{
	protected bool didInit;
	protected bool canControl;
	
	protected int id;

	protected Transform myTransform;
	protected GameObject myGO;
	protected Rigidbody myBody;

	protected Vector3 tempVEC;
	protected Transform tempTR;
	
	void Start() {
		Init();
	}
	
	public virtual void Init() {
		if (!myTransform) {
			myTransform = transform;
		}
		if (!myGO) {
			myGO = gameObject;
		}
		if (!myBody) {
			myBody = GetComponent<Rigidbody> ();
		}

		didInit = true;
	}

	public virtual void SetId( int anId )
	{
		id = anId;
	}
}
