using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpringJoint2D))]
[RequireComponent(typeof(LineRenderer))] // Mandates the component we just added
public class GrapplingController : MonoBehaviour
{
    [Header("Grapple Settings")]
    public Camera mainCamera;
    public LayerMask grappleLayer;
    public float maxGrappleDistance = 15f;
    [Range(0.1f, 1f)] public float tensionFactor = 0.8f;

    [Header("Momentum Settings")]
    public float releaseBoost = 5f; // Updated to your preferred value

    private SpringJoint2D springJoint;
    private Rigidbody2D rb;
    private LineRenderer lr; // The visual rope
    private Vector2 targetPos;

    void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.autoConfigureDistance = false;
        springJoint.enabled = false;

        // Initialize the rope as hidden
        lr.enabled = false;
        lr.positionCount = 2; // A line only needs a start and an end point

        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ReleaseGrapple();
        }
    }

    // LateUpdate runs after physics calculations, preventing visual jitter
    void LateUpdate()
    {
        if (springJoint.enabled)
        {
            lr.SetPosition(0, transform.position); // Point A: The Player
            lr.SetPosition(1, targetPos);          // Point B: The Anchor
        }
    }

    void FireGrapple()
    {
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxGrappleDistance, grappleLayer);

        if (hit.collider != null)
        {
            targetPos = hit.point;
            springJoint.connectedAnchor = targetPos;
            springJoint.enabled = true;
            springJoint.distance = Vector2.Distance(transform.position, targetPos) * tensionFactor;

            lr.enabled = true; // Show the rope
        }
    }

    void ReleaseGrapple()
    {
        if (springJoint.enabled)
        {
            springJoint.enabled = false;
            lr.enabled = false; // Hide the rope

            Vector2 currentTrajectory = rb.linearVelocity.normalized;
            rb.AddForce(currentTrajectory * releaseBoost, ForceMode2D.Impulse);
        }
    }
}