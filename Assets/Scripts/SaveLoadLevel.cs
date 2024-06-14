using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadLevel : MonoBehaviour
{

    public GameObject Child_MovementGizmo;
    public GameObject Child_RotationGizmo;
    public GameObject Child_ScaleGizmo;

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
        data.positions = new Vector3[data.listSize];
        data.rotations = new Quaternion[data.listSize];
        data.meshes = new Mesh[data.listSize];
        data.names = new string[data.listSize];
        for (int i = 0; i < data.listSize; i++)
        {
            data.positions[i] = allGMs[i].transform.position;
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
    public void ObjManagerSetup(ObjectManager objManager) 
    {
        objManager.renderer = objManager.transform.GetComponent<MeshRenderer>();
        objManager.screenPoint=Camera.main.ScreenToWorldPoint(objManager.transform.position);
        objManager.offset = Vector3.zero;
        objManager.currentPosition = objManager.transform.position;

        objManager.Child_MovementGizmo=Instantiate(Child_MovementGizmo,objManager.transform);
        objManager.GMoveX = objManager.Child_MovementGizmo.transform.Find("X_Collider").gameObject;
        objManager.GMoveY = objManager.Child_MovementGizmo.transform.Find("Y_Collider").gameObject;
        objManager.GMoveZ = objManager.Child_MovementGizmo.transform.Find("Z_Collider").gameObject;

        objManager.Child_RotationGizmo = Instantiate(Child_RotationGizmo, objManager.transform);
        objManager.GRotateX = objManager.Child_RotationGizmo.transform.Find("GizmoXRotation").gameObject;
        objManager.GRotateY = objManager.Child_RotationGizmo.transform.Find("GizmoYRotation").gameObject;
        objManager.GRotateZ = objManager.Child_RotationGizmo.transform.Find("GizmoZRotation").gameObject;

        objManager.Child_ScaleGizmo = Instantiate(Child_ScaleGizmo, objManager.transform);
        objManager.GScaleCenter = objManager.Child_ScaleGizmo.transform.Find("ScaleGizmoCenter").gameObject;
        objManager.GScaleX = objManager.Child_ScaleGizmo.transform.Find("ScaleGizmoX").gameObject;
        objManager.GScaleY = objManager.Child_ScaleGizmo.transform.Find("ScaleGizmoY").gameObject;
        objManager.GScaleZ = objManager.Child_ScaleGizmo.transform.Find("ScaleGizmoZ").gameObject;

        InstantiateModeManager.InstMode_IsDrag = false;
        objManager.SetState(new IdleObjectState());
    }

}
