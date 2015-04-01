Shader "SoL/Transparent/Cutout-Diffuse"
{
	Properties
	{
		[PerRendererData] _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}

	SubShader
	{
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha vertex:vert
		//alphatest:_Cutoff

		sampler2D _MainTex;
		fixed4 _Color;
		float _Cutoff;

		struct Input
		{
			float2 uv_MainTex;
			fixed4 color;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color * _Color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
			if (c.a < _Cutoff) discard;
			o.Albedo = c.rgb * c.a;
			o.Alpha = c.a;
		}
		
		ENDCG
	}

	Fallback "Transparent/Cutout/VertexLit"
}
