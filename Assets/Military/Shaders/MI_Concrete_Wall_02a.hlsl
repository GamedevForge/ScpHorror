#define NUM_TEX_COORD_INTERPOLATORS 1
#define NUM_MATERIAL_TEXCOORDS_VERTEX 1
#define NUM_CUSTOM_VERTEX_INTERPOLATORS 0

struct Input
{
	//float3 Normal;
	float2 uv_MainTex : TEXCOORD0;
	float2 uv2_Material_Texture2D_0 : TEXCOORD1;
	float4 color : COLOR;
	float4 tangent;
	//float4 normal;
	float3 viewDir;
	float4 screenPos;
	float3 worldPos;
	//float3 worldNormal;
	float3 normal2;
};
struct SurfaceOutputStandard
{
	float3 Albedo;		// base (diffuse or specular) color
	float3 Normal;		// tangent space normal, if written
	half3 Emission;
	half Metallic;		// 0=non-metal, 1=metal
	// Smoothness is the user facing name, it should be perceptual smoothness but user should not have to deal with it.
	// Everywhere in the code you meet smoothness it is perceptual smoothness
	half Smoothness;	// 0=rough, 1=smooth
	half Occlusion;		// occlusion (default 1)
	float Alpha;		// alpha for transparencies
};

#define HDRP 1
//#define URP 1
#define UE5
//#define HAS_CUSTOMIZED_UVS 1
#define MATERIAL_TANGENTSPACENORMAL 1
//struct Material
//{
	//samplers start
SAMPLER( SamplerState_Linear_Repeat );
SAMPLER( SamplerState_Linear_Clamp );
TEXTURE2D(       Material_Texture2D_0 );
SAMPLER(  samplerMaterial_Texture2D_0 );
float4 Material_Texture2D_0_TexelSize;
float4 Material_Texture2D_0_ST;
TEXTURE2D(       Material_Texture2D_1 );
SAMPLER(  samplerMaterial_Texture2D_1 );
float4 Material_Texture2D_1_TexelSize;
float4 Material_Texture2D_1_ST;
TEXTURE2D(       Material_Texture2D_2 );
SAMPLER(  samplerMaterial_Texture2D_2 );
float4 Material_Texture2D_2_TexelSize;
float4 Material_Texture2D_2_ST;
TEXTURE2D(       Material_Texture2D_3 );
SAMPLER(  samplerMaterial_Texture2D_3 );
float4 Material_Texture2D_3_TexelSize;
float4 Material_Texture2D_3_ST;
TEXTURE2D(       Material_Texture2D_4 );
SAMPLER(  samplerMaterial_Texture2D_4 );
float4 Material_Texture2D_4_TexelSize;
float4 Material_Texture2D_4_ST;
TEXTURE2D(       Material_Texture2D_5 );
SAMPLER(  samplerMaterial_Texture2D_5 );
float4 Material_Texture2D_5_TexelSize;
float4 Material_Texture2D_5_ST;
TEXTURE2D(       Material_Texture2D_6 );
SAMPLER(  samplerMaterial_Texture2D_6 );
float4 Material_Texture2D_6_TexelSize;
float4 Material_Texture2D_6_ST;
TEXTURE2D(       Material_Texture2D_7 );
SAMPLER(  samplerMaterial_Texture2D_7 );
float4 Material_Texture2D_7_TexelSize;
float4 Material_Texture2D_7_ST;
TEXTURE2D(       Material_Texture2D_8 );
SAMPLER(  samplerMaterial_Texture2D_8 );
float4 Material_Texture2D_8_TexelSize;
float4 Material_Texture2D_8_ST;
TEXTURE2D(       Material_Texture2D_9 );
SAMPLER(  samplerMaterial_Texture2D_9 );
float4 Material_Texture2D_9_TexelSize;
float4 Material_Texture2D_9_ST;

//};

#ifdef UE5
	#define UE_LWC_RENDER_TILE_SIZE			2097152.0
	#define UE_LWC_RENDER_TILE_SIZE_SQRT	1448.15466
	#define UE_LWC_RENDER_TILE_SIZE_RSQRT	0.000690533954
	#define UE_LWC_RENDER_TILE_SIZE_RCP		4.76837158e-07
	#define UE_LWC_RENDER_TILE_SIZE_FMOD_PI		0.673652053
	#define UE_LWC_RENDER_TILE_SIZE_FMOD_2PI	0.673652053
	#define INVARIANT(X) X
	#define PI 					(3.1415926535897932)

	#include "LargeWorldCoordinates.hlsl"
