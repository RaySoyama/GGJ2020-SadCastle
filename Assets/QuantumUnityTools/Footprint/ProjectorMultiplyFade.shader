Shader "Quantum Shaders/ProjectorMultiplyFade" {
	Properties {
		_FootstepTexLeft ("Left Footstep", 2D) = "gray" {}
		_FootstepTexRight ("Right Footstep", 2D) = "gray" {}
		[PerRendererData] _LeftRight ("Left Or Right Foot", Range(0.0,1.0)) = 1
		[PerRendererData] _Alpha("Alpha Fade",Range(0.0,1)) = 1
	}
	Subshader {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

		Pass {
			ZWrite Off
			ColorMask RGB
			Blend DstColor Zero
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
			};
			
			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uvShadow = mul (unity_Projector, vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			sampler2D _FootstepTexLeft;
			sampler2D _FootstepTexRight;
			float _Alpha;
			float _LeftRight;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 texS;
				
				texS = (tex2Dproj(_FootstepTexRight, UNITY_PROJ_COORD(i.uvShadow)) * floor(_LeftRight + 0.5f)) +
					   (tex2Dproj(_FootstepTexLeft, UNITY_PROJ_COORD(i.uvShadow)) * floor( abs(_LeftRight - 1) + 0.5f)   );

				texS.a = _Alpha;

				UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(1,1,1,1));
				
				//uniform fade
				return texS + (1.0f - texS) * _Alpha;
			}
			ENDCG
		}
	}
}
