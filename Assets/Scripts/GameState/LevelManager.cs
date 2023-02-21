using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Prefabs should be named as: LEVEL_{levelIndex}
    private static GameObject[] levelPrefabs;
    private static int levelIndex;

    private const string LEVEL_PREFABS_PATH = "Prefabs/Levels";
    private static readonly Vector3 INSTANTIATE_POS = new Vector3(0, 0.02f, 0);

    void Awake()
    {
        levelPrefabs = Resources.LoadAll<GameObject>(LEVEL_PREFABS_PATH);
        
        GameData.LoadLevelData(ref levelIndex);

        UIManager.instance.UpdateLevelText();
    }

    void Start()
    {
        if (levelIndex < levelPrefabs.Length)
        {
            Instantiate(levelPrefabs[levelIndex], INSTANTIATE_POS, Quaternion.identity);
        }
    }

    public static void IncreaseLevelIndex()
    {
        if (levelIndex < levelPrefabs.Length - 1)
        {
            levelIndex++;
        }
    }

    public static int GetLevelIndex()
    {
        return levelIndex;
    }
}