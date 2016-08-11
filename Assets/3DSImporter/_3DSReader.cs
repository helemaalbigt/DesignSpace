using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class _3DSReader
{
	static ArrayList MaterialArray = new ArrayList();

	public static string	TexturePath = Application.dataPath + "/3DSImporter/";
	public static bool		Reorientate = true;
	public static string 	ReadString( BinaryReader fptr )
	{
		//return fptr.ReadString ();
		byte c = 255;
		string retStr = "";
		while (c != 0)
		{
			c = fptr.ReadByte();
			if (c != 0)
			   retStr = retStr + ((char)c).ToString();
		}
		
		return retStr;
	}

	public static void ReadIndices(Mesh pMesh, BinaryReader fptr, UInt32 headerlen )
	{
		UInt16 f1,f2,f3,flags;
		UInt16 faceCount = fptr.ReadUInt16();
		int [] faces = new int[faceCount * 3]; 
		int findex = 0;
		for ( int i = 0; i < faceCount; ++i)
		{
			f1 = fptr.ReadUInt16();	f2 = fptr.ReadUInt16(); f3 = fptr.ReadUInt16();
			flags = fptr.ReadUInt16();
			faces[findex] = f3; findex++;
			faces[findex] = f2; findex++;
			faces[findex] = f1; findex++;
		 
		}
		pMesh.triangles = faces;// ( faces, MeshTopology.Triangles, 0 );
	}
	public static void ReadUVS(Mesh pMesh, BinaryReader fptr, UInt32 headerlen )
	{
		float u,v;
		UInt16 uvCount = fptr.ReadUInt16();
		Vector2 [] uvs = new Vector2[uvCount]; 
		
		for ( int i = 0; i < uvCount; ++i)
		{
			u = fptr.ReadSingle();	v = fptr.ReadSingle();
			uvs[i] = new Vector2(u,v);
		}
		pMesh.uv = uvs;
	}

	public static void ReadVertices(Mesh pMesh, BinaryReader fptr, UInt32 headerlen)
	{
		float x,y,z;
		UInt16 vertCount = fptr.ReadUInt16();
		Vector3 [] vertices = new Vector3[vertCount]; 

		for ( int i = 0; i < vertCount; ++i)
		{
			x = fptr.ReadSingle();	y = fptr.ReadSingle(); z = fptr.ReadSingle();
			if ( Reorientate )
				vertices[i] = new Vector3(x,z,y);
			else 
				vertices[i] = new Vector3(x,y,z);
		}

		pMesh.vertices = vertices;
	}

	public static void ReadMaterial(  BinaryReader fptr )
	{
		SMaterialEntry material = new SMaterialEntry();
	 
		while ( fptr.BaseStream.Position < fptr.BaseStream.Length )
		{
			long pos = fptr.BaseStream.Position;
			UInt16 headerid;
			UInt32 headerlen;
			headerid = fptr.ReadUInt16();
			headerlen = fptr.ReadUInt32();
			
			if( headerlen < 0 )
				break;

			switch ( headerid )
			{	

				case _3DSFile.CHUNK_MATNAME:
					material.MaterialName = ReadString (fptr);
					// Look at material array (from 3ds import )
					// not exist Create a new material named //
					// set as active material for population
				break;

				case _3DSFile.CHUNK_MATMAPFILE:
					material.DiffuseName = ReadString ( fptr );
					// populate current material with this texture name
				break;

				case _3DSFile.CHUNK_MATTEXMAP:
				case _3DSFile.CHUNK_MATSPECMAP:
				case _3DSFile.CHUNK_MATOPACMAP:
				case _3DSFile.CHUNK_MATREFLMAP:
				case _3DSFile.CHUNK_MATBUMPMAP:
					// Something wrong with these //
					break;
				default:
					//if ( fptr.BaseStream.Position < fptr.BaseStream.Length )
					//	fptr.BaseStream.Position +=  headerlen-6;
					if ( fptr.BaseStream.Position < fptr.BaseStream.Length )
						fptr.BaseStream.Position = pos + headerlen ;//- 6;
				break;
				//case _3DSFile.CHUNK_MATDIFFUSE:
			}
		}
		MaterialArray.Add ( material );
	}

	public static SMaterialEntry GetMaterial( string sMaterialName )
	{
		for ( int i = 0; i < MaterialArray.Count; ++i) // maybe use cmap in future
		{
			if ( ((SMaterialEntry) MaterialArray[i]).MaterialName == sMaterialName )
				return (SMaterialEntry) MaterialArray[i];
		}
		return null;
	}

	public static void ReadMain( BinaryReader fptr , GameObject parent )
	{
		GameObject go = new GameObject();
		Mesh pmesh = null;
		MeshRenderer mr = null;
		string geomName;

		SMaterialEntry matEntry;

		while ( fptr.BaseStream.Position < fptr.BaseStream.Length )
		{
			long pos = fptr.BaseStream.Position;
			UInt16 headerid;
			UInt32 headerlen;
			headerid = fptr.ReadUInt16();
			headerlen = fptr.ReadUInt32();

			if( headerlen < 0 )
				break;

			switch ( headerid )
			{	
				//----------------- MAIN3DS -----------------
				case _3DSFile.CHUNK_MAIN3DS: 
					break;    
					
				//----------------- EDIT3DS -----------------
				case _3DSFile.CHUNK_EDIT3DS:
					break;
					
				//--------------- EDIT_OBJECT ---------------
				case _3DSFile.CHUNK_EDIT_OBJECT: 
					geomName = ReadString(fptr);
					go = new GameObject();
					go.name = geomName;	
					go.transform.parent = parent.transform;
					
					pmesh = new Mesh();
					pmesh.name = geomName + "_mesh";
					mr = go.AddComponent<MeshRenderer>();
					MeshFilter mf = go.AddComponent <MeshFilter>();
					mf.sharedMesh = pmesh;

					// set parentage //
					break;
					
				//--------------- OBJ_TRIMESH ---------------
				case _3DSFile.CHUNK_OBJTRIMESH:
					break;
					
				//--------------- TRI_VERTEXL ---------------
				case _3DSFile.CHUNK_TRIVERT: 
					ReadVertices(pmesh, fptr, headerlen);
					break;
					
				case _3DSFile.CHUNK_TRIUV:
					ReadUVS(pmesh,fptr, headerlen);
					break;
					
					
				//--------------- TRI_FACEL1 ----------------
				case _3DSFile.CHUNK_TRIFACE:
					ReadIndices(pmesh, fptr, headerlen);
					pmesh.RecalculateNormals();
					pmesh.RecalculateBounds();
					break;
			case _3DSFile.CHUNK_TRIFACEMAT:
					matEntry = GetMaterial( ReadString ( fptr ) );
					if ( matEntry != null )
					{
						Shader shader1 = Shader.Find("Diffuse");
						mr.material = new Material(shader1);
						WWW www = new WWW("file://" + TexturePath  + "/" + matEntry.DiffuseName );
					//	while (!www.isDone );
						Texture2D txt = www.texture;//R//esources.LoadAssetAtPath<Texture2D>( "assets/" +  );
						mr.material.SetTexture ("_MainTex", txt);
						fptr.BaseStream.Position = pos + headerlen ;//- 6; 
					}
					break;

				//----------------- MESHVERSION -----------------
				case _3DSFile.CHUNK_MESHVERSION:
				case 0x01:
					UInt32 version = fptr.ReadUInt32();
					// do something nice with this information //
					break;

				//----------------- Material -----------------
				case _3DSFile.CHUNK_EDIT_MATERIAL:
					ReadMaterial ( fptr );
					
					fptr.BaseStream.Position = pos + headerlen ;//- 6;
					break;

				default:
					if ( fptr.BaseStream.Position < fptr.BaseStream.Length )
						fptr.BaseStream.Position = pos + headerlen ;//- 6;
					break;
						 
				
				
			}
		

		}
 
	}

	public static GameObject Import3DS( string sFilename )
	{
		BinaryReader stream = new BinaryReader( File.OpenRead(sFilename) );
		
		GameObject goRt = new GameObject();
		goRt.name = sFilename;
		
		ReadMain( stream, goRt );
		
		return goRt;
	}
}

public class SMaterialEntry
{
	public string MaterialName;
	public string DiffuseName;
}
