Shader "Custom/Portal"
{
    SubShader
{
    ZWrite off      //render Z-axis
    ColorMask 0     //transparent
    Cull off   		//Bidirectional behaviour

    Stencil{
        Ref 1
        Pass replace
    }

        Pass
        {	
			// CGPROGRAM
			// 		#pragma vertex vert
			// 		#pragma fragment frag

			// 		#include "UnityCG.cginc"

			// 		struct appdata
			// 		{
			// 			float4 vertex : POSITION;
			// 		};

			// 		struct v2f
			// 		{
			// 			float4 vertex : SV_POSITION;
			// 		};

			// 		v2f vert(appdata v)
			// 		{
			// 			v2f o;
			// 			o.vertex = UnityObjectToClipPos(v.vertex);
			// 			return o;
			// 		}

			// 		fixed4 frag(v2f i) : SV_Target
			// 		{
			// 			return fixed4(0.0, 0.0, 0.0, 0.0);
			// 		}
			// 		ENDCG
        }
    }
}
