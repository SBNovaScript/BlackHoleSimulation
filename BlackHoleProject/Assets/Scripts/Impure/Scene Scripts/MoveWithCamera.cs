using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCamera : MonoBehaviour
{

    public Transform cameraPos;
    public Vector3 offset = new Vector3(0, -0.1f, -0.7f);

    void Update()
    {
        transform.position = cameraPos.position - offset;
    }
}
