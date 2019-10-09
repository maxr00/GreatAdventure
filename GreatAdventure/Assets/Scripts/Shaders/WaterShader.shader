Shader "Unlit/WaterShader"
{
    Properties
    {
		_Tint("Tint", Color) = (1, 1, 1, .5)
        _MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "black" {}
		_UVScale("Scale", float) = 1
		_Amplitude("Wave Amplitude", float) = 1
		_Speed("Speed", float) = 1
		_Foam("Foam Thickness", float) = 1
		_Amount("Distortion Amount", float) = 0.03
		_DistortSpeed("Distortion Speed", float) = 2
		_DistortScale("Distortion Scale", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
				float4 worldPos : TEXCOORD2;
            };

			uniform sampler2D _CameraDepthTexture;

            sampler2D _MainTex;
			sampler2D _NoiseTex;
			float _UVScale;
			float _Amplitude;
			float _Speed;
			float _Amount;
			float _Foam;
			float _DistortSpeed;
			float _DistortScale;
            float4 _MainTex_ST;
			float4 _Tint;

            v2f vert (appdata v)
            {
                v2f o;

				v.vertex.y += _Amplitude * sin(_Time.z * _Speed + (v.vertex.x * v.vertex.z));
                o.vertex = UnityObjectToClipPos(v.vertex);
				
				o.screenPos = ComputeScreenPos(o.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                o.uv = TRANSFORM_TEX(float2(v.uv.x * _UVScale, v.uv.y * _UVScale), _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 distortion = tex2D(_NoiseTex, i.worldPos.xz / _DistortScale - _Time.x * _DistortSpeed).xy;

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv + distortion * _Amount);

				half depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)));
				half4 foam = 1 - saturate(_Foam * (depth - i.screenPos.w));//compare depth and screen pos

				col += foam * _Tint;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
