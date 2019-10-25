Shader "Custom/OutlineShader"
{
    Properties
    {
		_Color ("Main Color", Color) = (0.5, 0.5, 0.5, 1)
		_OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
		_Outline ("Outline Width", Range (0.002, 5.0)) = 5.0
        _MainTex ("Texture", 2D) = "white" {}
    }

	CGINCLUDE 
	#include "UnityCG.cginc"
	
	struct appdata
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f
	{
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	uniform float _Outline;
	uniform float4 _OutlineColor;

	v2f vert(appdata v)
	{
		// making a copy of vertex data but scaled according to normal direction
		v2f o;
		o.pos = float4(UnityObjectToViewPos(v.vertex), 1);

		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		return o;
	}
	ENDCG

    SubShader
    {
        Tags { "Queue"="Transparent" }
        
        Pass
		{
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Off
			ZWrite Off
			ZTest Always
			ColorMask RGB

			//Blend One OneMinusDstColor

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : COLOR
			{
				return i.color;
			}
			ENDCG
		}

		Pass
		{
			Name "BASE"
			ZWrite On
			ZTest LEqual			
			
			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}
			Lighting On
			SetTexture[_MainTex]
			{
				ConstantColor[_Color]
				Combine texture *constant
			}
			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}
    }
	Fallback "Diffuse"
}
