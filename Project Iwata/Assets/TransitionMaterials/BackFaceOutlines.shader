Shader "Outlines/BackFaceOutlines"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
		_Thickness("Thickness", Float) = 1
		_Color("Color", Color) = (1,1,1,1)
		_DepthOffset("Depth offset", Range(0, 1)) = 0 //an offset to the clip space Z, pushing the outline back
		//if enabled this shader will use "smoothed" normals stored in TEXCOORD1 to extrude along
		[Toggle(USE_PRECALCULATED_OUTLINE_NORMALS)]_PrecalculateNormals("Use UV1 normals", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }
        

        Pass
        {
           Name "Outlines"
		   //cull front faces
			Cull Front
			HLSLPROGRAM
			//standard urp requirements
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma shader_feature USE_PRECALCULATED_OUTLINE_NORMALS

			//Register our functions
			#pragma vertex Vertex
			#pragma fragment Fragment

			//Includes our logic file
			#include "BackFaceOutlines.hlsl"

			ENDHLSL
        }
    }
}
