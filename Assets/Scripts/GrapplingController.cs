using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpringJoint2D))]
public class GrapplingController : MonoBehaviour
{
    [Header("Grapple Settings")]
    public Camera mainCamera;
    public LayerMask grappleLayer;
    public float maxGrappleDistance = 15f;
    [Range(0.1f, 1f)] public float tensionFactor = 0.8f;

    private SpringJoint2D springJoint;
    private Vector2 targetPos;

    void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();

        // Strictly disable Unity's auto-calculations to prevent teleportation bugs
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.autoConfigureDistance = false;

        springJoint.enabled = false;

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
        }
    }

    void ReleaseGrapple()
    {
        springJoint.enabled = false;
    }
}