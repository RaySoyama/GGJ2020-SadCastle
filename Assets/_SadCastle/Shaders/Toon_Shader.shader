Shader "Custom/Inverted_Hull" {
	Properties
	{
		_OutlineThickness ("Outline Thickness", Range(0,2)) = 0.003
		_DarkenInnerLineColor("Darken Inner Line Color", Range(0, 1)) = 0.2

		_MainTex ("Texture", 2D) = "white" {}
		_ShadedTex ("Shaded Texture", 2D) = "white" {}
		_CombinedTex ("Combined", 2D) = "white" {}
	}
	SubShader
	{
		CGPROGRAM

		#pragma surface surf Custom fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _ShadedTex;
		sampler2D _CombinedTex;

		float _DarkenInnerLineColor;

		struct Input
		{
			float2 uv_MainTex;
			float3 vertexColor;
		};

		struct SurfaceOutputCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
			fixed Alpha;

			half3 BrightColor;
			half3 ShadowColor;
			half3 InnerLineColor;
			half ShadowThreshold;

			half SpecularIntensity;
			half SpecularSize;

		};

		float4 LightingCustom(SurfaceOutputCustom s, float3 lightDir, float atten)
		{
			float towardsLight = dot(lightDir, s.Normal);

			float4 col = float4(0, 0, 0, 1);

			towardsLight -= s.ShadowThreshold;
			half specStrength = s.SpecularIntensity;
			// if the vertex is facing away from the light
			if (towardsLight < 0)
			{
				if (towardsLight < - s.SpecularSize - 0.5f && specStrength <= 0.5f)
				{
					col.rgb = s.ShadowColor * (0.5f + specStrength);
				}
				else 
				{
					col.rgb = s.ShadowColor;
				}
			}
			else
			{
				if (s.SpecularSize < 1 && towardsLight * 1.8f > s.SpecularSize && specStrength >= 0.5f)
				{
					col.rgb = s.BrightColor * (0.5f + specStrength);
				}
				else
				{
					col.rgb = s.BrightColor;
				}
			}

			col.rgb *= s.InnerLineColor * _LightColor0.rgb;

			return col;
		}

		void surf(Input i, inout SurfaceOutputCustom o)
		{
			fixed4 col = tex2D(_MainTex, i.uv_MainTex);
			fixed4 shade = tex2D(_ShadedTex, i.uv_MainTex);
			fixed4 comb = tex2D(_CombinedTex, i.uv_MainTex);

			o.BrightColor = col.rgb;
			o.ShadowColor = col.rgb * shade.rgb;

			float clampedLineColor = comb.a;
			if (clampedLineColor < _DarkenInnerLineColor)
				clampedLineColor = _DarkenInnerLineColor;

			o.InnerLineColor = half3(clampedLineColor, clampedLineColor, clampedLineColor);

			o.ShadowThreshold = comb.g;
			o.ShadowThreshold *= i.vertexColor.r;
			o.ShadowThreshold = 1 - o.ShadowThreshold;

			o.SpecularIntensity = comb.r;
			o.SpecularSize = 1-comb.b;

			col *= shade;
		}
		ENDCG



		// render outline
		Pass
		{
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#include "UNITYCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			sampler2D _ShadedTex;

			float4 _MainTex_ST;

			float _OutlineThickness;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texCoord : TEXCOORD0;
				float4 vColor : COLOR;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float4 tex : TEXCOORD0;
				fixed4 vColor : COLOR0;
			};

			v2f vert(appdata v)
			{
				v2f o;

				o.position = UnityObjectToClipPos(v.vertex);
				float3 normA = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float3 norm = v.normal;
				float2 offset = TransformViewToProjection(normA.xy);
				o.position.xy += offset * _OutlineThickness;
				o.tex = v.texCoord;
				o.vColor = v.vColor;

				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET
			{
				fixed4 col = tex2D(_MainTex, i.tex.xy);
				fixed4 shade = tex2D(_ShadedTex, i.tex.xy);
				fixed4 dark = col * shade;
				dark *= 0.5f;
				dark.a = 1;
				return dark;
			}

		ENDCG
		}
	}
	FallBack "Diffuse"
}