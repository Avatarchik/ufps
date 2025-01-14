Shader "Marmoset/Terrain/Simple Terrain IBL" {
//This shader is a selectable version of the replacement shader. It mimics Unity's diffuse-only terrain shader, adding an IBL component.
Properties {
	_Control ("Splatmap (RGBA)", 2D) = "red" {}
	_Splat0 ("Layer 0 (R)", 2D) = "white" {}
	_Splat1 ("Layer 1 (G)", 2D) = "white" {}
	_Splat2 ("Layer 2 (B)", 2D) = "white" {}
	_Splat3 ("Layer 3 (A)", 2D) = "white" {}
	// used in fallback on old cards
	_MainTex ("BaseMap (RGB)", 2D) = "white" {}
	_Color ("Main Color", Color) = (1,1,1,1)
}
	
SubShader {
	Tags {
		"SplatCount" = "4"
		"Queue" = "Geometry-100"
		"RenderType" = "Opaque"
	}
	CGPROGRAM
	#pragma glsl
	#pragma target 4.0
	#pragma surface MarmosetSimpleSurf Lambert
	#pragma exclude_renderers d3d11_9x flash

	#pragma multi_compile MARMO_TERRAIN_BLEND_OFF MARMO_TERRAIN_BLEND_ON
	#if MARMO_TERRAIN_BLEND_ON			
	#define MARMO_SKY_BLEND
	#endif

	#define MARMO_DIFFUSE_IBL
	#define MARMO_SKY_ROTATION

	#include "../MarmosetCore.cginc"
	#include "MarmosetSimpleTerrain.cginc"

	ENDCG
}

SubShader {
	Tags {
		"SplatCount" = "4"
		"Queue" = "Geometry-100"
		"RenderType" = "Opaque"
	}
	CGPROGRAM
	#pragma glsl
	#pragma target 3.0
	#pragma surface MarmosetSimpleSurf Lambert nodirlightmap
	#pragma exclude_renderers d3d11_9x flash
	
	
	#pragma multi_compile MARMO_TERRAIN_BLEND_OFF MARMO_TERRAIN_BLEND_ON
	#if MARMO_TERRAIN_BLEND_ON			
		#define MARMO_SKY_BLEND
	#endif

	#define MARMO_DIFFUSE_IBL
	#define MARMO_SKY_ROTATION

	#include "../MarmosetCore.cginc"
	#include "MarmosetSimpleTerrain.cginc"

	ENDCG
}
Dependency "AddPassShader" = "Hidden/Marmoset/Terrain/Simple Terrain IBL AddPass"
Dependency "BaseMapShader" = "Hidden/Marmoset/Terrain/Simple Distant IBL"

// Fallback to Diffuse
FallBack "Marmoset/Diffuse IBL"
}
