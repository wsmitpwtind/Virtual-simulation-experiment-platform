Shader "Book_Shader/openBook"
{
    Properties
    {
    _MainTex("Texture", 2D) = "white" {}
    _SecTex("SecTex",2D) = "White"{}
    _Angle("Angle",Range(0,180)) = 0
    _WaveLength("WaveLength",Range(-1,1)) = 0

    }
        SubShader
    {

        Pass
        {
            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Angle;
            float _WaveLength;

            v2f vert(appdata v)
            {
                v2f o;
                v.vertex -= float4(5,0,0,0);
                float s;
                float c;
                sincos(radians(_Angle),s,c);
                float4x4 rotateMatrix = {
                    c ,s,0,0,
                    -s,c,0,0,
                    0 ,0,1,0,
                    0 ,0,0,1
                };
                v.vertex.y = sin(v.vertex.x * _WaveLength) * s;
                v.vertex = mul(rotateMatrix,v.vertex);
                v.vertex += float4(5,0,0,0);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }


            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }

         Pass
        {
                Cull Front
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
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
                float _Angle;
                float _WaveLength;

                sampler2D _SecTex;
                float4 _SecTex_ST;

                v2f vert(appdata v)
                {
                    v2f o;
                    v.vertex -= float4(5,0,0,0);
                    float s;
                    float c;
                    sincos(radians(_Angle),s,c);
                    float4x4 rotateMatrix = {
                        c ,s,0,0,
                        -s,c,0,0,
                        0 ,0,1,0,
                        0 ,0,0,1
                    };
                    v.vertex.y = sin(v.vertex.x * _WaveLength) * s;
                    v.vertex = mul(rotateMatrix,v.vertex);
                    v.vertex += float4(5,0,0,0);
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }


                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_SecTex, i.uv);
                    return col;
                }
                ENDCG
            }
    }
}

