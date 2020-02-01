Shader "Ray Shaders/UnlitCastShadows"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
		Tags { "RenderType" = "Opaque"}
		LOD 200
		Lighting On

		CGPROGRAM
		#pragma surface surf AidsCancer addshadow 
		#pragma target 3.0

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
