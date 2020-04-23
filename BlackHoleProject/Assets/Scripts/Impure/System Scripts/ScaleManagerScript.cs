using UnityEngine;

public class ScaleManagerScript : MonoBehaviour
{
    public double timeScale;
    public double distanceScale;
    public double velocityScale;
    public float velocityScaleF;
    // Start is called before the first frame update
    void Start()
    {
        velocityScale = distanceScale / timeScale;
        velocityScaleF = (float) velocityScale;
    }

    // Update is called once per frame
    void Update()
    {
        velocityScale = distanceScale / timeScale;
        velocityScaleF = (float)velocityScale;
    }

    void OnReduceTime()
    {
        timeScale -= 40000;
    }
}
