using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : MonoBehaviour
{
    public float coverDetectionDistance = 1f;
    public float coverMoveSpeed = 3f;
    public KeyCode coverButton = KeyCode.E;
    public KeyCode exitCoverButton = KeyCode.C;
    private bool isInCover = false;
    private Vector3 coverStartPos;
    private Vector3 coverOffset;
    private Collider coverCollider;
    private Rigidbody rb;
    public LayerMask groundLayer;
    private Vector3 lastCoverPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (isInCover)
        {
            Vector3 velocity = rb.velocity;
            velocity.y = 0f;
            rb.velocity = velocity;
        }

        if (isInCover)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 moveDirection = transform.right * horizontalInput * coverMoveSpeed * Time.deltaTime;
            Vector3 newPosition = rb.position + moveDirection;

            newPosition.x = Mathf.Clamp(newPosition.x, coverStartPos.x - coverCollider.bounds.size.x / 2f, coverStartPos.x + coverCollider.bounds.size.x / 2f);

            RaycastHit groundHit;
            if (Physics.Raycast(transform.position, Vector3.down, out groundHit, Mathf.Infinity, groundLayer))
            {
                newPosition.y = groundHit.point.y;
            }

            rb.MovePosition(newPosition);

            if (Input.GetKeyDown(exitCoverButton))
            {
                ExitCover();
            }
        }
        else
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, coverDetectionDistance))
            {
                if (hit.collider.CompareTag("Cover"))
                {
                    if (Input.GetKeyDown(coverButton))
                    {
                        SnapToCover(hit.collider);
                    }
                }
            }
        }
    }

    private void SnapToCover(Collider cover)
    {
        isInCover = true;
        coverCollider = cover;
        coverStartPos = transform.position;
        lastCoverPosition = coverStartPos;

        if (transform.position.x < coverCollider.bounds.center.x)
        {
            coverOffset = new Vector3(-coverCollider.bounds.size.x / 2f, 0f, 0f);
        }
        else
        {
            coverOffset = new Vector3(coverCollider.bounds.size.x / 2f, 0f, 0f);
        }

        Vector3 coverPositionBehindWall = coverCollider.bounds.center + coverOffset;

        RaycastHit groundHit;
        if (Physics.Raycast(transform.position, Vector3.down, out groundHit, Mathf.Infinity, groundLayer))
        {
            coverPositionBehindWall.y = groundHit.point.y;
        }

        rb.MovePosition(coverPositionBehindWall);

        transform.rotation = Quaternion.LookRotation(-coverCollider.transform.forward);
    }

    private void ExitCover()
    {
        isInCover = false;
        rb.MovePosition(lastCoverPosition);
    }
}
