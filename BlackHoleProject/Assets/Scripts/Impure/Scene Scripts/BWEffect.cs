using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BWEffect : MonoBehaviour
{
    [Range(0,1)]
    public float rad;
    public GameObject blackHoleObj;
    private Material material;

    public GameObject targetText;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Unlit/black_hole_hack"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Camera cam = this.gameObject.GetComponent<Camera>();

        

        Vector3 Edge = blackHoleObj.transform.position + cam.transform.right * rad;
        Vector3 screenPos = cam.WorldToScreenPoint(blackHoleObj.transform.position);
        Vector3 screenPosEdge = cam.WorldToScreenPoint(Edge);

        targetText.GetComponent<RectTransform>().position = new Vector3(screenPos.x, screenPos.y, 0);

        Vector2 norm = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
        Vector2 normEdge = new Vector2(screenPosEdge.x / Screen.width, screenPosEdge.y / Screen.height);

        Debug.Log(screenPos);

        //Debug.Log(normEdge);
        Shader.SetGlobalVector("_BlackHoleUV", norm);
        Shader.SetGlobalFloat("_BlackHoleUVRadius", Time.time);
        Shader.SetGlobalFloat("_GameTime", Time.time);
        Shader.SetGlobalFloat("_AspectRatio", cam.aspect);
        Shader.SetGlobalFloat("_Rad", (norm - normEdge).magnitude);
        Graphics.Blit(source, destination, material);
    }
}