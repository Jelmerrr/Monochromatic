Shader "Custom/CrosshairShader"
{
    Properties {
        _TintColor ("Tint Color", Color) = (1,1,1,1)
    }
        SubShader
        {
            Pass
            {
                Tags { "Queue"="Overlay+100" "IgnoreProjector"="True" }
   
                Blend OneMinusDstColor OneMinusSrcColor
                ColorMask RGB
   
                ZWrite Off
                ZTest Always
   
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
   
                float4 vert(float4 vertex : POSITION) : SV_Position {
                  return UnityObjectToClipPos(vertex);
                }
   
                fixed4 frag() : SV_Target {
                  return fixed4(1,1,1,1);
                }
   
                ENDCG
            }
        }
    FallBack "Diffuse"
}
