﻿Shader "Custom/VisorShader"
{
	Properties
	{
		//_VisorOn("_VisorOn", Float) = 1
		_MainTex ("Texture", 2D) = "white" {}
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
				float4 vertex : SV_POSITION;
			};
			float _VisorOn;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex); 
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv); 
				if(_VisorOn>0){
					col/=2;

					if(col.r > col.b)
					{
						col.r*=2;
						return col;
					}
					col.b=	col.b*2;//+clamp((cos(i.uv.x+_Time.y)*3*col.b),0,1);
				}
				return col;
			}
			ENDCG
		}
	}
}
