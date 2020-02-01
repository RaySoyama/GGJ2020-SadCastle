Shader "Quantum Shaders/Greybox Shader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			float4 _MainTex_ST;


			struct Input {
				float2 uv_MainTex;
				float4 vertexColor : COLOR;
				float3 worldNormal;	
				float3 worldPos;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o) {
				// Albedo comes from a texture tinted by color
				float2 UV;

				//UV = IN.worldPos.yz * step(0.5f, abs(IN.worldNormal.x));
				//UV += IN.worldPos.xy * step(0.5f, abs(IN.worldNormal.z));
				//UV += IN.worldPos.xz * step(0.5f, abs(IN.worldNormal.y));


				UV = IN.worldPos.yz * step(max(abs(IN.worldNormal.y), abs(IN.worldNormal.z)), abs(IN.worldNormal.x));
				UV += IN.worldPos.xy * step(max(abs(IN.worldNormal.y), abs(IN.worldNormal.x)), abs(IN.worldNormal.z));
				UV += IN.worldPos.xz * step(max(abs(IN.worldNormal.x), abs(IN.worldNormal.z)), abs(IN.worldNormal.y));


				fixed4 c = tex2D(_MainTex, UV + _MainTex_ST.zw) * _Color * IN.vertexColor;
			
				



				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Standard"
}
