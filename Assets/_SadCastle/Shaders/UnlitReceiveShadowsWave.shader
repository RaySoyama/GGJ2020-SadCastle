﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Hot Garbage, don't use

Shader "Ray Shaders/UnlitReceiveShadowsWave" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_WaveOrigin("Wave Origin", Range(0.0,1.0)) = 0.5
		_WaveIntensity("Wave Intesity", Float) = 0.1
		_WaveAmplitude("Wave Amp", Float) = 0.1
		_NoiseTex("Noise", 2D) = "white" {}
	}
		SubShader{
			//Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}
			Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
			//LOD 100

			ZWrite Off
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


					float _WaveOrigin;
					float _WaveIntensity;
					float _WaveAmplitude;
					sampler2D _NoiseTex;

					fixed4 frag(v2f i) : COLOR
					{
						//fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
						fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.

						float4 c = tex2D(_MainTex, i.uv) * atten * _Color;

						//if (i.uv.y < sin(i.uv.x * 20.0f))
						if (i.uv.y > _WaveOrigin + sin(i.uv.x * _WaveAmplitude * sin(i.uv.x * 0.01f * _WaveAmplitude + 0.1)) * (_WaveIntensity + (_SinTime.z + 0.2f) * 0.01f))
						{
							
							c = 0;
						}

						return c;
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

					sampler2D _MainTex;
					float4 _MainTex_ST;

					v2f vert(appdata_tan v)
					{
						v2f o;

						o.pos = UnityObjectToClipPos(v.vertex);
						o.uv = TRANSFORM_TEX(v.texcoord, _MainTex).xy;
						TRANSFER_VERTEX_TO_FRAGMENT(o);
						return o;
					}


					fixed4 frag(v2f i) : COLOR
					{
						fixed atten = LIGHT_ATTENUATION(i);	// Light attenuation + shadows.
						//fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.

						float4 c = tex2D(_MainTex, i.uv) * atten;
						

						return c;


					}
				ENDCG
			}
	}
		FallBack "VertexLit"
}
