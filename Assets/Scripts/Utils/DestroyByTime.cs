using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	[SerializeField]
	private float destroyTime;

	// main events
	void Start () {
		Destroy (gameObject, destroyTime);
	}

}
