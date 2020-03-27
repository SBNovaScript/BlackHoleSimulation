using UnityEngine;

public class OrbitObjectScript : MonoBehaviour
{
    public GameObject[] orbitTargets;
    public Vector3 initialVelocty;
    public float timeStep;
    public double mass = PhysicsConstants.EARTH_MASS;
    private Rigidbody rb;
    private ScaleManagerScript scaleManager;

    private void Start()
    {
        scaleManager = GameObject.Find("ScaleManager").GetComponent<ScaleManagerScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        initialVelocty.z = (float)((double)initialVelocty.z / scaleManager.velocityScale);
        initialVelocty.y = (float)((double)initialVelocty.y / scaleManager.velocityScale);

        if (float.IsInfinity(Mathf.Abs(initialVelocty.x)) || float.IsInfinity(Mathf.Abs(initialVelocty.y)) || float.IsInfinity(Mathf.Abs(initialVelocty.z)))
        {
            rb.velocity = Vector3.zero;
        }

        else if (float.IsNaN(Mathf.Abs(initialVelocty.x)) || float.IsNaN(Mathf.Abs(initialVelocty.y)) || float.IsNaN(Mathf.Abs(initialVelocty.z)))
        {
            rb.velocity = Vector3.zero;
        }

        else
        {
            rb.velocity = initialVelocty;
        }
        Debug.Log(rb.velocity);


    }


    private void FixedUpdate()
    {
        GameObject orbitTarget;
        for (int i = 0; i < orbitTargets.Length; i++)
        {
            orbitTarget = orbitTargets[i];
            Vector3 gravDir = orbitTarget.transform.position - gameObject.transform.position;
            double mag = (double)gravDir.magnitude;
            double gravForce = PhysicsCalculations.getGravitationalForceBasic(orbitTarget.GetComponent<OrbitObjectScript>().mass, mag * scaleManager.distanceScale / 1000);

            gravDir.Normalize();
            //gravForce = PhysicsCalculations.getAccelerationFromForce(mass, gravForce);

            float gravForce_f = PhysicsCalculations.ConvertDoubleToFloat(gravForce / scaleManager.velocityScale) * Time.smoothDeltaTime;
            gravDir.Scale(new Vector3(gravForce_f, gravForce_f, gravForce_f));
            rb.velocity += gravDir;

        }
        //initialVelocty = rb.velocity;
        //Debug.Log(rb.velocity);

    }
}