#endif
struct MaterialStruct
{
	float4 PreshaderBuffer[8];
	float4 ScalarExpressions[1];
	float VTPackedPageTableUniform[2];
	float VTPackedUniform[1];
};
static SamplerState View_MaterialTextureBilinearWrapedSampler;
static SamplerState View_MaterialTextureBilinearClampedSampler;
struct ViewStruct
{
	float GameTime;
	float RealTime;
	float DeltaTime;
	float PrevFrameGameTime;
	float PrevFrameRealTime;
	float MaterialTextureMipBias;	
	float4 PrimitiveSceneData[ 40 ];
	float4 TemporalAAParams;
	float2 ViewRectMin;
	float4 ViewSizeAndInvSize;
	float MaterialTextureDerivativeMultiply;
	uint StateFrameIndexMod8;
	float FrameNumber;
	float2 FieldOfViewWideAngles;
	float4 RuntimeVirtualTextureMipLevel;
	float PreExposure;
	float4 BufferBilinearUVMinMax;
};
struct ResolvedViewStruct
{
	#ifdef UE5
		FLWCVector3 WorldCameraOrigin;
		FLWCVector3 PrevWorldCameraOrigin;
		FLWCVector3 PreViewTranslation;
		FLWCVector3 WorldViewOrigin;
	#else
		float3 WorldCameraOrigin;
		float3 PrevWorldCameraOrigin;
		float3 PreViewTranslation;
		float3 WorldViewOrigin;
	#endif
	float4 ScreenPositionScaleBias;
	float4x4 TranslatedWorldToView;
	float4x4 TranslatedWorldToCameraView;
	float4x4 TranslatedWorldToClip;
	float4x4 ViewToTranslatedWorld;
	float4x4 PrevViewToTranslatedWorld;
	float4x4 CameraViewToTranslatedWorld;
	float4 BufferBilinearUVMinMax;
	float4 XRPassthroughCameraUVs[ 2 ];
};
struct PrimitiveStruct
{
	float4x4 WorldToLocal;
	float4x4 LocalToWorld;
};

static ViewStruct View;
static ResolvedViewStruct ResolvedView;
static PrimitiveStruct Primitive;
uniform float4 View_BufferSizeAndInvSize;
uniform float4 LocalObjectBoundsMin;
uniform float4 LocalObjectBoundsMax;
static SamplerState Material_Wrap_WorldGroupSettings;
static SamplerState Material_Clamp_WorldGroupSettings;

#include "UnrealCommon.cginc"

