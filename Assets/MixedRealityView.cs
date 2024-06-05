using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    public Transform anchorTransform;  // FixedAnchorのTransformを設定
    public float passthroughRadius = 1.0f;

    private Material passthroughMaterial;

    void Start()
    {
        passthroughMaterial = new Material(Shader.Find("Custom/PassthroughShader"));

        // 初期パススルー領域の設定
        SetPassthroughRegion(anchorTransform.position, passthroughRadius);

        // オブジェクトにマテリアルを適用
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = passthroughMaterial;
        }
        else
        {
            Debug.LogError("Renderer not found on the object.");
        }
    }

    void SetPassthroughRegion(Vector3 worldPosition, float radius)
    {
        passthroughMaterial.SetVector("_PassthroughCenter", new Vector4(worldPosition.x, worldPosition.y, worldPosition.z, 0));
        passthroughMaterial.SetFloat("_PassthroughRadius", radius);

        Debug.Log("Passthrough region center set to: " + worldPosition);
        Debug.Log("Passthrough region radius set to: " + radius);
    }

    void Update()
    {
        if (anchorTransform != null)
        {
            SetPassthroughRegion(anchorTransform.position, passthroughRadius);
            Debug.Log("Anchor position updated to: " + anchorTransform.position);
        }
        else
        {
            Debug.LogError("AnchorTransform is not set");
        }
    }
}
