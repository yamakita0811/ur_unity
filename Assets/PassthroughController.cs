using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    public Transform anchorTransform;  // FixedAnchorのTransformを設定
    public float passthroughRadius = 1.0f;

    private Material passthroughMaterial;

    void Start()
    {
        // Passthroughシェーダーを適用したマテリアルを作成
        passthroughMaterial = new Material(Shader.Find("Custom/PassthroughShader"));

        // 初期パススルー領域の設定
        SetPassthroughRegion(anchorTransform.position, passthroughRadius);
    }

    void SetPassthroughRegion(Vector3 worldPosition, float radius)
    {
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

    void OnRenderObject()
    {
        // パススルーマテリアルを使用してシーンをレンダリング
        passthroughMaterial.SetPass(0);
        Graphics.DrawProceduralNow(MeshTopology.Points, 1);
    }
}