static MaterialStruct Material;
void InitializeExpressions()
{
	Material.PreshaderBuffer[0] = float4(1.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(1.000000,0.000000,1.000000,0.979166);//(Unknown)
	Material.PreshaderBuffer[2] = float4(0.633598,485.499969,-484.499969,8.000000);//(Unknown)
	Material.PreshaderBuffer[3] = float4(0.500000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[5] = float4(0.958333,0.973911,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[6] = float4(1.000000,1.000000,1.000000,1.000000);//(Unknown)
	Material.PreshaderBuffer[7] = float4(0.000000,0.000000,1.000000,1.000000);//(Unknown)
}struct MaterialCollection0Type
{
	float4 Vectors[3];
};
//PC_FiringRange
MaterialCollection0Type MaterialCollection0;
void Initialize_MaterialCollection0()
{
	MaterialCollection0.Vectors[0] = float4(1.000000,2.000000,0.200000,-19.844847);//Stobe Lights,Strobe High,Strobe Low,Paint Height
	MaterialCollection0.Vectors[1] = float4(289.616547,0.711583,0.000000,0.000000);//Paint Contrast,Mask Multi,,
	MaterialCollection0.Vectors[2] = float4(0.383301,0.436536,0.468750,0.000000);//Paint Color
}
float3 GetMaterialWorldPositionOffset(FMaterialVertexParameters Parameters)
{
	return MaterialFloat3(0.00000000,0.00000000,0.00000000);;
}
void CalcPixelMaterialInputs(in out FMaterialPixelParameters Parameters, in out FPixelMaterialInputs PixelMaterialInputs)
{
	//WorldAligned texturing & others use normals & stuff that think Z is up
	Parameters.TangentToWorld[0] = Parameters.TangentToWorld[0].xzy;
	Parameters.TangentToWorld[1] = Parameters.TangentToWorld[1].xzy;
	Parameters.TangentToWorld[2] = Parameters.TangentToWorld[2].xzy;

	float3 WorldNormalCopy = Parameters.WorldNormal;

	// Initial calculations (required for Normal)
	MaterialFloat2 Local0 = Parameters.TexCoords[0].xy;
	MaterialFloat2 Local1 = (DERIV_BASE_VALUE(Local0) * ((MaterialFloat2)Material.PreshaderBuffer[0].x));
	MaterialFloat2 Local2 = (DERIV_BASE_VALUE(Local1) + Material.PreshaderBuffer[0].yz);
	MaterialFloat Local3 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local2), 1);
	MaterialFloat4 Local4 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,DERIV_BASE_VALUE(Local2),View.MaterialTextureMipBias));
	MaterialFloat Local5 = MaterialStoreTexSample(Parameters, Local4, 1);
	MaterialFloat3 Local6 = lerp(Local4.rgb,MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb,Material.PreshaderBuffer[0].w);
	MaterialFloat2 Local7 = (DERIV_BASE_VALUE(Local0) * ((MaterialFloat2)Material.PreshaderBuffer[1].x));
	MaterialFloat Local8 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local7), 1);
	MaterialFloat4 Local9 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,DERIV_BASE_VALUE(Local7),View.MaterialTextureMipBias));
	MaterialFloat Local10 = MaterialStoreTexSample(Parameters, Local9, 1);
	MaterialFloat3 Local11 = lerp(Local9.rgb,MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb,Material.PreshaderBuffer[1].y);
	MaterialFloat2 Local12 = (DERIV_BASE_VALUE(Local0) * ((MaterialFloat2)Material.PreshaderBuffer[1].z));
	MaterialFloat Local13 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local12), 0);
	MaterialFloat4 Local14 = Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local12),View.MaterialTextureMipBias);
	MaterialFloat Local15 = MaterialStoreTexSample(Parameters, Local14, 0);
	MaterialFloat Local16 = (1.00000000 - Local14.rgb.g);
	MaterialFloat Local17 = (Local16 * Material.PreshaderBuffer[1].w);
	MaterialFloat Local18 = PositiveClampedPow(Local17,Material.PreshaderBuffer[2].x);
	MaterialFloat Local19 = (Local18 - 1.00000000);
	MaterialFloat4 Local20 = Parameters.VertexColor;
	MaterialFloat Local21 = DERIV_BASE_VALUE(Local20).r;
	MaterialFloat Local22 = (1.00000000 - DERIV_BASE_VALUE(Local21));
	MaterialFloat Local23 = (DERIV_BASE_VALUE(Local22) * 2.00000000);
	MaterialFloat Local24 = (Local19 + DERIV_BASE_VALUE(Local23));
	MaterialFloat Local25 = saturate(Local24);
	MaterialFloat Local26 = lerp(Material.PreshaderBuffer[2].z,Material.PreshaderBuffer[2].y,Local25);
	MaterialFloat Local27 = saturate(Local26);
	MaterialFloat3 Local28 = lerp(Local6,Local11,Local27.r.r);
	MaterialFloat Local29 = (Local28.b + 1.00000000);
	MaterialFloat2 Local30 = (DERIV_BASE_VALUE(Local0) * ((MaterialFloat2)Material.PreshaderBuffer[2].w));
	MaterialFloat Local31 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local30), 10);
	MaterialFloat4 Local32 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,DERIV_BASE_VALUE(Local30),View.MaterialTextureMipBias));
	MaterialFloat Local33 = MaterialStoreTexSample(Parameters, Local32, 10);
	MaterialFloat3 Local34 = lerp(Local32.rgb,MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb,Material.PreshaderBuffer[3].x);
	MaterialFloat2 Local35 = (Local34.rg * ((MaterialFloat2)-1.00000000));
	MaterialFloat Local36 = dot(MaterialFloat3(Local28.rg,Local29),MaterialFloat3(Local35,Local34.b));
	MaterialFloat3 Local37 = (MaterialFloat3(Local28.rg,Local29) * ((MaterialFloat3)Local36));
	MaterialFloat3 Local38 = (((MaterialFloat3)Local29) * MaterialFloat3(Local35,Local34.b));
	MaterialFloat3 Local39 = (Local37 - Local38);
	MaterialFloat3 Local40 = normalize(Local39);

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local40;


#if TEMPLATE_USES_SUBSTRATE
	Parameters.SubstratePixelFootprint = SubstrateGetPixelFootprint(Parameters.WorldPosition_CamRelative, GetRoughnessFromNormalCurvature(Parameters));
	Parameters.SharedLocalBases = SubstrateInitialiseSharedLocalBases();
	Parameters.SubstrateTree = GetInitialisedSubstrateTree();
#if SUBSTRATE_USE_FULLYSIMPLIFIED_MATERIAL == 1
	Parameters.SharedLocalBasesFullySimplified = SubstrateInitialiseSharedLocalBases();
	Parameters.SubstrateTreeFullySimplified = GetInitialisedSubstrateTree();
#endif
#endif

	// Note that here MaterialNormal can be in world space or tangent space
	float3 MaterialNormal = GetMaterialNormal(Parameters, PixelMaterialInputs);

#if MATERIAL_TANGENTSPACENORMAL

#if FEATURE_LEVEL >= FEATURE_LEVEL_SM4
	// Mobile will rely on only the final normalize for performance
	MaterialNormal = normalize(MaterialNormal);
