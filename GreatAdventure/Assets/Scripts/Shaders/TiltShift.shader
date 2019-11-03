Shader "Custom/TiltShift"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_BlurAmount("BlurAmount",  Range(0,0.05)) = 0.025
		_BlurCenter("BlurCenter", Range(0, 5)) = 1.0
		_StandardDeviation("Standard Deviation", Range(0, 0.1)) = 0.02
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

		// Horizontal blur
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			#define PI 3.14159265359
			#define E 2.71828182846
			#define SAMPLES 10			

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
			half _BlurCenter;
			half _BlurAmount;
			float _StandardDeviation;
			sampler2D _MainTex;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				if (_StandardDeviation == 0)
					return tex2D(_MainTex, i.uv);

				float sum = 0;
				float invAspect = _ScreenParams.y / _ScreenParams.x;
				float4 col = 0;

				half amount = pow((i.uv.y * _BlurCenter) * 2.0 - 1.0, 2.0) * _BlurAmount;
				amount = clamp(amount, 0, _BlurAmount);
				for (float index = 0; index < SAMPLES; index++)
				{
					float offset = (index / (SAMPLES - 1) - 0.5) * amount * invAspect;
					float2 uv = i.uv + float2(offset, 0);
					//calculate the result of the gaussian function
					float stDevSquared = _StandardDeviation * _StandardDeviation;
					float gauss = (1 / sqrt(2 * PI * stDevSquared)) * pow(E, -((offset * offset) / (2 * stDevSquared)));
					sum += gauss;
					//multiply color with influence from gaussian function and add it to sum color
					col += tex2D(_MainTex, uv) * gauss;
				}
				col = col / sum;
				return col;// fixed4(amount, 0, 0, 1.0);
			}
			ENDCG
		}

		// Vertical blur
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			#define PI 3.14159265359
			#define E 2.71828182846
			#define SAMPLES 10	

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
			half _BlurCenter;
			half _BlurAmount;
			sampler2D _MainTex;
			float _StandardDeviation;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				if (_StandardDeviation == 0)
					return tex2D(_MainTex, i.uv);

				float sum = 0;
				float4 col = 0;
				
				half amount = pow((i.uv.y * _BlurCenter) * 2.0 - 1.0, 2.0) * _BlurAmount;
				amount = clamp(amount, 0, _BlurAmount);
				for (float index = 0; index < SAMPLES; index++) 
				{
					float offset = (index / (SAMPLES - 1) - 0.5) * amount;
					float2 uv = i.uv + float2(0, offset);
					//calculate the result of the gaussian function
					float stDevSquared = _StandardDeviation * _StandardDeviation;
					float gauss = (1 / sqrt(2 * PI * stDevSquared)) * pow(E, -((offset * offset) / (2 * stDevSquared)));
					sum += gauss;
					//multiply color with influence from gaussian function and add it to sum color
					col += tex2D(_MainTex, uv) * gauss;
				}
				col = col / sum;
				return col;
			}
			ENDCG
		}
    }
}
