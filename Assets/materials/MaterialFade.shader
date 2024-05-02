Shader "Custom/MaterialFade"
{
    Properties
    {
        _ColorA("Color A", Color) = (1,1,1,1)
        _ColorB("Color B", Color) = (1,1,1,1)
        _ColorC("Color C", Color) = (1,1,1,1) // Новое свойство для третьего цвета
        _Speed("Color Change Speed", Range(0, 10)) = 1.0
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
        };

        float4 _ColorA;
        float4 _ColorB;
        float4 _ColorC; // Переменная для третьего цвета
        float _Speed;
        sampler2D _MainTex;

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Вычисляем промежуточный цвет между цветами A и B и C
            float t = 0.5 + 0.5 * sin(_Time.y * _Speed); // Ставим в центр периода
            float4 newColor = lerp(lerp(_ColorA, _ColorB, t), _ColorC, t); // Интерполяция между A, B и C

            // Учитываем альбедо объекта
            float3 albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;

            // Устанавливаем цвет материала с учетом альбедо
            o.Albedo = albedo * newColor.rgb;
            o.Alpha = newColor.a;
        }
        ENDCG
    }
        FallBack "Diffuse"
}