#endif

	// normalizing after the tangent space to world space conversion improves quality with sheared bases (UV layout to WS causes shrearing)
	// use full precision normalize to avoid overflows
	Parameters.WorldNormal = TransformTangentNormalToWorld(Parameters.TangentToWorld, MaterialNormal);

#else //MATERIAL_TANGENTSPACENORMAL

	Parameters.WorldNormal = normalize(MaterialNormal);

#endif //MATERIAL_TANGENTSPACENORMAL

#if MATERIAL_TANGENTSPACENORMAL || TWO_SIDED_WORLD_SPACE_SINGLELAYERWATER_NORMAL
	// flip the normal for backfaces being rendered with a two-sided material
	Parameters.WorldNormal *= Parameters.TwoSidedSign;
#endif

	Parameters.ReflectionVector = ReflectionAboutCustomWorldNormal(Parameters, Parameters.WorldNormal, false);

#if !PARTICLE_SPRITE_FACTORY
	Parameters.Particle.MotionBlurFade = 1.0f;
#endif // !PARTICLE_SPRITE_FACTORY

	// Now the rest of the inputs
	MaterialFloat3 Local41 = lerp(((MaterialFloat3)0.00000000),Material.PreshaderBuffer[4].xyz,Material.PreshaderBuffer[3].y);
	MaterialFloat Local42 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local2), 2);
	MaterialFloat4 Local43 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local2),View.MaterialTextureMipBias));
	MaterialFloat Local44 = MaterialStoreTexSample(Parameters, Local43, 2);
	MaterialFloat3 Local45 = (Local43.rgb * Material.PreshaderBuffer[5].xyz);
	MaterialFloat4 Local46 = MaterialCollection0.Vectors[2];
	MaterialFloat3 Local47 = (Local45 * Local46.rgba.rgb);
	MaterialFloat Local48 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local2), 0);
	MaterialFloat4 Local49 = Texture2DSampleBias(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local2),View.MaterialTextureMipBias);
	MaterialFloat Local50 = MaterialStoreTexSample(Parameters, Local49, 0);
	MaterialFloat4 Local51 = MaterialCollection0.Vectors[1];
	MaterialFloat Local52 = (Local49.g * Local51.g);
	FWSVector3 Local53 = GetWorldPosition(Parameters);
	FWSVector3 Local54 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local53)), WSGetY(DERIV_BASE_VALUE(Local53)), WSGetZ(DERIV_BASE_VALUE(Local53)));
	MaterialFloat4 Local55 = MaterialCollection0.Vectors[0];
	MaterialFloat3 Local56 = (MaterialFloat3(MaterialFloat2(0.00000000,0.00000000),Local55.a) * ((MaterialFloat3)10.00000000));
	FWSVector3 Local57 = WSAdd(DERIV_BASE_VALUE(Local54), Local56);
	FWSScalar Local58 = WSGetZ(DERIV_BASE_VALUE(Local57));
	MaterialFloat Local59 = (Local51.r / 1000.00000000);
	FWSScalar Local60 = WSDot(DERIV_BASE_VALUE(Local58), WSPromote(Local59));
	MaterialFloat Local61 = WSSaturateDemote(DERIV_BASE_VALUE(Local60));
	FWSVector3 Local62 = WSDivideByPow2(DERIV_BASE_VALUE(Local54), MaterialFloat3(-1024.00000000,-1024.00000000,-1024.00000000));
	FWSVector2 Local63 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local62)), WSGetZ(DERIV_BASE_VALUE(Local62)));
	MaterialFloat2 Local64 = WSApplyAddressMode(DERIV_BASE_VALUE(Local63), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local65 = MaterialStoreTexCoordScale(Parameters, Local64, 5);
	MaterialFloat4 Local66 = Texture2DSample(Material_Texture2D_6,GetMaterialSharedSampler(samplerMaterial_Texture2D_6,View_MaterialTextureBilinearWrapedSampler),Local64);
	MaterialFloat Local67 = MaterialStoreTexSample(Parameters, Local66, 5);
	FWSVector2 Local68 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local62)), WSGetZ(DERIV_BASE_VALUE(Local62)));
	MaterialFloat2 Local69 = WSApplyAddressMode(DERIV_BASE_VALUE(Local68), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local70 = MaterialStoreTexCoordScale(Parameters, Local69, 5);
	MaterialFloat4 Local71 = Texture2DSample(Material_Texture2D_6,GetMaterialSharedSampler(samplerMaterial_Texture2D_6,View_MaterialTextureBilinearWrapedSampler),Local69);
	MaterialFloat Local72 = MaterialStoreTexSample(Parameters, Local71, 5);
	MaterialFloat Local73 = abs(Parameters.TangentToWorld[2].r);
	MaterialFloat Local74 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),Local73);
	MaterialFloat Local75 = saturate(Local74);
	MaterialFloat4 Local76 = lerp(MaterialFloat4(Local66.rgb,Local66.a),MaterialFloat4(Local71.rgb,Local71.a),Local75.r.r);
	MaterialFloat4 Local77 = (((MaterialFloat4)DERIV_BASE_VALUE(Local61)) * Local76);
	MaterialFloat4 Local78 = (Local77 * ((MaterialFloat4)3.50000000));
	MaterialFloat4 Local79 = saturate(Local78);
	MaterialFloat Local80 = lerp((0.00000000 - 1.31528902),(1.31528902 + 1.00000000),Local79.x);
	MaterialFloat Local81 = saturate(Local80);
	MaterialFloat Local82 = (1.00000000 - Local81.r);
	MaterialFloat Local83 = saturate(Local82);
	MaterialFloat Local84 = (Local52 * Local83);
	MaterialFloat Local85 = saturate(Local84);
	MaterialFloat3 Local86 = lerp(Local45,Local47,Local85);
	MaterialFloat Local87 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local7), 2);
	MaterialFloat4 Local88 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_7,samplerMaterial_Texture2D_7,DERIV_BASE_VALUE(Local7),View.MaterialTextureMipBias));
	MaterialFloat Local89 = MaterialStoreTexSample(Parameters, Local88, 2);
	MaterialFloat3 Local90 = (Local88.rgb * Material.PreshaderBuffer[6].xyz);
	MaterialFloat3 Local91 = lerp(Local86,Local90,Local27.r.r);
	MaterialFloat Local92 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local30), 9);
	MaterialFloat4 Local93 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_8,samplerMaterial_Texture2D_8,DERIV_BASE_VALUE(Local30),View.MaterialTextureMipBias));
	MaterialFloat Local94 = MaterialStoreTexSample(Parameters, Local93, 9);
	MaterialFloat3 Local95 = (Local91 * Local93.rgb);
	MaterialFloat3 Local96 = lerp(Local91,Local95,Material.PreshaderBuffer[6].w);
	MaterialFloat Local97 = lerp(Material.PreshaderBuffer[7].x,Material.PreshaderBuffer[7].y,Local27.r.r);
	MaterialFloat Local98 = (Local49.b * 0.50000000);
	MaterialFloat Local99 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local7), 0);
	MaterialFloat4 Local100 = Texture2DSampleBias(Material_Texture2D_9,samplerMaterial_Texture2D_9,DERIV_BASE_VALUE(Local7),View.MaterialTextureMipBias);
	MaterialFloat Local101 = MaterialStoreTexSample(Parameters, Local100, 0);
	MaterialFloat Local102 = (Local100.b * 0.50000000);
	MaterialFloat Local103 = lerp(Local98,Local102,Local27.r.r);
	MaterialFloat Local104 = PositiveClampedPow(Local49.r,Material.PreshaderBuffer[7].z);
	MaterialFloat Local105 = (Local104 * Material.PreshaderBuffer[7].w);
	MaterialFloat Local106 = saturate(Local105);
	MaterialFloat Local107 = saturate(Local100.r);
	MaterialFloat Local108 = lerp(Local106,Local107,Local27.r.r);
	MaterialFloat Local109 = lerp(Local49.b,Local100.b,Local27.r.r);

	PixelMaterialInputs.EmissiveColor = Local41;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = Local96;
	PixelMaterialInputs.Metallic = Local97;
	PixelMaterialInputs.Specular = Local103;
	PixelMaterialInputs.Roughness = Local108;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local40;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = Local109;
	PixelMaterialInputs.Refraction = 0;
	PixelMaterialInputs.PixelDepthOffset = 0.00000000;
	PixelMaterialInputs.ShadingModel = 1;
	PixelMaterialInputs.FrontMaterial = GetInitialisedSubstrateData();
	PixelMaterialInputs.SurfaceThickness = 0.01000000;
	PixelMaterialInputs.Displacement = 0.50000000;


