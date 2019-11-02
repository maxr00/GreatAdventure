﻿Shader "Custom/CartoonShaderDitherTransparency"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Ramp("Ramp Texture", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_StencilMask("Mask Layer", Range(0, 255)) = 2
		_Transparency("Transparency", Range(0,1)) = 1.0
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" "Glowable" = "True"}
			LOD 200

			Stencil
			{
				Ref[_StencilMask] // ReferenceValue = 1
				Comp NotEqual // Only render pixels whose reference value differs from the value in the buffer.
			}

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			//#pragma surface surf Standard fullforwardshadows
			#pragma surface surf Ramp noforwardadd

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _Ramp;

			struct Input
			{
				float2 uv_MainTex;
				float4 screenPos;
			};
			half _Transparency;
			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutput o)
			{
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;

				// Screen-door transparency: Discard pixel if below threshold.
				float4x4 thresholdMatrix =
				{ 1.0 / 17.0,  9.0 / 17.0,  3.0 / 17.0, 11.0 / 17.0,
				  13.0 / 17.0,  5.0 / 17.0, 15.0 / 17.0,  7.0 / 17.0,
				   4.0 / 17.0, 12.0 / 17.0,  2.0 / 17.0, 10.0 / 17.0,
				  16.0 / 17.0,  8.0 / 17.0, 14.0 / 17.0,  6.0 / 17.0
				};
				float4x4 _RowAccess = { 1,0,0,0, 0,1,0,0, 0,0,1,0, 0,0,0,1 };
				float2 pos = IN.screenPos.xy / IN.screenPos.w;
				pos *= _ScreenParams.xy; // pixel position

				clip(_Transparency - thresholdMatrix[fmod(pos.x, 4)] * _RowAccess[fmod(pos.y, 4)]);
			}

			half4 LightingRamp(SurfaceOutput s, half3 lightDir, half atten)
			{
				half NdotL = dot(s.Normal, lightDir);
				half ramp = clamp(NdotL, 0.0, 1.0);

				float3 tex = tex2D(_Ramp, float2(ramp, 0.5)).rgb;

				half4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * tex * atten;
				c.a = s.Alpha;
				return c;
			}
			ENDCG
		}
			FallBack "Diffuse"
}