// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/PSXTextureAlphaOneLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GeometricResolution ("Geometric Resolution (XY)", Range(5,1000)) = 100
        _Tint ("Tint", Color) = (1,1,1,1)

        _LightPos("Light Position",Vector)=(1,0,0)
        _LightCol("Light color",Color)=(1,1,1,1)
        _ShadowCol("Shadow color",Color)=(1,1,1,1)
    }
    SubShader
    {
        Tags {
            "RenderType"="Transparent"
            "Queue" = "Transparent" }
        LOD 100

        ColorMask RGB
        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 normal : NORMAL;
            };

            struct v2f
            {
                noperspective float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                noperspective float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _GeometricResolution;
            float4 _Tint;
            float4 _LightPos;
            float4 _LightCol;
            float4 _ShadowCol;

            v2f vert (appdata v)
            {
                v2f o;
                float illum;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float4 worldNormal = normalize(mul( unity_ObjectToWorld, v.normal ));
                
                o.vertex[0]=floor(_GeometricResolution*o.vertex[0]+0.5)/_GeometricResolution;
                o.vertex[1]=floor(_GeometricResolution*o.vertex[1]+0.5)/_GeometricResolution;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                illum=min( max(dot(worldNormal,_LightPos),0.001) , 1);
                o.color=v.color*_Tint*(_LightCol*illum+_ShadowCol*(1-illum));
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
