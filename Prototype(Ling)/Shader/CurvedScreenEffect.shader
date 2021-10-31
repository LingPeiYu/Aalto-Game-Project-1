Shader "PostEffect/CurvedScreenEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WeakFactor("Weak Factor",Range(1,5)) = 1
        _StretchFactor("Stretch Factor", Float) = 5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            //拉伸强度
            float _StretchFactor;
            //衰减强度
            float _WeakFactor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 dir = i.uv - float2(0.5f,0.5f);
                float power = pow(length(dir), _WeakFactor) * _StretchFactor;
                float2 Offset = power * normalize(dir);
                float2 uv = i.uv + Offset;
                return tex2D(_MainTex,uv);
            }
            ENDCG
        }
    }
}
