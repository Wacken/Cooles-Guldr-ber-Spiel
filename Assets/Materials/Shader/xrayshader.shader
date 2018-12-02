// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/xrayshader" 
{
	Properties
	{
		_PingModulo("_PingModulo", Range (0, 30)) = 8 
		_VisorRange("_VisorRange", Range (0, 30)) = 12
		_PingSpeed("_PingSpeed", Range (0, 30)) = 4
		_CableColor("_CableColor", Color) = (1, 0, 0.5, 1)
		//_MainTex("Texture", 2D) = "white" {}
		//_RampTex("Ramp", 2D) = "white" {}
		_SilColor("Silouette Color", Color) = (1, 0, 0.5, 1)
	}

	SubShader
	{
 		// Regular color & lighting pass
		Pass
		{
            Tags
			{	
			            "IgnoreProjector"="True" 
            "RenderType"="Transparent+10"
				"Queue" = "Transparent+10"
				"LightMode" = "ForwardBase" // allows shadow rec/cast
			}

			// Write to Stencil buffer (so that silouette pass can read)
			Stencil
			{
				Ref 4
				Comp always
				Pass replace
				ZFail keep
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase // shadows
			#include "AutoLight.cginc"
			#include "UnityCG.cginc"

			// Properties
			//float4 _PlayerPos; 
			float _VisorOn;
			float4 _CableColor;
			//sampler2D _MainTex;
			//sampler2D _RampTex;
			float4 _LightColor0; // provided by Unity

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float3 texCoord : TEXCOORD0;
				LIGHTING_COORDS(1,2) // shadows
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				// convert input to world space
				output.pos = UnityObjectToClipPos(input.vertex);
				float4 normal4 = float4(input.normal, 0.0); // need float4 to mult with 4x4 matrix
				output.normal = normalize(mul(normal4, unity_WorldToObject).xyz);

				output.texCoord = input.texCoord;

                TRANSFER_VERTEX_TO_FRAGMENT(output); // shadows
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
				float ramp = clamp(dot(input.normal, lightDir), 0, 1.0);
				//float3 lighting = tex2D(_RampTex, float2(ramp, 0.5)).rgb;
				
				//float4 albedo = tex2D(_MainTex, input.texCoord.xy);
				float4 albedo = _CableColor;
				float attenuation = LIGHT_ATTENUATION(input); // shadow value
				float3 rgb = albedo.rgb * _LightColor0.rgb  * attenuation;//* lighting;
				return float4(rgb, 1.0);
			}

			ENDCG
		}

		// Shadow pass
		Pass
    	{
            Tags 
			{
				"LightMode" = "ShadowCaster"
			}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
    	}

		// Silouette pass 1 (backfaces)
		Pass
		{
			Tags
            {
			"RenderType"="Transparent+10"
                "Queue" = "Transparent+10"
            }
			// Won't draw where it sees ref value 4
			Cull Front // draw back faces
			ZWrite OFF
			ZTest Always
			Stencil
			{
				Ref 3
				Comp Greater
				Fail keep
				Pass replace
			}
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// Properties			
			float4 _PlayerPos;
			uniform float4 _SilColor;
			float _VisorRange;
			float _PingSpeed;
			float _PingModulo;
			float _VisorOn;
			struct vertexInput
			{
				float4 vertex : POSITION;
			};

			struct vertexOutput
			{
				float4 worldPos : POSITION1;
				float4 pos : SV_POSITION;
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				output.worldPos = mul(unity_ObjectToWorld, input.vertex);

				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
			float alphaVal = 1;
				float distance = length(input.worldPos.xyz-_PlayerPos.xyz);

				/*
				alphaVal = fmod(400 + distance +_Time.y*_PingSpeed, _PingModulo);
 
				if(alphaVal > _PingModulo-1){
				alphaVal = _PingModulo- alphaVal;
				}
				alphaVal = max(alphaVal, (1-distance/ _VisorRange));
				//alphaVal = fmod(400 + distance -sin(_Time.y*1)*10, 4);*/

 			if( distance > _VisorRange ){ //&& alphaVal  > 1	//this thing is not useless
			
			//clip(-1);
			return float4(1,0,1,0);
			}
			return(float4(1,0,1,0));
				return float4( _SilColor.xyz, 1-alphaVal) ;
			}

			ENDCG
		}

		// Silouette pass 2 (front faces)
		Pass
		{
			Tags
            {
                "Queue" = "Transparent+10" "RenderType"="Transparent+10"
            }
			// Won't draw where it sees ref value 4
			Cull Back // draw front faces
			ZWrite OFF
			ZTest Always
			Stencil
			{
				Ref 4 
				Comp NotEqual
				Pass keep
			}
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// Properties
			 float4 _PlayerPos;
			 float _VisorRange;
			 float _PingSpeed;
			 float _PingModulo;
			uniform float4 _SilColor;
			float _VisorOn;

			struct vertexInput
			{
				float4 vertex : POSITION;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 worldPos : POSITION1;

			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				output.worldPos = mul(unity_ObjectToWorld, input.vertex);
				return output;
			}

float4 frag(vertexOutput input) : COLOR
			{
				float alphaVal = 1;
				float distance = length(input.worldPos.xyz-_PlayerPos.xyz);

				/*
				alphaVal = fmod(400 + distance +_Time.y*_PingSpeed, _PingModulo);
				if(alphaVal > _PingModulo-1){
				alphaVal = _PingModulo- alphaVal;
				}*/
				//alphaVal = (sin(distance*5+_Time.y*0)*5-4); 
				float _PingSparseness = 3;
				float timeAndDist  = distance - _Time.y*_PingSpeed;

				float distance2point = abs(timeAndDist-floor(timeAndDist/_PingSparseness+0.5)*_PingSparseness);

				alphaVal = 1-((distance2point)); 

				alphaVal = min(alphaVal, (1-distance/ _VisorRange));	//fades out the cable before it reaches max visorrange
				//alphaVal = fmod(400 + distance -sin(_Time.y*1)*10, 4);
 			/*if( distance > _VisorRange ){ //&& alphaVal  > 1 
			
			//clip(-1);
			//return float4(1,0,1,0);
			}*/
			if(_VisorOn < 1){
			alphaVal = 0;
			} 
				return float4( _SilColor.xyz, sqrt(alphaVal)) ;
			}

			ENDCG
		}
	}
}