// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MaskTest" {

    Properties {
        _MainTex ("Base (RGB)", 2D) = "" {}
         _MainMask ("Base (RGB)", 2D) = "" {}
    }
    SubShader {
        Pass
        {
        Blend SrcAlpha OneMinusSrcAlpha  //一定不要忘了这个
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            struct vert_Input
            {
                fixed4 vertex : POSITION;
                fixed2 texcoord : TEXCOORD0;
            };
            struct vert_Output
            {
                fixed4 pos : SV_POSITION;
                fixed2 texcoord : TEXCOORD0;
            };
            uniform sampler2D _MainTex;
            uniform sampler2D _MainMask;
            vert_Output vert(vert_Input i)
            {
                vert_Output o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.texcoord = i.texcoord;
                return o;
            }

            fixed4 frag(vert_Output o):COLOR
            {
                fixed4 color1  = tex2D(_MainTex,o.texcoord);
                fixed4 mask = tex2D(_MainMask,o.texcoord);
                color1.a = mask.r;
                return color1;
            }

        ENDCG
        }
    }
}