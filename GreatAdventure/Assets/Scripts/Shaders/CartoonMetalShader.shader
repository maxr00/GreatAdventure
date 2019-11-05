Shader "Custom/Cartoon Metal"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Ramp ("Ramp Texture", 2D) = "white" {}

		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_SpecularSize ("Specular Cutoff", float) = 0.8
		_Brightness ("Specular Brightness", float) = 1.3

		_HighlightSize ("Highlight Size", float) = 0.9
		_HighlightColor ("Highlight Color", Color) = (1,1,1,1)

		_RimColor ("Rim Color", Color) = (1,1,1,1)
		_RimPower ("Rim Power", Range(0,20)) = 6
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

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
			float2 uv_Normal;
			float3 viewDir;
			float3 lightDir;
        };
		float  _SpecularSize;
		float _Brightness;
		float  _HighlightSize;
		float4 _HighlightColor;
		float4 _RimColor;
		float  _RimPower;
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

			float spec = dot(normalize(IN.viewDir + IN.lightDir), o.Normal);
			float cutOff = step(spec, _SpecularSize);

            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			c = c * cutOff; // base color

			// Specular
			float3 specularColor = _SpecColor.rgb * (1 - cutOff) * _Brightness;

			// Highlight
			float highlight = dot(IN.lightDir, o.Normal);
			float3 highlightColor = _HighlightColor * step(_HighlightSize, highlight);

            o.Albedo = c.rgb + specularColor + highlightColor;
			
			half rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal)); // Rim light factor
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
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
