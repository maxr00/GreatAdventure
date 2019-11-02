#ifndef __DITHER_FUNCTIONS__
#define __DITHER_FUNCTIONS__
#include "UnityCG.cginc"

void Dither(float4 screenPos, float transparency)
{
	float4x4 thresholdMatrix =
	{ 
		1.0 / 17.0,  9.0 / 17.0,  3.0 / 17.0, 11.0 / 17.0,
	  13.0 / 17.0,  5.0 / 17.0, 15.0 / 17.0,  7.0 / 17.0,
	   4.0 / 17.0, 12.0 / 17.0,  2.0 / 17.0, 10.0 / 17.0,
	  16.0 / 17.0,  8.0 / 17.0, 14.0 / 17.0,  6.0 / 17.0
	};
	float4x4 _RowAccess = { 1,0,0,0, 0,1,0,0, 0,0,1,0, 0,0,0,1 };
	float2 pos = screenPos.xy / screenPos.w;
	pos *= _ScreenParams.xy; // pixel position

	clip(transparency - thresholdMatrix[fmod(pos.x, 4)] * _RowAccess[fmod(pos.y, 4)]);
}

#endif