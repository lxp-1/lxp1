Shader "Hidden/001"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MainTex2("Texture2", 2D) = "white" {}
		_shift("shift",Range(0,1)) = 0
		_centre("centre",Range(0 ,1 )) = 0

	}
		SubShader
		{
			// No culling or depth
		   // Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

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

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;
				sampler2D _MainTex2;
				float _centre;
				float _shift;

				fixed4 frag(v2f i) : SV_Target
				{
				//	float f=
					if (i.uv.x < _centre - _shift / 2)
					{
						return  tex2D(_MainTex, i.uv);
					}
					if (i.uv.x > _centre + _shift / 2)
					{
						return  tex2D(_MainTex2, i.uv);
					}
					fixed4 col = tex2D(_MainTex, i.uv);
					fixed4 col1 = tex2D(_MainTex2, i.uv);
					col = col * (1 - ((i.uv.x - (_centre - _shift / 2)) / _shift));


					 col1 = col1 * ((i.uv.x - (_centre - _shift / 2)) / _shift);
					 return col + col1;// 2;
				 }
				 ENDCG
			 }
		}
}
