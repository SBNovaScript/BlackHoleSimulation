Shader "Unlit/black_hole_hack"
	{
		Properties
		{
			_MainTex("Texture", 2D) = "white" {}
		}
			SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				float _GameTime;

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
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float2 _BlackHoleUV;
				float _AspectRatio;
				float _Rad;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					float u = o.uv[0];
					float v2 = o.uv[1];
					//o.uv = float2(u, 0.1 * cos(_GameTime) + v2);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{


					float2 uv = i.uv;
					float2 center = _BlackHoleUV;
					float rad = _Rad;

					float2 dir = center - uv;
					dir.x *= _AspectRatio;
					float len = length(dir);
					float2 new_dir = normalize(dir) * rad;
					float2 new_uv = uv + new_dir * rad / len;
					float4 col = tex2D(_MainTex, new_uv);

					//col *= min(lerp(-15.0, 1.0, len / (rad * 1.1)), 1.0);
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG
			}
		}
	}