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
			float _BHRad;

			float4x4 _DiskMat;

			int _NumSphereTargets;
			float4 _SphereTargetsPos[4];
			float _SphereTargetsRad[4];


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

				float4 NormalLeft;
				float4 NormalRight;
				float4 NormalH;
				fixed4 col = tex2D(_MainTex, i.uv);
				NormalLeft = lerp(_BL, _TL,i.uv.y);
				NormalRight = lerp(_BR, _TR, i.uv.y);
				NormalH = lerp(NormalLeft, NormalRight,i.uv.x);
				NormalH = normalize(NormalH);

				float3 stepH = NormalH.xyz * stepSize;
				stepH.z *= -1;
				float3 pos = float3(0, 0, 0);

				for (int i = 0; i < numSteps; i++)
				{
					pos += stepH;
					/*float4 fourPos = float4(pos.x, pos.y, pos.z, 1);
					float4 newPos = mul(_DiskMat, fourPos);

					if (length(newPos.xz) < 0.00998)
					{
						if (abs(newPos.y < 0.1))
						{
							col = float4(1, 1, 1, 1);
							break;
						}
					}*/

					if (distance(pos, _BHPos) < _BHRad)
					{
						col = float4(0.5, 0, 0.5, 1);
						break;
					}

					
					for (int j = 0; j < _NumSphereTargets; j++)
					{
						if (distance(pos, _SphereTargetsPos[j]) < _SphereTargetsRad[j])
						{
							col = float4(1, 1, 1, 1);
							break;
						}
					}
					
				}

				return col;
			}
				ENDCG
		}
	}
}