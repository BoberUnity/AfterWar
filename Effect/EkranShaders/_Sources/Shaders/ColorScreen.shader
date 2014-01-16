Shader "Cg shader using blending" {
Properties
        {
                _Mtl ("Main Color", Range(0,1)) = 0.5
        }

   SubShader {
      Tags {"Queue"="Overlay+100" "IgnoreProjector"="True" "RenderType"="Transparent"}

      Pass {
         ZWrite Off
                 ZTest Off
 
         Blend DstColor SrcColor // use alpha blending
 
         CGPROGRAM
 
         #pragma vertex vert
         #pragma fragment frag

                 fixed _Mtl;
 
         float4 vert(float4 vertexPos : POSITION) : SV_POSITION
         {
            return mul(UNITY_MATRIX_MVP, vertexPos);
         }
 
         float4 frag(void) : COLOR
         {
            return _Mtl;
         }
 
         ENDCG  
      }         
   }
}