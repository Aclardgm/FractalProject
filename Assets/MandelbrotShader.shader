Shader "Hidden/MandelbrotShader"
{
    Properties
    {
		_Zoom ("Zoom",float) = 10000.0
		_IterationMax("IterationMax",float) = 1000
		_OffsetX("OffsetX",float) = 0
		_OffsetY("OffsetY",float) = 0
		_OffsetR("OffsetR",float) = 0
		_OffsetG("OffsetG",float) = 0
		_OffsetB("OffsetB",float) = 0

		_SizeImageX("SizeImageX",float) = 0
		_SizeImageY("SizeImageY",float) = 0
		_RotAngle("RotAngle",float) = 0

    }
    SubShader
    {
		Tags{
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}

        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float _Zoom;
			float _IterationMax;
			float _OffsetX;
			float _OffsetY;

			float _OffsetR;
			float _OffsetG;
			float _OffsetB;

			float _SizeImageX;
			float _SizeImageY;

			float _RotAngle;


            fixed4 frag (v2f i) : SV_Target
			{
				//for doubles passed in properties use 2 uint and the asdouble HLSL function 
				fixed4 col = (0,0,0,0);
				

				float x1 = i.uv.x - _OffsetX;
				float x2 = i.uv.x + _OffsetX;
				float y1 = i.uv.y - _OffsetY;
				float y2 = i.uv.y + _OffsetY;

				x1 = x1 * cos(_RotAngle) + y1 * sin(_RotAngle);
				y1 = y1 * cos(_RotAngle) - x1 * sin(_RotAngle);


				x2 = x2 * cos(_RotAngle) + y2 * sin(_RotAngle);
				y2 = y2 * cos(_RotAngle) - x2 * sin(_RotAngle);


				float rotUVx = i.uv.x * cos(_RotAngle) + i.uv.y * sin(_RotAngle);
				float rotUVy = i.uv.y  * cos(_RotAngle) - i.uv.x * sin(_RotAngle);


				float c_r = (rotUVx) / _Zoom + x1;
				float c_i = (rotUVy) / _Zoom + y1;
				float z_r = 0;
				float z_i = 0;
				int iter = 0;
				for (iter = 0; iter < _IterationMax && z_r*z_r + z_i * z_i < 4; iter++) {
					float tmp = z_r;
					z_r = z_r * z_r - z_i * z_i + c_r;
					z_i = 2 * z_i*tmp + c_i;
				}
				if (iter == _IterationMax)
				{
					col.rgb = half3(0, 0, 0);
				}
				else
				{
					float r = (((iter / _IterationMax) + _OffsetR) * 10000) % 10000 / 10000.0f;
					float g = (((iter / _IterationMax) + _OffsetG) * 10000) % 10000 / 10000.0f;
					float b = (((iter / _IterationMax) + _OffsetB) * 10000) % 10000 / 10000.0f;

					col.rgb = half3(r,g,b);
				}

                return col;
            }
            ENDCG
        }
    }
}
