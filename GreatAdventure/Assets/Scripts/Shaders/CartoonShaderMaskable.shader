Shader "Custom/CartoonShaderMaskable"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Ramp("Ramp Texture", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_StencilMask ("Mask Layer", Range(0, 255)) = 2
    }
    SubShader
    {
        Tags 
		{ 
			"RenderType"="Opaque" 
			"Glowable" = "True"
			"Maskable" = "True"
		}
        LOD 200

		Stencil
		{
			Ref [_StencilMask] // ReferenceValue = 1
			Comp NotEqual // Only render pixels whose reference value differs from the value in the buffer.
		}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        //#pragma surface surf Standard fullforwardshadows
		#pragma surface surf Ramp

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _Ramp;

        struct Input
        {
            float2 uv_MainTex;
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

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
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
