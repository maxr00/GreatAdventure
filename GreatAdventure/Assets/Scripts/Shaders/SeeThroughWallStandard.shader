Shader "Custom/SeeThroughWallStandard"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_StencilMask("Mask Layer", Range(0, 255)) = 1
		_Transparency("Transparency", Range(0,1)) = 1.0
		_FallOff("FallOff", Range(0,1)) = 1
    }
    SubShader
    {

		Tags
		{
			"Queue" = "Geometry-100" // write to stencil buffer before drawing any geometry to the screen
			"RenderType" = "Opaque"
			"ForceNoShadowCasting" = "True"
			"LightMode" = "Always"
		}
        LOD 200

		ColorMask 0 // dont write to color channels
		ZWrite Off // Don't write to the Depth buffer

		Stencil
		{
			Ref[_StencilMask] // ReferenceValue = 1
			Comp Always
			Pass Replace
		}

        CGPROGRAM
		#include "Dither.cginc"
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float4 screenPos;
			float3 worldPos;
        };
		half _Transparency;
        half _Glossiness;
        half _Metallic;
		half _FallOff;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

			float3 localPos = IN.worldPos - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;
			float dist = length(localPos);
			float transp_factor = dist == 0 ? 0 : 1 / pow((dist * dist), _FallOff);
			transp_factor = clamp(transp_factor, 0, 1);
			float transparency = transp_factor * _Transparency;

			// Screen-door transparency: Discard pixel if below threshold.
			if (transparency != 1.0f)
				Dither(IN.screenPos, transparency);

			//o.Albedo = float3(transparency, 0, 0);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
