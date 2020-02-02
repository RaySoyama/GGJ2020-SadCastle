Shader "Ray Shaders/ThunderEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_LineSize("Lightning Size", Range(0.0,1.0)) = 0.1
		_Speed("Noise Speed", Float) = 0.01
		_Noise("Noise Amount", Float) = 0.01

		_VerticalCut("Vertical Cutoff", Range(0,1)) = 1
    }
    SubShader
    {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100
		ZWrite On
		//ZTest Off
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;

			float _LineSize;
			float4 _Color;

			float _Speed;
			float _Noise;

			float _VerticalCut;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = (1,1,1,1);


				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				
				//determine noise
				fixed4 noise = tex2D(_NoiseTex, float2(i.uv.x * _NoiseTex_ST.x - _Time.y * _Speed, i.uv.y * _NoiseTex_ST.y + _Time.y * _Speed ));

				fixed4 grad = tex2D(_MainTex, float2(i.uv.x, i.uv.y));
				
				//Make Line
				col.a = step((0.5f - _LineSize / 2) + (noise.r - 0.5f) * _Noise, i.uv.x) * (1 - step((0.5f + _LineSize / 2) + (noise.r - 0.5f) * _Noise, i.uv.x)) * step(1.0f - _VerticalCut, i.uv.y);
				
				float left = (0.5f - _LineSize / 2) + (noise.r - 0.5f) * _Noise;
				float right = (0.5f + _LineSize / 2) + (noise.r - 0.5f) * _Noise;

				//if (i.uv.x < left)
				//{
				//	col.a = 0;
				//}
				//
				//if (i.uv.x > right)
				//{
				//	col.a = 0;
				//}


				////a += 1.0 / ((b-a) * amount );
				
				float yeet = (right - i.uv.x) / (right - left);


				col *= tex2D(_MainTex, yeet);
				//col.rgb = yeet;

				return col * _Color;
            }
            ENDCG
        }
    }
}
