using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BWEffect : MonoBehaviour
{

    public float intensity;
    public GameObject blackHoleObj;
    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Unlit/TestShader"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }
        Camera cam = this.gameObject.GetComponent<Camera>();

        Vector3 screenPos = cam.WorldToScreenPoint(blackHoleObj.transform.position);

        Vector2 norm = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);

        Debug.Log(cam.aspect);
        Shader.SetGlobalVector("_BlackHoleUV", norm);
        Shader.SetGlobalFloat("_GameTime", Time.time);
        Shader.SetGlobalFloat("_AspectRatio", cam.aspect);
        Graphics.Blit(source, destination, material);
    }
}