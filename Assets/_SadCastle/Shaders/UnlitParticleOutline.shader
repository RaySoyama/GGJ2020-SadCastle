// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/UnlitParticle" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_OutColor("Outline Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Scale("Outline Scale", Float) = 0.1

	}
		SubShader{
			Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}
			//Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
			//LOD 100

			//ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			
		
			Pass {
				Tags {"LightMode" = "ForwardBase"}
				CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma multi_compile_fwdbase
					#pragma fragmentoption ARB_fog_exp2
					#pragma fragmentoption ARB_precision_hint_fastest

					#include "UnityCG.cginc"
					#include "AutoLight.cginc"

					struct v2f
					{
						float4	pos			: SV_POSITION;
						float2	uv			: TEXCOORD0;
						LIGHTING_COORDS(1,2)
					};

					float4 _MainTex_ST;

					v2f vert(appdata_tan v)
					{
						v2f o;

						o.pos = UnityObjectToClipPos(v.vertex);
						o.uv = TRANSFORM_TEX(v.texcoord, _MainTex).xy;
						TRANSFER_VERTEX_TO_FRAGMENT(o);
						return o;
					}

					sampler2D _MainTex;
					fixed4 _Color;

					fixed4 frag(v2f i) : COLOR
					{
						//fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
						fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
						return tex2D(_MainTex, i.uv) * atten * _Color;
					}
				ENDCG
			}

			Pass {
				Tags {"LightMode" = "ForwardAdd"}
				Blend One One
				CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#pragma multi_compile_fwdadd_fullshadows
					#pragma fragmentoption ARB_fog_exp2
					#pragma fragmentoption ARB_precision_hint_fastest

					#include "UnityCG.cginc"
					#include "AutoLight.cginc"

					struct v2f
					{
						float4	pos			: SV_POSITION;
						float2	uv			: TEXCOORD0;
						LIGHTING_COORDS(1,2)
					};

					float4 _MainTex_ST;

					v2f vert(appdata_tan v)
					{
						v2f o;

						o.pos = UnityObjectToClipPos(v.vertex);
						o.uv = TRANSFORM_TEX(v.texcoord, _MainTex).xy;
						TRANSFER_VERTEX_TO_FRAGMENT(o);
						return o;
					}

					sampler2D _MainTex;

					fixed4 frag(v2f i) : COLOR
					{
						fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
						//fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
						return tex2D(_MainTex, i.uv) * atten;
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
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _OutColor;
			float _Scale;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex + (v.normal * _Scale));
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);


				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return col * _OutColor;
			}
			ENDCG
		}
	}
		FallBack "VertexLit"
}
