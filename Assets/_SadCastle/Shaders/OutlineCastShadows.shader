Shader "Ray Shaders/OutlineCastShadows"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Scale("Scale", Float) = 0.1
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque"}
			LOD 200
			
			Cull Front


			CGPROGRAM
			#pragma surface surf AidsCancer addshadow vertex:vert
			#pragma target 3.0        

			float _Scale;

			void vert(inout appdata_full v) 
			 {
				  //v.vertex.xyz += v.normal * _Scale;
					v.vertex.xyz = v.vertex.xyz + (v.normal * _Scale);
			}

			  half4 LightingAidsCancer(SurfaceOutput s, half3 lightDir, half atten) {
				  half NdotL = dot(s.Normal, lightDir);
				  half4 c;
				  c.rgb = s.Albedo * _LightColor0.rgb;	// *(NdotL * atten);
				  c.a = s.Alpha;

				  return c;
			  }



			struct Input
			{
				float2 uv_MainTex;
			};

			sampler2D _MainTex;
			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
				//o.Albedo = col.rgb;
				o.Emission = col.rgb * _Color;
			}

			ENDCG
		}


}
