// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32904,y:32579,varname:node_4013,prsc:2|diff-6459-OUT;n:type:ShaderForge.SFN_Tex2d,id:3722,x:31785,y:32792,ptovrint:False,ptlb:FloorTex,ptin:_FloorTex,varname:node_3722,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f2559600c6306954891e8d0c5c5426ff,ntxv:0,isnm:False|UVIN-57-OUT;n:type:ShaderForge.SFN_Tex2d,id:7300,x:31773,y:32609,ptovrint:False,ptlb:zTex,ptin:_zTex,varname:node_7300,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f2559600c6306954891e8d0c5c5426ff,ntxv:0,isnm:False|UVIN-9650-OUT;n:type:ShaderForge.SFN_NormalVector,id:3819,x:30994,y:32138,prsc:2,pt:False;n:type:ShaderForge.SFN_Abs,id:2766,x:31882,y:32227,varname:node_2766,prsc:2|IN-8041-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:5379,x:31163,y:32822,varname:node_5379,prsc:2;n:type:ShaderForge.SFN_Append,id:9650,x:31460,y:32631,varname:node_9650,prsc:2|A-5379-Z,B-5379-Y;n:type:ShaderForge.SFN_Append,id:57,x:31525,y:32804,varname:node_57,prsc:2|A-5379-Z,B-5379-X;n:type:ShaderForge.SFN_Append,id:6495,x:31510,y:32946,varname:node_6495,prsc:2|A-5379-X,B-5379-Y;n:type:ShaderForge.SFN_Tex2d,id:7217,x:31795,y:32989,ptovrint:False,ptlb:xTex,ptin:_xTex,varname:_VerticalTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f2559600c6306954891e8d0c5c5426ff,ntxv:0,isnm:False|UVIN-6495-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:6459,x:32184,y:32654,varname:node_6459,prsc:2,chbt:0|M-9454-OUT,R-7300-RGB,G-3722-RGB,B-7217-RGB;n:type:ShaderForge.SFN_ComponentMask,id:9642,x:31206,y:32194,varname:node_9642,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-3819-OUT;n:type:ShaderForge.SFN_Multiply,id:8084,x:31358,y:32371,varname:node_8084,prsc:2|A-9642-G,B-813-OUT;n:type:ShaderForge.SFN_Append,id:5756,x:31513,y:32226,varname:node_5756,prsc:2|A-9642-R,B-8084-OUT,C-9642-B;n:type:ShaderForge.SFN_Normalize,id:8041,x:31696,y:32172,varname:node_8041,prsc:2|IN-5756-OUT;n:type:ShaderForge.SFN_Multiply,id:9454,x:32098,y:32292,varname:node_9454,prsc:2|A-2766-OUT,B-2766-OUT;n:type:ShaderForge.SFN_Slider,id:813,x:30994,y:32423,ptovrint:False,ptlb:floorWeight,ptin:_floorWeight,varname:node_813,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:3,max:5;proporder:3722-7300-7217-813;pass:END;sub:END;*/

Shader "Shader Forge/PlanarShader" {
    Properties {
        _FloorTex ("FloorTex", 2D) = "white" {}
        _zTex ("zTex", 2D) = "white" {}
        _xTex ("xTex", 2D) = "white" {}
        _floorWeight ("floorWeight", Range(1, 5)) = 3
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _FloorTex; uniform float4 _FloorTex_ST;
            uniform sampler2D _zTex; uniform float4 _zTex_ST;
            uniform sampler2D _xTex; uniform float4 _xTex_ST;
            uniform float _floorWeight;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float3 tangentDir : TEXCOORD4;
                float3 bitangentDir : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - 0;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 node_9642 = i.normalDir.rgb;
                float3 node_2766 = abs(normalize(float3(node_9642.r,(node_9642.g*_floorWeight),node_9642.b)));
                float3 node_9454 = (node_2766*node_2766);
                float2 node_9650 = float2(i.posWorld.b,i.posWorld.g);
                float4 _zTex_var = tex2D(_zTex,TRANSFORM_TEX(node_9650, _zTex));
                float2 node_57 = float2(i.posWorld.b,i.posWorld.r);
                float4 _FloorTex_var = tex2D(_FloorTex,TRANSFORM_TEX(node_57, _FloorTex));
                float2 node_6495 = float2(i.posWorld.r,i.posWorld.g);
                float4 _xTex_var = tex2D(_xTex,TRANSFORM_TEX(node_6495, _xTex));
                float3 diffuseColor = (node_9454.r*_zTex_var.rgb + node_9454.g*_FloorTex_var.rgb + node_9454.b*_xTex_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _FloorTex; uniform float4 _FloorTex_ST;
            uniform sampler2D _zTex; uniform float4 _zTex_ST;
            uniform sampler2D _xTex; uniform float4 _xTex_ST;
            uniform float _floorWeight;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float3 tangentDir : TEXCOORD4;
                float3 bitangentDir : TEXCOORD5;
                LIGHTING_COORDS(6,7)
                UNITY_FOG_COORDS(8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_9642 = i.normalDir.rgb;
                float3 node_2766 = abs(normalize(float3(node_9642.r,(node_9642.g*_floorWeight),node_9642.b)));
                float3 node_9454 = (node_2766*node_2766);
                float2 node_9650 = float2(i.posWorld.b,i.posWorld.g);
                float4 _zTex_var = tex2D(_zTex,TRANSFORM_TEX(node_9650, _zTex));
                float2 node_57 = float2(i.posWorld.b,i.posWorld.r);
                float4 _FloorTex_var = tex2D(_FloorTex,TRANSFORM_TEX(node_57, _FloorTex));
                float2 node_6495 = float2(i.posWorld.r,i.posWorld.g);
                float4 _xTex_var = tex2D(_xTex,TRANSFORM_TEX(node_6495, _xTex));
                float3 diffuseColor = (node_9454.r*_zTex_var.rgb + node_9454.g*_FloorTex_var.rgb + node_9454.b*_xTex_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _FloorTex; uniform float4 _FloorTex_ST;
            uniform sampler2D _zTex; uniform float4 _zTex_ST;
            uniform sampler2D _xTex; uniform float4 _xTex_ST;
            uniform float _floorWeight;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float3 node_9642 = i.normalDir.rgb;
                float3 node_2766 = abs(normalize(float3(node_9642.r,(node_9642.g*_floorWeight),node_9642.b)));
                float3 node_9454 = (node_2766*node_2766);
                float2 node_9650 = float2(i.posWorld.b,i.posWorld.g);
                float4 _zTex_var = tex2D(_zTex,TRANSFORM_TEX(node_9650, _zTex));
                float2 node_57 = float2(i.posWorld.b,i.posWorld.r);
                float4 _FloorTex_var = tex2D(_FloorTex,TRANSFORM_TEX(node_57, _FloorTex));
                float2 node_6495 = float2(i.posWorld.r,i.posWorld.g);
                float4 _xTex_var = tex2D(_xTex,TRANSFORM_TEX(node_6495, _xTex));
                float3 diffColor = (node_9454.r*_zTex_var.rgb + node_9454.g*_FloorTex_var.rgb + node_9454.b*_xTex_var.rgb);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
