Shader "Unlit/RayTracer"
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
			sampler2D _MainTex;

			float4 _BL;
			float4 _TL;
			float4 _TR;
			float4 _BR;
			float4 _BHPos;
			float _Rad;


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

			//https://gist.github.com/wwwtyro/beecc31d65d1004f5a9d
			float raySphereIntersect(float3 r0, float3 rd, float3 s0, float sr) {
				// - r0: ray origin
				// - rd: normalized ray direction
				// - s0: sphere center
				// - sr: sphere radius
				// - Returns distance from r0 to first intersecion with sphere,
				//   or -1.0 if no intersection.
				float a = dot(rd, rd);
				float3 s0_r0 = r0 - s0;
				float b = 2.0 * dot(rd, s0_r0);
				float c = dot(s0_r0, s0_r0) - (sr * sr);
				if (b*b - 4.0*a*c < 0.0) {
					return -1.0;
				}
				return (-b - sqrt((b*b) - 4.0*a*c)) / (2.0*a);
			}


			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}



		
			fixed4 frag(v2f i) : SV_Target
			{
				int numSteps = 5000;
				float stepSize = 0.01;
				float3 bh = _BHPos;
				bh.z *= -1;

				float4 NormalLeft;
				float4 NormalRight;
				float4 NormalH;
				fixed4 col = tex2D(_MainTex, i.uv);
				NormalLeft = lerp(_BL, _TL,i.uv.y);
				NormalRight = lerp(_BR, _TR, i.uv.y);
				NormalH = lerp(NormalLeft, NormalRight,i.uv.x);
				NormalH = normalize(NormalH);

				float3 stepH = NormalH.xyz * stepSize;
				float3 pos = float3(0, 0, 0);

				for (int i = 0; i < numSteps; i++)
				{
					pos += stepH;
					if (distance(pos, bh) < _Rad)
					{
						col = float4(1, 1, 1, 1);
						break;
					}
				}

				return col;
			}
				ENDCG
		}
	}
}