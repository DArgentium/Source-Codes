using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [SerializeField] PlayerControler pControler;
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;
    [SerializeField] private float wallRunGrav;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;


    private Rigidbody Rb;

    bool wallLeft = false;
    bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }
    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }
    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right,out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right,out rightWallHit, wallDistance);
    }

    private void Update()
    {
        CheckWall();
        if (CanWallRun())
        {
            if (wallLeft)
            {
                startWallRun();
            }
            else if (wallRight)
            {
                startWallRun();
            }
            else
            {
                stopWallRun();
            }
        }
        else
        {
            stopWallRun();
        }

    }
    void startWallRun()
    {
        Rb.useGravity = false;
        Rb.AddForce(Vector3.down * wallRunGrav, ForceMode.Force);


        if (Input.GetKeyDown(KeyCode.Space))
        {
         
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);
                Rb.AddForce(wallRunJumpDirection * wallJumpForce, ForceMode.Force);
           
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);
               
            }
        }
    }
    void stopWallRun()
    {
        Rb.useGravity = true;
    }

}

