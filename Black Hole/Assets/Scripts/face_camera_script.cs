using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_camera_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
    }
}
