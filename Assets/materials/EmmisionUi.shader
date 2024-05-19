Shader "Unlit/EmmisionUi"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _Color("Tint Color", Color) = (1,1,1,1)
        _EmissionColor("Emission Color", Color) = (1,1,1,1)
        _EmissionPower("Emission Power", Range(0, 10)) = 1
    }
        SubShader
        {
            Tags { "Queue" = "Overlay" "RenderType" = "Transparent" "IgnoreProjector" = "True" "PreviewType" = "Plane" }
            LOD 100

            Pass
            {
                Name "Default"
                Tags { "LightMode" = "Always" }

                Cull Off
                Lighting Off
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                    float4 color : COLOR;
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    float2 texcoord : TEXCOORD0;
                    fixed4 color : COLOR;
                };

                sampler2D _MainTex;
                fixed4 _Color;
                fixed4 _EmissionColor;
                float _EmissionPower;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = v.texcoord;
                    o.color = v.color * _Color;
                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    half4 mainTex = tex2D(_MainTex, i.texcoord) * i.color;
                    half4 emission = _EmissionColor * _EmissionPower;
                    return mainTex + emission;
                }
                ENDCG
            }
        }
            FallBack "UI/Default"
}