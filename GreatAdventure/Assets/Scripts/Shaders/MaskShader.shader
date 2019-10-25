Shader "Custom/MaskShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_StencilMask ("Mask Layer", Range(0, 255)) = 1
    }
    SubShader
    {
        LOD 100
        Tags 
		{ 
			"Queue" = "Geometry-100" // write to stencil buffer before drawing any geometry to the screen
			"RenderType" = "Opaque" 
			"ForceNoShadowCasting" = "True" 
		} 
		ColorMask 0 // dont write to color channels
		ZWrite Off // Don't write to the Depth buffer

        Pass
        {
			Stencil
			{
				Ref [_StencilMask] // ReferenceValue = 1
				Comp Always
				Pass Replace
			}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _CutOff;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
	FallBack "Diffuse"
}
