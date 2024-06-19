using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveLoadLevel : MonoBehaviour
{

    public GameObject Child_MovementGizmo;
    public GameObject Child_RotationGizmo;
    public GameObject Child_ScaleGizmo;
    public string[] names;

    public void ChangeLevel()
    {
        SceneManager.LoadScene("PlayScene");
    }

	public void WriteToFile() {
        LevelData data = new LevelData();
        ObjectManager[] objectList = GameObject.FindObjectsOfType<ObjectManager>();
        List<GameObject> levelStuff = new List<GameObject>();
        foreach (var objectInScene in objectList)
        {
            Debug.Log(objectInScene);
            levelStuff.Add(objectInScene.transform.gameObject);
            //data.testString = objectInScene.transform.GetComponent<MeshFilter>().sharedMesh.ToString();
        }
        //Debug.Log(objectList.Length);
        GameObject[] allGMs = levelStuff.ToArray();

        data.listSize = allGMs.Length;
        data.types = new int[data.listSize];
        data.positions = new Vector3[data.listSize];
        data.scales = new Vector3[data.listSize];
        data.rotations = new Quaternion[data.listSize];
        data.meshes = new Mesh[data.listSize];
        data.names = new string[data.listSize];
        for (int i = 0; i < data.listSize; i++)
        {
            for (int c = 0; c < names.Length; c++)
            {
                if (allGMs[i].transform.gameObject.name.Contains(names[c]))
                {
                    data.types[i]=c+1;
                    c = names.Length + 1;
                }
            }
            data.positions[i] = allGMs[i].transform.position;
            data.scales[i] = allGMs[i].transform.localScale;
            data.rotations[i] = allGMs[i].transform.localRotation;
            data.meshes[i] = allGMs[i].transform.GetComponent<MeshFilter>().sharedMesh;
            data.names[i] = allGMs[i].transform.gameObject.name;
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath+"/levelData.json",json);
        Debug.Log(Application.dataPath + "/levelData.json");
    }

    public void LoadFromFile() {
        string json = File.ReadAllText(Application.dataPath + "/levelData.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);
        InstantiateModeManager.InstMode_IsDrag = false;
        for (int i = 0; i < data.listSize; i++)
        {
            GameObject objToSpawn = new GameObject();
            objToSpawn.transform.position = data.positions[i];
            objToSpawn.transform.rotation = data.rotations[i];
            objToSpawn.transform.localScale = data.scales[i];
            objToSpawn.transform.tag = "Object";
            objToSpawn.transform.gameObject.name = data.names[i];
            objToSpawn.AddComponent<MeshFilter>();
            objToSpawn.AddComponent<MeshRenderer>();
            objToSpawn.AddComponent<BoxCollider>();
            objToSpawn.AddComponent<ObjectManager>();

            //objToSpawn.GetComponent<MeshCollider>().sharedMesh = data.meshes[i];
            objToSpawn.GetComponent<MeshFilter>().sharedMesh = data.meshes[i];
            objToSpawn.GetComponent<MeshRenderer>().material= new Material(Shader.Find("Standard"));

            //ObjManagerSetup(objToSpawn.GetComponent<ObjectManager>());
        }
    }
}
