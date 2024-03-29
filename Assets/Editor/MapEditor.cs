using UnityEngine;
using System.Collections;
using UnityEditor;

public class MapEditor : Editor
{
    public static string GAME_OBJECTS = "MapObjects";

    static GameObject gameObjects;


    [MenuItem("Level Editor/Create Level")]
    public static void CreateLevel()
    {
        gameObjects = GameObject.Find(GAME_OBJECTS);
  
        if (gameObjects == null)
        {
            EditorUtility.DisplayDialog("Error", GAME_OBJECTS + " not found!", "OK");
            return;
        }
    
        string path = EditorUtility.SaveFilePanel("Save Level", EditorPrefs.GetString("level_path", ""), EditorPrefs.GetString("level_name", ""), "asset");
        path = path.Substring(path.IndexOf("Asset"));
        string fileName = path.Substring(path.LastIndexOf("/") + 1);
        EditorPrefs.SetString("level_name", fileName);
        EditorPrefs.SetString("level_path", path.Substring(0, path.Length - fileName.Length));

        AssetLevelData objectList = CreateInstance<AssetLevelData>();
        
        for (int i = 0; i < gameObjects.transform.childCount; i++)
        {
            Transform objectTransform = gameObjects.transform.GetChild(i);
            Object prefab = PrefabUtility.GetCorrespondingObjectFromSource(objectTransform.gameObject);
            string prefabPath = AssetDatabase.GetAssetPath(prefab);
            Debug.Log(prefabPath);
            LevelObject levelObject = LevelObject.Create(AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath), objectTransform);
            objectList.Add(levelObject);
        }
        AssetLevelData existingAsset = AssetDatabase.LoadAssetAtPath<AssetLevelData>(path);
        if (existingAsset == null)
            AssetDatabase.CreateAsset(objectList, path);
        else
            EditorUtility.CopySerialized(objectList, existingAsset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Level Editor/Load Level")]
    public static void LoadLevel()
    {
        string path = EditorUtility.OpenFilePanel("Open map", EditorPrefs.GetString("level_path", ""), "asset");
        path = path.Substring(path.IndexOf("Asset"));
        if (path.Length > 0)
        {
            AssetLevelData data = AssetDatabase.LoadAssetAtPath<AssetLevelData>(path);

            Clear();
            foreach(LevelObject obj in data.GetAllObjects())
            {
                GameObject go = PrefabUtility.InstantiatePrefab(obj.prefab) as GameObject;
                go.transform.SetParent(gameObjects.transform);
                go.transform.position = obj.position;
                go.transform.eulerAngles = obj.rotation;
				if (go.gameObject.tag!="enemy")
                go.transform.localScale = obj.scale;
            }
        }
        Debug.Log(path);
    }

    public static void Clear()
    {
        gameObjects = GameObject.Find(GAME_OBJECTS);
      
        if (gameObjects != null) GameObject.DestroyImmediate(gameObjects);
        gameObjects = new GameObject(GAME_OBJECTS);
        //if (background != null) GameObject.DestroyImmediate(background);
        //background = new GameObject(BACKGROUND);
    }
}