#if MATERIAL_USES_ANISOTROPY
	Parameters.WorldTangent = CalculateAnisotropyTangent(Parameters, PixelMaterialInputs);
#else
	Parameters.WorldTangent = 0;
#endif
}

#define UnityObjectToWorldDir TransformObjectToWorld

void SetupCommonData( int Parameters_PrimitiveId )
{
	View_MaterialTextureBilinearWrapedSampler = SamplerState_Linear_Repeat;
	View_MaterialTextureBilinearClampedSampler = SamplerState_Linear_Clamp;

	Material_Wrap_WorldGroupSettings = SamplerState_Linear_Repeat;
	Material_Clamp_WorldGroupSettings = SamplerState_Linear_Clamp;

	View.GameTime = View.RealTime = _Time.y;// _Time is (t/20, t, t*2, t*3)
	View.PrevFrameGameTime = View.GameTime - unity_DeltaTime.x;//(dt, 1/dt, smoothDt, 1/smoothDt)
	View.PrevFrameRealTime = View.RealTime;
	View.DeltaTime = unity_DeltaTime.x;
	View.MaterialTextureMipBias = 0.0;
	View.TemporalAAParams = float4( 0, 0, 0, 0 );
	View.ViewRectMin = float2( 0, 0 );
	View.ViewSizeAndInvSize = View_BufferSizeAndInvSize;
	View.MaterialTextureDerivativeMultiply = 1.0f;
	View.StateFrameIndexMod8 = 0;
	View.FrameNumber = (int)_Time.y;
	View.FieldOfViewWideAngles = float2( PI * 0.42f, PI * 0.42f );//75degrees, default unity
	View.RuntimeVirtualTextureMipLevel = float4( 0, 0, 0, 0 );
	View.PreExposure = 0;
	View.BufferBilinearUVMinMax = float4(
		View_BufferSizeAndInvSize.z * ( 0 + 0.5 ),//EffectiveViewRect.Min.X
		View_BufferSizeAndInvSize.w * ( 0 + 0.5 ),//EffectiveViewRect.Min.Y
		View_BufferSizeAndInvSize.z * ( View_BufferSizeAndInvSize.x - 0.5 ),//EffectiveViewRect.Max.X
		View_BufferSizeAndInvSize.w * ( View_BufferSizeAndInvSize.y - 0.5 ) );//EffectiveViewRect.Max.Y

	for( int i2 = 0; i2 < 40; i2++ )
		View.PrimitiveSceneData[ i2 ] = float4( 0, 0, 0, 0 );

	float4x4 LocalToWorld = transpose( UNITY_MATRIX_M );
    LocalToWorld[3] = float4(ToUnrealPos(LocalToWorld[3]), LocalToWorld[3].w);
	float4x4 WorldToLocal = transpose( UNITY_MATRIX_I_M );
	float4x4 ViewMatrix = transpose( UNITY_MATRIX_V );
	float4x4 InverseViewMatrix = transpose( UNITY_MATRIX_I_V );
	float4x4 ViewProjectionMatrix = transpose( UNITY_MATRIX_VP );
	uint PrimitiveBaseOffset = Parameters_PrimitiveId * PRIMITIVE_SCENE_DATA_STRIDE;
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 0 ] = LocalToWorld[ 0 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 1 ] = LocalToWorld[ 1 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 2 ] = LocalToWorld[ 2 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 3 ] = LocalToWorld[ 3 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 5 ] = float4( ToUnrealPos( SHADERGRAPH_OBJECT_POSITION ), 100.0 );//ObjectWorldPosition
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 6 ] = WorldToLocal[ 0 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 7 ] = WorldToLocal[ 1 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 8 ] = WorldToLocal[ 2 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 9 ] = WorldToLocal[ 3 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 10 ] = LocalToWorld[ 0 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 11 ] = LocalToWorld[ 1 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 12 ] = LocalToWorld[ 2 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 13 ] = LocalToWorld[ 3 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 18 ] = float4( ToUnrealPos( SHADERGRAPH_OBJECT_POSITION ), 0 );//ActorWorldPosition
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 19 ] = LocalObjectBoundsMax - LocalObjectBoundsMin;//ObjectBounds
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 21 ] = mul( LocalToWorld, float3( 1, 0, 0 ) );
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 23 ] = LocalObjectBoundsMin;//LocalObjectBoundsMin 
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 24 ] = LocalObjectBoundsMax;//LocalObjectBoundsMax

