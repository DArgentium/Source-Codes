using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask Grapple;
    public Transform shootPoint, Pcam, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(Pcam.position, Pcam.forward, out hit, maxDistance, Grapple))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.3f;
            joint.minDistance = distanceFromPoint * 0.1f;

            //Adjust these values to fit your game.
            joint.spring = 12f;
            joint.damper = 5f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = shootPoint.position;
        }
    }


    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, shootPoint.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
