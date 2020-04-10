using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Barrel[] _barrels;
    [SerializeField] private GameUI _gameUI;

    #region Private
    [HideInInspector] public int currentLevel;
    [HideInInspector] public int nextLevel = -1;
    private GameData _gameData;

    private int _destroyedBarrels;
    #endregion

    private void Awake()
    {
        _gameData = GameData.Instance;
        FB.Init();

        currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel + 1 < _gameData._levels.Count) { nextLevel = currentLevel + 1; }

        

        for(int  i = 0; i < _barrels.Length; i++)
        {
            _barrels[i].Initialization(this, i);
            _gameUI.AddTarget();
        }
    }

    public void TargetDestroyed(int id)
    {
        if(_barrels[id].isDestroyed == false)
        {
            _gameUI.TargetDestroyed(id);
            _destroyedBarrels += 1;

            if (isAllBarrelsDestroyed())
            {
                EventHandler.OnGameWinEvent();
            }
        }
    }

    public void LoadLevel(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void LoadNextLevel()
    {
        FB.LogAppEvent("Level completed " + currentLevel.ToString());

        if (nextLevel != -1)
        {
            LoadLevel(currentLevel + 1);
        }
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }

    public bool isAllBarrelsDestroyed()
    {
        if (_destroyedBarrels >= (_barrels.Length))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}