#ifdef UE5
	ResolvedView.WorldCameraOrigin = LWCPromote( ToUnrealPos( _WorldSpaceCameraPos.xyz ) );
	ResolvedView.PreViewTranslation = LWCPromote( float3( 0, 0, 0 ) );
	ResolvedView.WorldViewOrigin = LWCPromote( float3( 0, 0, 0 ) );
#else
	ResolvedView.WorldCameraOrigin = ToUnrealPos( _WorldSpaceCameraPos.xyz );
	ResolvedView.PreViewTranslation = float3( 0, 0, 0 );
	ResolvedView.WorldViewOrigin = float3( 0, 0, 0 );
#endif
	ResolvedView.PrevWorldCameraOrigin = ResolvedView.WorldCameraOrigin;
	ResolvedView.ScreenPositionScaleBias = float4( 1, 1, 0, 0 );
	ResolvedView.TranslatedWorldToView		 = ViewMatrix;
	ResolvedView.TranslatedWorldToCameraView = ViewMatrix;
	ResolvedView.TranslatedWorldToClip		 = ViewProjectionMatrix;
	ResolvedView.ViewToTranslatedWorld		 = InverseViewMatrix;
	ResolvedView.PrevViewToTranslatedWorld = ResolvedView.ViewToTranslatedWorld;
	ResolvedView.CameraViewToTranslatedWorld = InverseViewMatrix;
	ResolvedView.BufferBilinearUVMinMax = View.BufferBilinearUVMinMax;
	Primitive.WorldToLocal = WorldToLocal;
	Primitive.LocalToWorld = LocalToWorld;
}
#define VS_USES_UNREAL_SPACE 1
float3 PrepareAndGetWPO( float4 VertexColor, float3 UnrealWorldPos, float3 UnrealNormal, float4 InTangent,
						 float4 UV0, float4 UV1 )
{
	InitializeExpressions();
	Initialize_MaterialCollection0();

	FMaterialVertexParameters Parameters = (FMaterialVertexParameters)0;

	float3 InWorldNormal = UnrealNormal;
	float4 tangentWorld = InTangent;
	tangentWorld.xyz = normalize( tangentWorld.xyz );
	//float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
	Parameters.TangentToWorld = float3x3( normalize( cross( InWorldNormal, tangentWorld.xyz ) * tangentWorld.w ), tangentWorld.xyz, InWorldNormal );

	
	#ifdef VS_USES_UNREAL_SPACE
		UnrealWorldPos = ToUnrealPos( UnrealWorldPos );
	#endif
	Parameters.WorldPosition = UnrealWorldPos;
	#ifdef VS_USES_UNREAL_SPACE
		Parameters.TangentToWorld[ 0 ] = Parameters.TangentToWorld[ 0 ].xzy;
		Parameters.TangentToWorld[ 1 ] = Parameters.TangentToWorld[ 1 ].xzy;
		Parameters.TangentToWorld[ 2 ] = Parameters.TangentToWorld[ 2 ].xzy;//WorldAligned texturing uses normals that think Z is up
	#endif

	Parameters.VertexColor = VertexColor;

#if NUM_MATERIAL_TEXCOORDS_VERTEX > 0			
	Parameters.TexCoords[ 0 ] = float2( UV0.x, UV0.y );
#endif
#if NUM_MATERIAL_TEXCOORDS_VERTEX > 1
	Parameters.TexCoords[ 1 ] = float2( UV1.x, UV1.y );
#endif
#if NUM_MATERIAL_TEXCOORDS_VERTEX > 2
	for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
	{
		Parameters.TexCoords[ i ] = float2( UV0.x, UV0.y );
	}
#endif

	Parameters.PrimitiveId = 0;

	SetupCommonData( Parameters.PrimitiveId );

#ifdef UE5
	Parameters.PrevFrameLocalToWorld = MakeLWCMatrix( float3( 0, 0, 0 ), Primitive.LocalToWorld );
#else
	Parameters.PrevFrameLocalToWorld = Primitive.LocalToWorld;
#endif
	
	float3 Offset = float3( 0, 0, 0 );
	Offset = GetMaterialWorldPositionOffset( Parameters );
	#ifdef VS_USES_UNREAL_SPACE
		//Convert from unreal units to unity
		Offset /= float3( 100, 100, 100 );
		Offset = Offset.xzy;
	#endif
	return Offset;
}

