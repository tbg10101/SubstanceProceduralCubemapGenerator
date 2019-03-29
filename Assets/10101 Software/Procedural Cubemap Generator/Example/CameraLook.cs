using UnityEngine;

/*
	Don't use this for any real products, it is quick, simple, and un-documented. 
*/
public class CameraLook : MonoBehaviour {
	private Vector3 lookDelta = Vector3.zero;

	public float xSensitivity = 0.01f;
	public float ySensitivity = 0.01f;

	void Update () {
		lookDelta.x = Input.GetAxis("Mouse Y") * ySensitivity;
		lookDelta.y = Input.GetAxis("Mouse X") * xSensitivity;

		transform.Rotate(lookDelta);
	}
}
