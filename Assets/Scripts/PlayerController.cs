using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Camera mainCamera;
    public List<GameObject> taggedToCamera;
    public float RunningSpeed = 2.0f;
    public float RotationSpeedH = 4.0f;
    public float RotationSpeedV = 4.0f;
    public float maxSpeed = 5;
    public Rigidbody rb;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        float rotX = RotationSpeedH * Input.GetAxis("Mouse X") * Time.deltaTime * 100;
        float rotY = RotationSpeedV * Input.GetAxis("Mouse Y") * Time.deltaTime * 100;

        mainCamera.gameObject.transform.Rotate(new Vector3(-rotY, 0, 0.0f));
        transform.Rotate(new Vector3(0, rotX, 0.0f));






    }
}
