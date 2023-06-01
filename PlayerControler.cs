using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : MonoBehaviour
{
    private Vector2 PMouseIn;
    private Vector3 PMoveIn;
    private float xRot;

    [SerializeField] WallRun wallRun;
    [SerializeField] private LayerMask Floor;
    [SerializeField] private Transform GCheck;
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private Transform PCam;
    [Space]
    [SerializeField] private float raydistance;
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float JumpForce = 2.0f;
    [SerializeField] private float bufferRaydistance = 0.1f;


    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        raydistance = (GetComponent<CapsuleCollider>().height / 2) + bufferRaydistance;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            Rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);
        }
        PMoveIn = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PMouseIn = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MovePlayerCam();
    }

    private void MovePlayer()
    {
        Vector3 Moveit = transform.TransformDirection(PMoveIn) * Speed;
        Rb.velocity = new Vector3(Moveit.x, Rb.velocity.y, Moveit.z);

        RaycastHit hit;
        Ray GCheck = new Ray(transform.position, Vector3.down);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (Physics.Raycast(transform.position, -transform.up, out hit, raydistance))
            {


                Rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

            }
        }
    }
   
    private void MovePlayerCam()
    {
        xRot -= PMouseIn.y * Sensitivity;
        transform.Rotate(0f, PMouseIn.x * Sensitivity, 0f);
        PCam.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
