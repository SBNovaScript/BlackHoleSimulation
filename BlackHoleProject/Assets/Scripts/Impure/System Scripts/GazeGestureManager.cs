using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    // This script will track where the user is looking, and store the viewed hologram.
    // Most of this sampled from:
    // https://docs.microsoft.com/en-us/windows/mixed-reality/holograms-101

    public static GazeGestureManager Instance {get; private set;}

    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    private void Awake()
    {
        Instance = this;

        recognizer = new GestureRecognizer();
        recognizer.Tapped += (args) =>
        {
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
            }
        };
        recognizer.StartCapturingGestures();
    }

    private void Update()
    {
        GameObject oldFocusObject = FocusedObject;

        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        RaycastHit rayHitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out rayHitInfo))
        {
            FocusedObject = rayHitInfo.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }

        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}
