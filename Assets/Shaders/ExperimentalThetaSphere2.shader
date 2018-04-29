Shader "Theta/ExperimentalSphere2" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaBlendTex ("Alpha Blend (RGBA)", 2D) = "white" {}
		_OffsetU ("Offset U", Range(-0.5, 0.5)) = 0 
		_OffsetV ("Offset V", Range(-0.5, 0.5)) = 0
		_ScaleU ("Scale U", Range(0.8, 1.2)) = 1
		_ScaleV ("Scale V", Range(0.8, 1.2)) = 1
		_ScaleCenterU ("Scale Center U", Range(0.0, 1.0)) = 0 
		_ScaleCenterV ("Scale Center V", Range(0.0, 1.0)) = 0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" "Queue" = "Background+1" }
		
		Cull Off

		
        CGPROGRAM

        #pragma surface surf Lambert vertex:vert
		sampler2D _MainTex;

		 struct Input {
            float2 uv_MainTex;
            float4 color : COLOR;
		};

		void vert(inout appdata_full v) {
			v.normal.xyz = v.normal * -1;
		}

		 void surf (Input IN, inout SurfaceOutput o) {
             fixed3 result = tex2D(_MainTex, IN.uv_MainTex);
             o.Albedo = result.rgb;
             o.Alpha = 1;
        }

		ENDCG
		
		UsePass "Theta/ExperimentalSphere1/BASE"
	}
}