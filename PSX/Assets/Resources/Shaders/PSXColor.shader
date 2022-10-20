Shader "Unlit/PSXColor"
{
    Properties
    {
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
                float4 color : COLOR;
            };

            struct v2f
            {
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                noperspective float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _tint;
            float _GeometricResolution;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex[0]=floor(_GeometricResolution*o.vertex[0])/_GeometricResolution;
                o.vertex[1]=floor(_GeometricResolution*o.vertex[1])/_GeometricResolution;
                o.color=v.color*_tint;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = i.color;
                // apply fog
                return col;
            }
            ENDCG
        }
    }
}
