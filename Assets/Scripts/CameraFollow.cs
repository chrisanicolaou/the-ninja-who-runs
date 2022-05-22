using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0,1)]
    public float dampen;
    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    public Transform followTarget;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget.position.y > -5)
        {
        Vector3 destination = new Vector3(followTarget.position.x + 1.5f, followTarget.position.y + 1.5f, -10);
        // transform.position = Vector3.Lerp(transform.position, destination, dampen);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampen);
        }
            // Vector3 point = cam.WorldToViewportPoint(followTarget.position);
            // Vector3 delta = followTarget.position - cam.WorldToViewportPoint(new Vector3(0.5f, 0.5f, point.z));
            // Vector3 destination = transform.position + delta;
    }
}
