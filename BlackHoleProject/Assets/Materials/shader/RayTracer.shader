// Upgrade NOTE: replaced '_CameraToWorld' with 'unity_CameraToWorld'

// Upgrade NOTE: replaced '_CameraToWorld' with 'unity_CameraToWorld'

// Upgrade NOTE: commented out 'float4x4 _CameraToWorld', a built-in variable

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
			Texture2D  _MainTex;
			Texture2D  _DiskTexture;
			SamplerState sampler_MainTex;


			float4 _BL;
			float4 _TL;
			float4 _TR;
			float4 _BR;
			float4 _BHPos;
			float _BHRad;

			int _Width;
			int _Height;

			// float4x4 _CameraToWorld;
			float4x4 _CameraInverseProjection;

			float4x4 _DiskMat;

			int _NumSphereTargets;
			float4 _SphereTargetsPos[4];
			float _SphereTargetsRad[4];

			int _NumCyTargets;
			float4 _CyTargetsPos[4];
			float4 _CyTargetsShape[4];


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
				UNITY_VERTEX_OUTPUT_STEREO

				return o;
			}
		


			fixed4 frag(v2f i) : SV_Target
			{
				int numSteps = 2500;
				float stepSize = 0.01;
				float3 bh = _BHPos;
				float G = 0.0000000000667430;
				float PI = 3.1414;
				float4 col = _MainTex.Sample(sampler_MainTex, i.uv);
				float2 newUV = i.uv * 2 - float2(1, 1);
				newUV *= -1;

				float3 stepH = mul(_CameraInverseProjection, float4(newUV.x, newUV.y, 0.f, 1.0f)).xyz;
				stepH = mul(unity_CameraToWorld, float4(stepH, 0.0f)).xyz;
				stepH *= -1 * stepSize;

				float3 pos = float3(0, 0, 0);
				pos = _WorldSpaceCameraPos;

				float2 diskUV = float2(-1, -1);

				for (int i = 0; i < numSteps; i++)
				{
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

					float3 dir = _BHPos - pos;
					float dist = length(dir);
					float mass = 59700000000.0 * _BHRad;
					float c = 299;
					float grav = (4 * G * mass) / (dist * dist * c * c);
					if (length(grav > 1))
					{
						i += 5;
					}
					dir = normalize(dir);
					stepH += dir * grav;

					if (distance(pos, _BHPos) < _BHRad)
					{
						col = float4(0, 0, 0, 1);
						break;
					}
					/*
					for (int j = 0; j < _NumSphereTargets; j++)
					{
						if (distance(pos, _SphereTargetsPos[j]) < _SphereTargetsRad[j])
						{
							col = float4(1, 1, 1, 1);
							i = numSteps;
							break;
						}
					}*/

					
					for (int k = 0; k < _NumCyTargets; k++)
					{
						if (distance(pos.xz, _CyTargetsPos[k].xz) < _CyTargetsShape[k].x)
						{
							if (abs(pos.y - _CyTargetsPos[k].y) < _CyTargetsShape[k].y)
							{
								float shade = 1 - distance(pos.xz, _CyTargetsPos[k].xz) / _CyTargetsShape[k].x;

								float2 delta = _CyTargetsPos[k].xz - pos.xz;
								float deg = atan2(delta.x, delta.y) + PI;
								deg /= 2 * PI;
								deg = fmod(deg + _GameTime / 5, 1.0);
								diskUV = float2(deg, shade);

								col = float4( deg, 0, 1 - deg, 1);
								i = numSteps;
								k = 100;
								break;
							}
							
						}
					}
					pos += stepH;

					
				}
				if (diskUV.x != -1)
				{
					col = _DiskTexture.Sample(sampler_MainTex, diskUV);
				}
				return col;
			}
				ENDCG
		}
	}
}