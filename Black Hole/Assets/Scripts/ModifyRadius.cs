using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyRadius : MonoBehaviour
{
    public float radius;

    private void Update()
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }

}
