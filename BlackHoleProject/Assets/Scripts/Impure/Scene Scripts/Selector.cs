using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    // Most of this sampled from:
    // https://docs.microsoft.com/en-us/windows/mixed-reality/holograms-101

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        RaycastHit rayHitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out rayHitInfo))
        {

            meshRenderer.enabled = true;

            this.transform.position = rayHitInfo.point;

            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, rayHitInfo.normal);
        } else
        {
            meshRenderer.enabled = false;
        }
    }
}
