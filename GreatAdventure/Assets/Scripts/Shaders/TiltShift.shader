Shader "Custom/TiltShift"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_BlurAmount("BlurAmount",  Range(0,10)) = 1.0
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
			half _BlurAmount;
			sampler2D _MainTex;
			float2 _MainTex_TexelSize;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				half amount = pow(i.uv.y * 2.0 - 1.0, 2.0) * _BlurAmount;
				fixed4 s = tex2D(_MainTex, i.uv) * 0.38774;
				s += tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x * 2 * amount, 0)) * 0.06136;
				s += tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x * amount, 0)) * 0.24477;
				s += tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x * -1 * amount, 0)) * 0.24477;
				s += tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x * -2 * amount, 0)) * 0.06136;

				return s;// fixed4(amount, 0, 0, 1.0);
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
			half _BlurAmount;
			sampler2D _MainTex;
			float2 _MainTex_TexelSize;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				half amount = pow(i.uv.y * 2.0 - 1.0, 2.0) * _BlurAmount;
				fixed4 s = tex2D(_MainTex, i.uv) * 0.38774;
				s += tex2D(_MainTex, i.uv + float2(0, _MainTex_TexelSize.y * 2 * amount)) * 0.06136;
				s += tex2D(_MainTex, i.uv + float2(0, _MainTex_TexelSize.y * amount)) * 0.24477;
				s += tex2D(_MainTex, i.uv + float2(0, _MainTex_TexelSize.y * -1 * amount)) * 0.24477;
				s += tex2D(_MainTex, i.uv + float2(0, _MainTex_TexelSize.y * -2 * amount)) * 0.06136;

				return s;// fixed4(amount, 0, 0, 1.0);
			}
			ENDCG
		}
    }
}
