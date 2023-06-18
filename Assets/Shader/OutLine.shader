Shader "Custom/OutLine"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness",Range(0,1)) = 0.5
		_OutlineColor("Outline Color", Color) = (1,0,0,1)
		_OutlineWidth("Outline Width", Range(0, 4)) = 0.002

		[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2
	}
		SubShader
		{
			Tags { /*"RenderType" = "Geometry"*/"RenderType" = "Opaque" "Queue" = "Transparent" }
			LOD 200
			Cull[_Cull]

			Pass{
				ZWrite Off
				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				struct appdata {
					float4 vertex : POSITION;
					float4 tangent : TANGENT;
					float3 normal : NORMAL;
					float4 texcoord : TEXCOORD0;
					fixed4 color : COLOR;
				};

				struct v2f {
					float4 pos : SV_POSITION;
					float3 normal : NORMAL;
				};

				fixed4 _OutlineColor;
				half _OutlineWidth;

				v2f vert(appdata input) {
					input.vertex += float4(input.normal * _OutlineWidth, 1);

					v2f output;

					output.pos = UnityObjectToClipPos(input.vertex);
					output.normal = mul(unity_ObjectToWorld, input.normal);

					return output;
				}

				fixed4 frag(v2f input) : SV_Target
				{
					return _OutlineColor;
				}

				ENDCG
			}

			ZWrite On
			CGPROGRAM
					// Physically based Standard lighting model, and enable shadows on all light types
					#pragma surface surf Standard fullforwardshadows

					#ifndef SHADER_API_D3D11
						#pragma target 3.0
					#else
						#pragma target 4.0
					#endif

					#include "UnityPBSLighting.cginc"

					UNITY_INSTANCING_BUFFER_START(Props)
					// put more per-instance properties here
				UNITY_INSTANCING_BUFFER_END(Props)

				half4 BRDFjp_Unity_PBS(half3 diffColor, half3 specColor, half oneMinusReflectivity, half oneMinusRoughness,half3 normal, half3 viewDir,UnityLight light, UnityIndirect gi)
				 {
					half roughness = 1 - oneMinusRoughness;
					half3 halfDir = Unity_SafeNormalize(light.dir + viewDir);

					half nl = light.ndotl;
					half nh = BlinnTerm(normal, halfDir);
					half nv = DotClamped(normal, viewDir);
					half lv = DotClamped(light.dir, viewDir);
					half lh = DotClamped(light.dir, halfDir);
					#if UNITY_BRDF_GGX
					half V = SmithGGXVisibilityTerm(nl, nv, roughness);
					half D = GGXTerm(nh, roughness);
					#else
					half V = SmithBeckmannVisibilityTerm(nl, nv, roughness);
					half D = NDFBlinnPhongNormalizedTerm(nh, RoughnessToSpecPower(roughness));
					#endif

					half nlPow5 = Pow5(1 - nl);
					half nvPow5 = Pow5(1 - nv);
					half Fd90 = 0.5 + 2 * lh * lh * roughness;
					half disneyDiffuse = (1 + (Fd90 - 1) * nlPow5) * (1 + (Fd90 - 1) * nvPow5);

					// HACK: theoretically we should divide by Pi diffuseTerm and not multiply specularTerm!
					// BUT 1) that will make shader look significantly darker than Legacy ones
					// and 2) on engine side "Non-important" lights have to be divided by Pi to in cases when they are injected into ambient SH
					// NOTE: multiplication by Pi is part of single constant together with 1/4 now

					half specularTerm = max(0, (V * D * nl) * unity_LightGammaCorrectionConsts_PIDiv4);// Torrance-Sparrow model, Fresnel is applied later (for optimization reasons)
					half diffuseTerm = disneyDiffuse * nl;

					half grazingTerm = saturate(oneMinusRoughness + (1 - oneMinusReflectivity));
					half3 color = diffColor * (gi.diffuse + light.color * diffuseTerm)
						+ specularTerm * light.color * FresnelTerm(specColor, lh)
						+ gi.specular * FresnelLerp(specColor, grazingTerm, nv);
					//    return float4 (light.ndotl.xxx+0.5,1);
					return half4(color, 1);
				}


				inline half4 LightingJP(SurfaceOutputStandard s, half3 viewDir, UnityGI gi)
				{
					s.Normal = normalize(s.Normal);

					half oneMinusReflectivity;
					half3 specColor;
					s.Albedo = DiffuseAndSpecularFromMetallic(s.Albedo, s.Metallic, /*out*/ specColor, /*out*/ oneMinusReflectivity);

					// shader relies on pre-multiply alpha-blend (_SrcBlend = One, _DstBlend = OneMinusSrcAlpha)
					// this is necessary to handle transparency in physically correct way - only diffuse component gets affected by alpha
					half outputAlpha;
					s.Albedo = PreMultiplyAlpha(s.Albedo, s.Alpha, oneMinusReflectivity, /*out*/ outputAlpha);
					half4 c = BRDFjp_Unity_PBS(s.Albedo, specColor, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, gi.light, gi.indirect);
					// c.rgb += UNITY_BRDF_GI (s.Albedo, specColor, oneMinusReflectivity, s.Smoothness, s.Normal, viewDir, s.Occlusion, gi);
					c.a = outputAlpha;
					return c;
				}


				inline void LightingJP_GI(
					SurfaceOutputStandard s,
					UnityGIInput data,
					inout UnityGI gi)
				{
					gi = UnityGlobalIllumination(data, s.Occlusion, s.Smoothness, s.Normal);
				}


				sampler2D _MainTex;

				struct Input
				{
					float2 uv_MainTex;
				};

				half _Glossiness;
				half _Metallic;
				fixed4 _Color;
				fixed4 pixel;

				void surf(Input IN, inout SurfaceOutputStandard o) {
					// Albedo comes from a texture tinted by color
					fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					o.Albedo = c.rgb;
					// Metallic and smoothness come from slider variables
					o.Metallic = _Metallic;
					o.Smoothness = _Glossiness;
					o.Alpha = c.a;
					pixel = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					o.Albedo = pixel.rgb;
				}

				ENDCG
		}
			FallBack "Diffuse"
}
