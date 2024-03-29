using UnityEngine;
using System.Collections.Generic;

public class AssetLevelData : ScriptableObject
{
  

    [SerializeField]
    private List<LevelObject> levelObjects;

    public void Add(LevelObject levelObject)
    {
        if (levelObjects == null) levelObjects = new List<LevelObject>();
        levelObjects.Add(levelObject);
    }

    public List<LevelObject> GetAllObjects()
    {
        return levelObjects;
    }
}
