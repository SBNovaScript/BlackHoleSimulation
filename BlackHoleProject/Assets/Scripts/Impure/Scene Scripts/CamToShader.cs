using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CamToShader : MonoBehaviour
{
    private Material MyMaterial;
    private Shader MyShade;
    public GameObject Disk;
    public GameObject BH;
    public GameObject[] targets;
    public GameObject[] Cy_targets;
    public Texture DiskTexture;

    [Range(0, 1)]
    public float rad;

    Camera MyCam;
    // Creates a private material used to the effect
    void Awake()
    {
        MyShade = Shader.Find("Unlit/RayTracer");
        MyMaterial = new Material(MyShade);

    }

    private void Start()
    {
        MyCam = this.gameObject.GetComponent<Camera>();
        //MyCam.RenderWithShader(Shader.Find("Unlit/RayTracer"), "ray_tracer");
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
            Vector4[] ptArray = new Vector4[targets.Length];
            float[] radArray = new float[targets.Length];

            Vector4[] ptArray_cy = new Vector4[Cy_targets.Length];
            Vector4[] shapeArray_cy = new Vector4[Cy_targets.Length];

            MyCam = this.gameObject.GetComponent<Camera>();

            for (int i = 0; i < targets.Length; i++)
            {
                ptArray[i] = targets[i].transform.position;
                radArray[i] = targets[i].transform.localScale.x;
            }

            for (int i = 0; i < Cy_targets.Length; i++)
            {
                ptArray_cy[i] = Cy_targets[i].transform.position;
                shapeArray_cy[i] = Cy_targets[i].transform.localScale;
            }

            MyMaterial.SetVector("_BHPos",BH.transform.position);
            Shader.SetGlobalFloat("_BHRad", (MyCam.transform.right * rad).magnitude);


            MyMaterial.SetInt("_NumSphereTargets", ptArray.Length);
            MyMaterial.SetVectorArray("_SphereTargetsPos", ptArray);
            MyMaterial.SetFloatArray("_SphereTargetsRad", radArray);

            MyMaterial.SetInt("_NumCyTargets", ptArray_cy.Length);
            MyMaterial.SetVectorArray("_CyTargetsPos", ptArray_cy);
            MyMaterial.SetVectorArray("_CyTargetsShape", shapeArray_cy);

            
            MyMaterial.SetFloat("_GameTime", Time.time);


            MyMaterial.SetTexture("_DiskTexture", DiskTexture);

            MyMaterial.SetMatrix("_CameraInverseProjection", MyCam.projectionMatrix.inverse);


            MyMaterial.SetInt("_Width", Screen.width);
            MyMaterial.SetInt("_Height", Screen.height);



            //MyMaterial.SetMatrix("_DiskMat", Disk.transform.worldToLocalMatrix);

            Graphics.Blit(source, destination, MyMaterial);
            return;
        }
    }
}