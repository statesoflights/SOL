Shader "MyShaders/ChangeSprite"
{
	Properties
	{
		//[PerRendererData]
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_LitTex ("Lit texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_LightThreshold ("Light Threshold", Range(0, 1)) = 0.5
		_DarkThreshold ("Dark Threshold", Range(0, 1)) = 0.5
		_Cutout ("Alpha Cutout", Range(0, 1)) = 0.05
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert nofog keepalpha finalcolor:finalblend addshadow
		//#pragma multi_compile _ PIXELSNAP_ON

		//sampler2D _MainTex;
		sampler2D _MainTex;
		sampler2D _LitTex;
		fixed4 _Color;
		float _LightThreshold;
		float _DarkThreshold;
		float _Cutout;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_LitTex;
			fixed4 color;
		};
		
		void vert (inout appdata_full v, out Input o)
		{
			#if defined(PIXELSNAP_ON)
			v.vertex = UnityPixelSnap (v.vertex);
			#endif
			
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color * _Color;
		}

		void surf (Input IN, inout SurfaceOutput o)
		{
			//fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
			//o.Albedo = c.rgb * c.a;
			o.Albedo = 1.0;
			o.Alpha = 1.0;
		}
		
		void finalblend (Input IN, SurfaceOutput o, inout fixed4 color)
		{
			float light = 0.1 * (color.r + color.g + color.b);
			if (light >= _LightThreshold)
			{
				color *= tex2D(_LitTex, IN.uv_LitTex);
			}
			else if (light <= _DarkThreshold)
			{
				color *= tex2D(_MainTex, IN.uv_MainTex);
			}
			
			else
			{
				color *= lerp(tex2D(_MainTex, IN.uv_MainTex), tex2D(_LitTex, IN.uv_MainTex), (light - _DarkThreshold) / (_LightThreshold - _DarkThreshold));
			}
			//color *= IN.color;
			if (color.a < _Cutout) discard;
			
			//if (light <= _DarkThreshold) discard;
			//color = light;
			color.a = 1;
			
		}
		
		ENDCG
	}

Fallback "Transparent/VertexLit"
}
