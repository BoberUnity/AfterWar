// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Animated/Point Light Animated Water"
{
	Properties
	{
		_Color ("Water Color (RGB) Transparency (A)", COLOR) = (1, 1, 1, 0.5)
		_BumpMap ("Normal Map 1", 2D) = "white" {}
		_BumpMap2 ("Normal Map 2", 2D) = "white" {}
		_FlowMap ("Flow Map", 2D) = "white" {}
		_Cube ("Reflection Cubemap", Cube) = "white" { TexGen CubeReflect }
		_Cycle ("Cycle", float) = 5.0
		_Speed ("Speed", float) = -0.2
		//_Shininess ("Shininess", Range (0.01, 200)) = 150
		//_Alfa ("Alfa", Range (0.01, 3)) = 0.5
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			Fog {Mode Off}
				 
         CGPROGRAM
			#pragma only_renderers d3d9 gles
			#pragma glsl
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

         #pragma vertex vert  
         #pragma fragment frag 
 
         uniform fixed4 _Color;

         uniform fixed4 _LightColor0; 

		uniform sampler2D _BumpMap;
		uniform half2 _BumpMap_ST; 

		uniform sampler2D _BumpMap2;
		uniform samplerCUBE _Cube;
		uniform sampler2D _FlowMap;
		uniform half _Cycle;
		uniform half _Speed;
		//uniform half _Shininess;
		//uniform half _Alfa;

		uniform sampler2D _LightTexture0;
		uniform half4x4 unity_WorldToLight;
 
         struct vertexInput
		 {
            float4 vertex : POSITION;
			half2 texcoord : TEXCOORD0;
         };
         struct vertexOutput
		 {
            float4 pos : SV_POSITION;
			half2 tex : TEXCOORD0;
			fixed3 dir : TEXCOORD1;
			fixed3 lightDir : TEXCOORD2;
			half3 _LightCoord : TEXCOORD3;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;

			float4 vertWorldPos = mul(unity_ObjectToWorld, input.vertex);

            half3 vertexToLightSource = half3(_WorldSpaceLightPos0 - vertWorldPos);
			output.lightDir = normalize(vertexToLightSource);

			output.dir = -(vertWorldPos.xyz - _WorldSpaceCameraPos.xyz);

            output.pos = UnityObjectToClipPos(input.vertex);
			output.tex = input.texcoord;

			output._LightCoord = mul(unity_WorldToLight, vertWorldPos).xyz;

            return output;
         }
 
         fixed4 frag(vertexOutput input) : COLOR
         {
			
			half4 flowDir = tex2D(_FlowMap, input.tex);
			fixed s = flowDir.b;
			flowDir = flowDir * 2 - 1;

			flowDir *= _Speed*s;
			flowDir.y *= -1;

			fixed phase = _Time[1] / _Cycle + flowDir.a;

			fixed f = frac(phase);

			half2 texAndCaleST = input.tex*_BumpMap_ST.xy;

			half3 n1 = UnpackNormal(tex2D(_BumpMap, texAndCaleST + flowDir.xy * frac(phase + 0.5f)));
			half3 n2 = UnpackNormal(tex2D(_BumpMap2, texAndCaleST + flowDir.xy * f));

			f = lerp(2.0f * f, 2.0f * (1.0f - f) , step(0.5, f));

			fixed3 normal = lerp(n1.xzy, n2.xzy, f);
			//fixed3 normal = n1.xzy*2;

			fixed3 c = texCUBE (_Cube, reflect(-input.dir, normal)).rgb;

            fixed attenuation = tex2D(_LightTexture0, dot(input._LightCoord,input._LightCoord).rr).UNITY_ATTEN_CHANNEL;

			fixed3 h = normalize (input.lightDir + normalize(input.dir));
			fixed nh = max (0, dot (normal, h));
			//fixed spec = pow (nh, _Shininess);

            //return fixed4(c*_Color.rgb + spec, _Color.a)*attenuation;
						//return fixed4(c*_Color.rgb, _Color.a)*_Alfa;
						return fixed4(c*_Color.rgb, _Color.a);
         }
 
         ENDCG

	}
}
}