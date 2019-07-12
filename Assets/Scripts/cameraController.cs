// cameraController.cs - Dominick Aiudi 2019
//
// Attached to a camera.
// Takes specific key input for camera movement.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
	public float camSpeed, camRotationSpeed;

	[Header("Limits")]
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public float minZ;
	public float maxZ;

    void Start() { }

    void Update()
    {
    	///////////////////////////
    	// Movement on X/Z plane //
    	///////////////////////////
        if (Input.GetKey(KeyCode.W))
        	moveOnXZ(Vector3.forward);

        if (Input.GetKey(KeyCode.A))
        	moveOnXZ(Vector3.left);

        if (Input.GetKey(KeyCode.S))
        	moveOnXZ(Vector3.back);

        if (Input.GetKey(KeyCode.D))
        	moveOnXZ(Vector3.right);

        ////////////////////////////
        // Rotation around Y axis //
        ////////////////////////////
        if (Input.GetKey(KeyCode.Q))
        	rotateOnY(false);

        if (Input.GetKey(KeyCode.E))
        	rotateOnY(true);

        /////////////////////////
        // Movement on Y plane //
        /////////////////////////
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        	moveOnY(Vector3.up);

        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        	moveOnY(Vector3.down);
    }

    // Function for X and Z translations
    void moveOnXZ (Vector3 direction)
    {
    	float originalY = transform.position.y;
    	var rotationOffset = Quaternion.Euler(-transform.rotation.eulerAngles.x, 0.0f, 0.0f);
    	Vector3 vectTrans = rotationOffset * direction * camSpeed * (transform.position.y * 0.1f);

    	transform.Translate(vectTrans);

    	// Retain boundaries
        Vector3 check = transform.position;
        check.x = Mathf.Clamp(check.x, minX, maxX);
        check.y = originalY;
        check.z = Mathf.Clamp(check.z, minZ, maxZ);
        transform.position = check;
    }

    // Function for Y translations
    void moveOnY(Vector3 direction)
    {
    	float originalX = transform.position.x;
   		float originalZ = transform.position.z;
	   	var rotationOffset = Quaternion.Euler(-transform.rotation.eulerAngles.x, 0.0f, 0.0f);
    	Vector3 vectTrans = rotationOffset * direction * camSpeed;

    	transform.Translate(vectTrans);

    	// Retain boundaries
        Vector3 check = transform.position;
        check.x = originalX;
        check.y = Mathf.Clamp(check.y, minY, maxY);
        check.z = originalZ;
        transform.position = check;
    }

    // Function for Y rotation
    void rotateOnY(bool isRight)
    {
		transform.Rotate(0.0f, (isRight ? camRotationSpeed : -camRotationSpeed), 0.0f, Space.World);
    }
}