void SurfaceReplacement( Input In, out SurfaceOutputStandard o )
{
	InitializeExpressions();
	Initialize_MaterialCollection0();


	float3 Z3 = float3( 0, 0, 0 );
	float4 Z4 = float4( 0, 0, 0, 0 );

	float3 UnrealWorldPos = float3( In.worldPos.x, In.worldPos.y, In.worldPos.z );

	float3 UnrealNormal = In.normal2;	

	FMaterialPixelParameters Parameters = (FMaterialPixelParameters)0;
#if NUM_TEX_COORD_INTERPOLATORS > 0			
	Parameters.TexCoords[ 0 ] = float2( In.uv_MainTex.x, 1.0 - In.uv_MainTex.y );
#endif
#if NUM_TEX_COORD_INTERPOLATORS > 1
	Parameters.TexCoords[ 1 ] = float2( In.uv2_Material_Texture2D_0.x, 1.0 - In.uv2_Material_Texture2D_0.y );
#endif
#if NUM_TEX_COORD_INTERPOLATORS > 2
	for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
	{
		Parameters.TexCoords[ i ] = float2( In.uv_MainTex.x, 1.0 - In.uv_MainTex.y );
	}
#endif
	Parameters.VertexColor = In.color;
	Parameters.WorldNormal = UnrealNormal;
	Parameters.ReflectionVector = half3( 0, 0, 1 );
	Parameters.CameraVector = normalize( _WorldSpaceCameraPos.xyz - UnrealWorldPos.xyz );
	//Parameters.CameraVector = mul( ( float3x3 )unity_CameraToWorld, float3( 0, 0, 1 ) ) * -1;
	Parameters.LightVector = half3( 0, 0, 0 );
	//float4 screenpos = In.screenPos;
	//screenpos /= screenpos.w;
	Parameters.SvPosition = In.screenPos;
	Parameters.ScreenPosition = Parameters.SvPosition;

	Parameters.UnMirrored = 1;

	Parameters.TwoSidedSign = 1;


	float3 InWorldNormal = UnrealNormal;	
	float4 tangentWorld = In.tangent;
	tangentWorld.xyz = normalize( tangentWorld.xyz );
	//float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
	Parameters.TangentToWorld = float3x3( normalize( cross( InWorldNormal, tangentWorld.xyz ) * tangentWorld.w ), tangentWorld.xyz, InWorldNormal );

	//WorldAlignedTexturing in UE relies on the fact that coords there are 100x larger, prepare values for that
	//but watch out for any computation that might get skewed as a side effect
	UnrealWorldPos = ToUnrealPos( UnrealWorldPos );
	
	Parameters.AbsoluteWorldPosition = UnrealWorldPos;
	Parameters.WorldPosition_CamRelative = UnrealWorldPos;
	Parameters.WorldPosition_NoOffsets = UnrealWorldPos;

	Parameters.WorldPosition_NoOffsets_CamRelative = Parameters.WorldPosition_CamRelative;
	Parameters.LightingPositionOffset = float3( 0, 0, 0 );

	Parameters.AOMaterialMask = 0;

	Parameters.Particle.RelativeTime = 0;
	Parameters.Particle.MotionBlurFade;
	Parameters.Particle.Random = 0;
	Parameters.Particle.Velocity = half4( 1, 1, 1, 1 );
	Parameters.Particle.Color = half4( 1, 1, 1, 1 );
	Parameters.Particle.TranslatedWorldPositionAndSize = float4( UnrealWorldPos, 0 );
	Parameters.Particle.MacroUV = half4( 0, 0, 1, 1 );
	Parameters.Particle.DynamicParameter = half4( 0, 0, 0, 0 );
	Parameters.Particle.LocalToWorld = float4x4( Z4, Z4, Z4, Z4 );
	Parameters.Particle.Size = float2( 1, 1 );
	Parameters.Particle.SubUVCoords[ 0 ] = Parameters.Particle.SubUVCoords[ 1 ] = float2( 0, 0 );
	Parameters.Particle.SubUVLerp = 0.0;
	Parameters.TexCoordScalesParams = float2( 0, 0 );
	Parameters.PrimitiveId = 0;
	Parameters.VirtualTextureFeedback = 0;

	FPixelMaterialInputs PixelMaterialInputs = (FPixelMaterialInputs)0;
	PixelMaterialInputs.Normal = float3( 0, 0, 1 );
	PixelMaterialInputs.ShadingModel = 0;
	PixelMaterialInputs.FrontMaterial = 0;

	SetupCommonData( Parameters.PrimitiveId );
	//CustomizedUVs
	#if NUM_TEX_COORD_INTERPOLATORS > 0 && HAS_CUSTOMIZED_UVS
		float2 OutTexCoords[ NUM_TEX_COORD_INTERPOLATORS ];
		//Prevent uninitialized reads
		for( int i = 0; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
		{
			OutTexCoords[ i ] = float2( 0, 0 );
		}
		GetMaterialCustomizedUVs( Parameters, OutTexCoords );
		for( int i = 0; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
		{
			Parameters.TexCoords[ i ] = OutTexCoords[ i ];
		}
	#endif
	//<-
	CalcPixelMaterialInputs( Parameters, PixelMaterialInputs );

	#define HAS_WORLDSPACE_NORMAL 0
	#if HAS_WORLDSPACE_NORMAL
		PixelMaterialInputs.Normal = mul( PixelMaterialInputs.Normal, (MaterialFloat3x3)( transpose( Parameters.TangentToWorld ) ) );
	#endif

	o.Albedo = PixelMaterialInputs.BaseColor.rgb;
	o.Alpha = PixelMaterialInputs.Opacity;
	//if( PixelMaterialInputs.OpacityMask < 0.333 ) discard;

	o.Metallic = PixelMaterialInputs.Metallic;
	o.Smoothness = 1.0 - PixelMaterialInputs.Roughness;
	o.Normal = normalize( PixelMaterialInputs.Normal );
	o.Emission = PixelMaterialInputs.EmissiveColor.rgb;
	o.Occlusion = PixelMaterialInputs.AmbientOcclusion;

	//BLEND_ADDITIVE o.Alpha = ( o.Emission.r + o.Emission.g + o.Emission.b ) / 3;
}