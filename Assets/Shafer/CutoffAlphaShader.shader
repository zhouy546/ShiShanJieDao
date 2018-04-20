﻿Shader "Custom/CutoffAlphaShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Cutoff("Cutoff Value",Range(0,1.1)) = 0.5

	}
		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM

#pragma surface surf Lambert alphatest:_Cutoff

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
	};

	fixed4 _Color;

	void surf(Input IN, inout SurfaceOutput o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Albedo = c.rgb;

		o.Alpha = c.b;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
