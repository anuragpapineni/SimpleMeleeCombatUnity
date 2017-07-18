using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    GameObject cameraTarget;
    public Vector3 offset = new Vector3(0, -1, -1);
    private float rotation = 0.0f;
    // Use this for initialization
    void Start () {
        cameraTarget = GameObject.FindGameObjectWithTag("Player");
        offset = transform.localPosition;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rotation -= Input.GetAxis("Mouse Y") * 5;
        rotation = Mathf.Clamp(rotation, -40.0f, 40.0f);
        transform.localPosition = Quaternion.AngleAxis(rotation,Vector3.right)*offset;
        if (transform.parent)
            transform.LookAt(transform.parent.position);
	}
}
