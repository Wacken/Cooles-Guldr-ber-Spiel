using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public Camera mainCamera;
    public List<GameObject> taggedToCamera; 
    public float RunningSpeed = 2.0f;
    public float RotationSpeedH = 4.0f;
    public float RotationSpeedV = 4.0f;
    public float maxSpeed = 5;
    public Rigidbody rb; 

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main; 
    }
    private void Update()
    {
            /*  //IF RAYCAST NEEDS TO BE IMPLEMENTED
        if (Input.GetMouseButtonDown(0))
        {
             

            #region InteractionRayCast 
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            int mask = (1 << 8);    //layer 8 is the "interactable" layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))      //hit registered
            { 
                if (hit.collider.tag == "Collidertag")  //if the mouse is above a nonDragging zone
                { 
                }

            }
            #endregion
            
        }*/
 
        float rotX = RotationSpeedH * Input.GetAxis("Mouse X") * Time.deltaTime * 100;
        float rotY = RotationSpeedV * Input.GetAxis("Mouse Y") * Time.deltaTime * 100;

        mainCamera.gameObject.transform.Rotate(new Vector3(-rotY, 0, 0.0f));
        transform.Rotate(new Vector3(0, rotX, 0.0f));



        Vector3 moveVector = new Vector3(0, 0, 0);
        if (Input.GetButton("Vertical"))
        {
            Vector3 tmpVector = transform.rotation * Vector3.forward * RunningSpeed * Input.GetAxis("Vertical");
            tmpVector.y = 0;
            moveVector += tmpVector;
        }
        if (Input.GetButton("Horizontal"))
        {
            Vector3 tmpVector = transform.rotation * Vector3.right * RunningSpeed * Input.GetAxis("Horizontal");
            tmpVector.y = 0;
            moveVector += tmpVector;
        }



        rb.AddForce(moveVector * 400);
        Vector2 flatSpeed = new Vector2(rb.velocity.x, rb.velocity.z);
        float totalSpeed = flatSpeed.magnitude;
        if (totalSpeed > maxSpeed)
        {

            rb.velocity = new Vector3(flatSpeed.x / totalSpeed * maxSpeed, rb.velocity.y, flatSpeed.y / totalSpeed * maxSpeed);    //cap the 2D velocity at a total of maxSpeed while keeping vertical speed 

        }



    } //end of update
}
