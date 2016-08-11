using System;
using System.Collections.Generic;

public class _3DSFile
{
    public const int  CHUNK_MAIN3DS      = 0x4D4D;

    // Main
    public const int CHUNK_EDIT3DS       = 0x3D3D; 
    public const int CHUNK_KEYF3DS       = 0xB000; 
    public const int CHUNK_VERSION       = 0x0002; 
    public const int CHUNK_MESHVERSION   = 0x3D3E; 

	// -> Sub CHUNK_EDIT3DS
    public const int CHUNK_EDIT_MATERIAL = 0xAFFF; 
    public const int CHUNK_EDIT_OBJECT   = 0x4000; 

	// -> Sub CHUNK_EDIT_MATERIAL
    public const int CHUNK_MATNAME       = 0xA000; 
    public const int CHUNK_MATAMBIENT    = 0xA010; 
    public const int CHUNK_MATDIFFUSE    = 0xA020; 
    public const int CHUNK_MATSPECULAR   = 0xA030; 
    public const int CHUNK_MATSHININESS  = 0xA040; 
    public const int CHUNK_MATSHIN2PCT   = 0xA041; 
    public const int CHUNK_TRANSPARENCY  = 0xA050;
    public const int CHUNK_TRANSPARENCY_FALLOFF  = 0xA052; 
    public const int CHUNK_REFL_BLUR     = 0xA053; 
    public const int CHUNK_TWO_SIDE      = 0xA081; 
    public const int CHUNK_WIRE          = 0xA085; 
    public const int CHUNK_SHADING       = 0xA100; 
    public const int CHUNK_MATTEXMAP     = 0xA200; 
    public const int CHUNK_MATSPECMAP    = 0xA204; 
    public const int CHUNK_MATOPACMAP    = 0xA210; 
    public const int CHUNK_MATREFLMAP    = 0xA220; 
    public const int CHUNK_MATBUMPMAP    = 0xA230; 
    public const int CHUNK_MATMAPFILE    = 0xA300;
    public const int CHUNK_MAT_TEXTILING = 0xA351; 
    public const int CHUNK_MAT_USCALE    = 0xA354; 
    public const int CHUNK_MAT_VSCALE    = 0xA356; 
    public const int CHUNK_MAT_UOFFSET   = 0xA358; 
    public const int CHUNK_MAT_VOFFSET   = 0xA35A; 

    // -> Sub CHUNK_EDIT_OBJECT
    public const int CHUNK_OBJTRIMESH    = 0x4100; 

	// -> Sub CHUNK_OBJTRIMESH
    public const int CHUNK_TRIVERT        = 0x4110; 
    public const int CHUNK_POINTFLAGARRAY = 0x4111; 
    public const int CHUNK_TRIFACE        = 0x4120; 
    public const int CHUNK_TRIFACEMAT     = 0x4130; 
    public const int CHUNK_TRIUV          = 0x4140; 
    public const int CHUNK_TRISMOOTH      = 0x4150; 
    public const int CHUNK_TRIMATRIX      = 0x4160; 
    public const int CHUNK_MESHCOLOR      = 0x4165; 
    public const int CHUNK_DIRECT_LIGHT   = 0x4600; 
    public const int CHUNK_DL_INNER_RANGE = 0x4659; 
    public const int CHUNK_DL_OUTER_RANGE = 0x465A; 
    public const int CHUNK_DL_MULTIPLIER  = 0x465B; 
    public const int CHUNK_CAMERA         = 0x4700; 
    public const int CHUNK_CAM_SEE_CONE   = 0x4710; 
    public const int CHUNK_CAM_RANGES     = 0x4720; 
       
	// -> Sub CHUNK_KEYF3DS
    public const int CHUNK_KF_HDR         = 0xB00A; 
    public const int CHUNK_AMBIENT_TAG    = 0xB001; 
    public const int CHUNK_OBJECT_TAG     = 0xB002; 
    public const int CHUNK_CAMERA_TAG     = 0xB003; 
    public const int CHUNK_TARGET_TAG     = 0xB004; 
    public const int CHUNK_LIGHTNODE_TAG  = 0xB005; 
    public const int CHUNK_KF_SEG         = 0xB008; 
    public const int CHUNK_KF_CURTIME     = 0xB009; 
    public const int CHUNK_KF_NODE_HDR    = 0xB010; 
    public const int CHUNK_PIVOTPOINT     = 0xB013; 
    public const int CHUNK_BOUNDBOX       = 0xB014; 
    public const int CHUNK_MORPH_SMOOTH   = 0xB015; 
    public const int CHUNK_POS_TRACK_TAG  = 0xB020; 
    public const int CHUNK_ROT_TRACK_TAG  = 0xB021; 
    public const int CHUNK_SCL_TRACK_TAG  = 0xB022; 
    public const int CHUNK_NODE_ID        = 0xB030; 

    
    public const int CHUNK_VIEWPORT_LAYOUT  = 0x7001; 
    public const int CHUNK_VIEWPORT_DATA    = 0x7011; 
    public const int CHUNK_VIEWPORT_DATA_3  = 0x7012; 
    public const int CHUNK_VIEWPORT_SIZE    = 0x7020; 

  
    public const int CHUNK_COL_RGB     = 0x0010; 
    public const int CHUNK_COL_TRU     = 0x0011; 
    public const int CHUNK_COL_LIN_24  = 0x0012; 
    public const int CHUNK_COL_LIN_F   = 0x0013; 

    public const int CHUNK_PERCENTAGE_I  = 0x0030;
    public const int CHUNK_PERCENTAGE_F  = 0x0031; 

    public const int CHUNK_CHUNK_MAX	   = 0xFFFF;
	
}

