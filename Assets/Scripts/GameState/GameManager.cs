using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance { get; private set; }
    private bool isGameStopped, isPlayerReachedFinishLine;
    
    public const string FINISH_LINE_TAG = "Finish";
    private WaitForSeconds gameStopDelay = new WaitForSeconds(2);

    // For debugging purposes switch saving on/off
    public bool dontSave;
    
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 0;
    }

    public void SetNextLevel()
    {
        LevelManager.IncreaseLevelIndex();
        // Add new gun for new level
        Player.instance.IncreaseUnlockedGunCount();
        isPlayerReachedFinishLine = true;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        UIManager.instance.CloseMainMenuUI();
    }

    public void StopGame()
    {
        UIManager.instance.CloseInGameUI();
        StartCoroutine(StopGameWithDelay());
    }

    public void ContinueGame()
    {
        GameData.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public bool IsGameWaitingToStart()
    {
        return Time.timeScale == 0 && !isGameStopped;
    }

    private IEnumerator StopGameWithDelay()
    {
        yield return gameStopDelay;
        UIManager.instance.ShowGameOverUI(isPlayerReachedFinishLine);
        Time.timeScale = 0;
    }

    private void OnApplicationQuit()
    {
        GameData.Save();
    }

}