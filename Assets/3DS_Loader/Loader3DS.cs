using UnityEngine;
using System.Collections;
using System.IO;

public class Loader3DS : MonoBehaviour {

	public string modelPath;
	public string texturePath = "";

	private string nameModel = "";
	private Vector3 [] verticesModel;
	private int [] trianglesModel;
	private Vector2[] uvsModel;

    void Awake()
    {
        modelPath = Application.dataPath + "/Resources/Models/Pine3DS.3ds";
    }

	void OnGUI ()
	{
		if (GUI.Button(new Rect (0,0,100,50),"Load"))
			StartCoroutine(Loader(modelPath));
	}

    public static GameObject Load3DS()
    {
        return null;
    }

	IEnumerator Loader (string path)
	{
		
		if (!File.Exists (path)) 
		{
			Debug.LogError("File does not exist.");
			yield break;
		}
		
		ushort chunk_id;
		uint chunk_lenght;
		char charReader;
		ushort qty;
		ushort face_flags;
		int i;


		using (BinaryReader myFileStream = new BinaryReader (File.OpenRead (path))) 
		{
			

			while (myFileStream.BaseStream.Position < myFileStream.BaseStream.Length) 
			{
				chunk_id = myFileStream.ReadUInt16 ();
				chunk_lenght = myFileStream.ReadUInt32 ();

				switch (chunk_id) 
				{
					case 0x4d4d:
						Debug.Log("Main chunk");
						break;

					case 0x3d3d:
						Debug.Log("3D editor chunck");
						break;

					case 0x4000:
						nameModel = "";
						i = 0;
						do
						{
							charReader = myFileStream.ReadChar();
							nameModel += charReader.ToString();
						}
						while (charReader != '\0' && i<20);
						Debug.Log("Model name: " + nameModel);
						break;

					case 0x4100:
						break;

					case 0x4110:
						qty = myFileStream.ReadUInt16();
						Debug.Log("Vertices: "+ qty);
						verticesModel = new Vector3[qty];
						for (i=0; i<qty; i++)
						{
							verticesModel[i].x = myFileStream.ReadSingle();
							verticesModel[i].y = myFileStream.ReadSingle();
							verticesModel[i].z = myFileStream.ReadSingle();
						}
						break;

					case 0x4120:
						qty = myFileStream.ReadUInt16();
						Debug.Log("Faces: " + qty);
						trianglesModel = new int[qty * 3 ];

						for (i=0; i<qty*3; i++)
						{
							trianglesModel[i] = myFileStream.ReadUInt16();
							i++;
							trianglesModel[i] = myFileStream.ReadUInt16();
							i++;
							trianglesModel[i] = myFileStream.ReadUInt16();
							face_flags = myFileStream.ReadUInt16();
						}
						break;

					case 0x4140:
						qty = myFileStream.ReadUInt16();
						Debug.Log("Uvs: "+ qty);
						uvsModel = new Vector2[qty];
						for (i=0; i<qty; i++)
						{
							uvsModel[i].x = myFileStream.ReadSingle();
							uvsModel[i].y = myFileStream.ReadSingle();
						}
						break;
					//Local coordinate system
//					case 0x4160:
//						Debug.Log("X1: "+ myFileStream.ReadSingle());
//						Debug.Log("X1: "+ myFileStream.ReadSingle());
//						Debug.Log("X1: "+ myFileStream.ReadSingle());
//						Debug.Log("X2: "+ myFileStream.ReadSingle());
//						Debug.Log("X2: "+ myFileStream.ReadSingle());
//						Debug.Log("X2: "+ myFileStream.ReadSingle());
//						Debug.Log("X3: "+ myFileStream.ReadSingle());
//						Debug.Log("X3: "+ myFileStream.ReadSingle());
//						Debug.Log("X3: "+ myFileStream.ReadSingle());
//						Debug.Log("O: "+ myFileStream.ReadSingle());
//						Debug.Log("O: "+ myFileStream.ReadSingle());
//						Debug.Log("O: "+ myFileStream.ReadSingle());
//						break;

					default:
						myFileStream.BaseStream.Seek(chunk_lenght-6, SeekOrigin.Current);
						break;
				}

			}

			myFileStream.Close ();
		}

		yield return StartCoroutine (SetMesh());

		yield return new WaitForEndOfFrame();
	}

	IEnumerator SetMesh ()
	{
		WWW wwwTexture = new WWW (texturePath);
		yield return wwwTexture;

		GameObject gameObjectMesh = new GameObject (nameModel);
		gameObjectMesh.transform.parent = gameObject.transform;

		gameObjectMesh.AddComponent<MeshFilter>();
		gameObjectMesh.AddComponent<MeshRenderer>();

		MeshFilter meshFilter = gameObjectMesh.GetComponent<MeshFilter>();
		meshFilter.mesh.Clear();
		meshFilter.mesh.vertices = verticesModel;
		meshFilter.mesh.uv = uvsModel;
		meshFilter.mesh.triangles = trianglesModel;
		meshFilter.mesh.RecalculateBounds();

		Material modelMaterial = new Material (Shader.Find("Diffuse"));

		//wwwTexture.LoadImageIntoTexture ((Texture2D) modelMaterial.mainTexture);
		modelMaterial.mainTexture = wwwTexture.texture;
		MeshRenderer meshRenderer = gameObjectMesh.GetComponent<MeshRenderer>();
		meshRenderer.material = modelMaterial;

		yield return new WaitForEndOfFrame();
	}

}
