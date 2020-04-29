using UnityEngine;

public class TimeDilationObject : MonoBehaviour
{
    void OnToggleTimeDilation()
    {
        MeshRenderer meshRenerer = GetComponent<MeshRenderer>();
        meshRenerer.enabled = !meshRenerer.enabled;
    }
}
