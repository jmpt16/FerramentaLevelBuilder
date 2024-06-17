using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelSetup : MonoBehaviour
{
	public Transform[] spawners;
	public GameObject player;
	// Start is called before the first frame update
	void Start()
    {
        LoadFromFile();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Spawner");
        spawners = new Transform[gameObjects.Length];
        for(int i = 0; i < gameObjects.Length; i++)
        {
            spawners[i] = gameObjects[i].transform;
        }
		Instantiate(player, spawners[Random.Range(0, spawners.Length)].transform.position, Quaternion.identity);
	}

	public void LoadFromFile()
    {
        string json = File.ReadAllText(Application.dataPath + "/levelData.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);
        //InstantiateModeManager.InstMode_IsDrag = false;
        for (int i = 0; i < data.listSize; i++)
        {
            GameObject objToSpawn = new GameObject();
            switch (data.types[i])
            {
                default:
                    objToSpawn.transform.position = data.positions[i];
                    objToSpawn.transform.rotation = data.rotations[i];
                    objToSpawn.transform.localScale = data.scales[i];
                    //objToSpawn.transform.tag = "Object";
                    objToSpawn.transform.gameObject.name = data.names[i];
                    objToSpawn.AddComponent<MeshFilter>();
                    objToSpawn.AddComponent<MeshRenderer>();
                    objToSpawn.AddComponent<MeshCollider>();

                    //objToSpawn.GetComponent<MeshCollider>().sharedMesh = data.meshes[i];
                    objToSpawn.GetComponent<MeshFilter>().sharedMesh = data.meshes[i];
                    objToSpawn.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
                    objToSpawn.GetComponent<MeshCollider>().sharedMesh = data.meshes[i];
                    objToSpawn.GetComponent<MeshCollider>().convex = true;
                    break;
                case 1:
                    objToSpawn.transform.position = data.positions[i];
                    objToSpawn.transform.rotation = data.rotations[i];
                    objToSpawn.transform.tag = "Spawner";
                    break;

            }
            

            //ObjManagerSetup(objToSpawn.GetComponent<ObjectManager>());



        }
    }
}
