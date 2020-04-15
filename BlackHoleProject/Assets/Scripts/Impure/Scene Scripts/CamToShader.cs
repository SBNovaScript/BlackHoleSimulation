using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CamToShader : MonoBehaviour
{
    private Material MyMaterial;
    private Shader MyShade;

    public GameObject BH;
    [Range(0, 1)]
    public float rad;
    // Creates a private material used to the effect
    void Awake()
    {
        MyShade = Shader.Find("Unlit/RayTracer");
        MyMaterial = new Material(MyShade);
    }


    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!MyMaterial)
        {
            Graphics.Blit(source, destination);
            return;
        }
        else
        {
            Vector3[] FrustumCorners = new Vector3[4];
            Vector3[] NormCorners = new Vector3[4];
            Camera MyCam = this.gameObject.GetComponent<Camera>();


            MyCam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), MyCam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, FrustumCorners);
            for (int i = 0; i < 4; i++)
            {
                NormCorners[i] = MyCam.transform.TransformVector(FrustumCorners[i]);
                NormCorners[i] = NormCorners[i].normalized;
            }
            MyMaterial.SetVector("_BL", NormCorners[0]);
            MyMaterial.SetVector("_TL", NormCorners[1]);
            MyMaterial.SetVector("_TR", NormCorners[2]);
            MyMaterial.SetVector("_BR", NormCorners[3]);

            Shader.SetGlobalFloat("_Rad", (MyCam.transform.right * rad).magnitude);

            MyMaterial.SetVector("_BHPos", MyCam.worldToCameraMatrix.MultiplyPoint(BH.transform.position));

            Graphics.Blit(source, destination, MyMaterial);
            return;
        }
    }
}