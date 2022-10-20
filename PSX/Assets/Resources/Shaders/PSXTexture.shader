Shader "Unlit/PSXTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GeometricResolution ("Geometric Resolution (XY)", Range(5,1000)) = 100
        _tint ("Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                noperspective float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                noperspective float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _GeometricResolution;
            float4 _tint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                o.vertex[0]=floor(_GeometricResolution*o.vertex[0])/_GeometricResolution;
                o.vertex[1]=floor(_GeometricResolution*o.vertex[1])/_GeometricResolution;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color=v.color*_tint;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                // apply fog
                return col;
            }
            ENDCG
        }
    }
}
