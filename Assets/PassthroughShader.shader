Shader "Custom/PassthroughShader"
{
    Properties
    {
        _PassthroughCenter ("Passthrough Center", Vector) = (0, 0, 0, 0)
        _PassthroughRadius ("Passthrough Radius", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 worldPos : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _PassthroughCenter;
            float _PassthroughRadius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 passthroughCenter = _PassthroughCenter.xyz;
                float dist = distance(i.worldPos, passthroughCenter);
                if (dist < _PassthroughRadius)
                {
                    return fixed4(0, 0, 0, 0); // パススルー領域を透明に
                }
                return fixed4(1, 1, 1, 1); // 確認のために白
            }
            ENDCG
        }
    }
}
