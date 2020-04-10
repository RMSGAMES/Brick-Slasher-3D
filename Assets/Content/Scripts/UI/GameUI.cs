using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private TextMeshProUGUI _nextLevel;

    [Header("Targets List")]
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private Transform _targetList;

    [Header("Other")]
    [SerializeField] private GameController _gameController;
    [SerializeField] private UIPanelsController _panelsController;

    [SerializeField] private List<GameObject> _targetListCash = new List<GameObject>();

    #region Private
    private GameData _gameData;
    #endregion

    private void Start()
    {
        _gameData = GameData.Instance;
        _currentLevel.text = _gameData._levels[_gameController.currentLevel].showName;
        _nextLevel.text = _gameData._levels[_gameController.nextLevel].showName;
    }

    protected void OnEnable()
    {
        EventHandler.onGameWin += GameWinUI;
        EventHandler.onGameLose += GameLoseUI;
    }

    protected void OnDisable()
    {
        EventHandler.onGameWin -= GameWinUI;
        EventHandler.onGameLose -= GameLoseUI;
    }

    public void GameWinUI()
    {
        //_audioManager.PlayWonGame();
        _panelsController.ChangeWindow(1);
    }

    public void GameLoseUI()
    {
        //_audioManager.PlayLostGame();
        _panelsController.ChangeWindow(2);
    }

    public void LoadLevel(int id)
    {
        _gameController.LoadLevel(id);
    }

    public void NextLevel()
    {
        _gameController.LoadNextLevel();
    }

    public void RestartLevel()
    {
        _gameController.RestartLevel();
    }

    //Update UI info
    public void AddTarget()
    {
        GameObject _gameObject = Instantiate(_targetPrefab, _targetList);
        _targetListCash.Add(_gameObject);
    }

    public void TargetDestroyed(int id)
    {
        _targetListCash[id].GetComponent<CanvasGroup>().alpha = 0.55f;
    }
}