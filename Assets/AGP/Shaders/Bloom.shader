Shader "_Custom/Bloom"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	sampler2D _MainTex, _SourceTex;
	float4 _MainTex_TexelSize;
	half _Threshold, _Intensity;

	half3 Sample(float2 uv) {
		return tex2D(_MainTex, uv).rgb;
	}

	half3 SampleBox(float2 uv, float delta) {
		float4 o = _MainTex_TexelSize.xyxy * float2(-delta, delta).xxyy;
		half3 s =
			Sample(uv + o.xy) + Sample(uv + o.zy) +
			Sample(uv + o.xw) + Sample(uv + o.zw);
		return s * 0.25f;
	}

	half3 Prefilter(half3 c) {
		half brightness = max(c.r, max(c.g, c.b));
		half contribution = max(0, brightness - _Threshold);
		contribution /= max(brightness, 0.00001);
		return c * contribution;
	}

	struct appdata {
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert(appdata v) {
		v2f i;
		i.pos = UnityObjectToClipPos(v.vertex);
		i.uv = v.uv;
		return i;
	}
	ENDCG

		SubShader
	{
		Cull Off
		ZTest Always
		ZWrite Off

		// Prefiltering
		Pass
	{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		half4 frag(v2f i) : SV_Target
	{
		return half4(Prefilter(SampleBox(i.uv, 1)), 1);
	}
		ENDCG
	}

		// Down sampling
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : SV_Target
		{
			return half4(SampleBox(i.uv, 1), 1);
		}
			ENDCG
		}

		// Up sampling
		Pass
		{
			// Additive blending
			Blend One One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : SV_Target
		{
			return half4(SampleBox(i.uv, 0.5), 1);
		}
			ENDCG
		}

		// Default blend mode, adds the box sample to a sample of the source texture
		Pass
		{ 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : SV_Target{
			half4 c = tex2D(_SourceTex, i.uv);
			c.rgb += _Intensity * SampleBox(i.uv, 0.5);
			return c;
		}
			ENDCG
		}
	}
}
