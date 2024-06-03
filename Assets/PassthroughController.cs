using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class PassthroughController : MonoBehaviour
{
    public Camera passthroughCamera;
    public RawImage passthroughImage;
    public Transform anchorTransform;  // FixedAnchorのTransformを設定
    public float passthroughRadius = 1.0f;

    private Material passthroughMaterial;

    void Start()
    {
        // PassthroughImageのマテリアルを取得
        passthroughMaterial = passthroughImage.material;

        // PassthroughカメラのRenderTextureを設定
        RenderTexture passthroughTexture = new RenderTexture(Screen.width, Screen.height, 24);
        passthroughCamera.targetTexture = passthroughTexture;
        passthroughImage.texture = passthroughTexture;

        // 初期パススルー領域の設定
        SetPassthroughRegion(anchorTransform.position, passthroughRadius);
    }

    void SetPassthroughRegion(Vector3 worldPosition, float radius)
    {
        // ワールド座標をそのまま使用
        passthroughMaterial.SetVector("_PassthroughCenter", new Vector4(worldPosition.x, worldPosition.y, worldPosition.z, 0));
        passthroughMaterial.SetFloat("_PassthroughRadius", radius);

        // デバッグログで位置を確認
        Debug.Log("Anchor position: " + worldPosition);
    }

    void Update()
    {
        // アンカーの位置を取得してパススルー領域を更新
        SetPassthroughRegion(anchorTransform.position, passthroughRadius);
    }
}
