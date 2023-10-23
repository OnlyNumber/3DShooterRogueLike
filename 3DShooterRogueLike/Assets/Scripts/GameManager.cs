using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyAI> _enemyPrefab;

    [SerializeField]
    private List<Transform> _spawnPositions;

    [SerializeField]
    private List<EnemyAI> _enemyPool = new List<EnemyAI>();

    [SerializeField]
    private PlayerInfoSO _playerInfo;

    [SerializeField]
    private Image _gameOverPanel;

    [SerializeField]
    private Image _escapeMenu;

    [SerializeField]
    private float _timeToShow;

    [SerializeField]
    private TMP_Text _lastText;

    [SerializeField]
    private string _mainMenuScene;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private int _currentCountEnemies = 0;

    private bool _isEscapeShowed;

    private bool _isGameOverPanelShowed = false;

    private void Start()
    {
        _player.GetComponent<HealthHandler>().OnDeath += PlayerLose;

        SpawnWave();


    }

    private void Update()
    {

        if (!_isGameOverPanelShowed)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !_isEscapeShowed)
            {
                Debug.Log("Show");

                ShowGameOverPanel(_escapeMenu);
                _isEscapeShowed = true;

            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _isEscapeShowed)
            {
                
                ClosePanel(_escapeMenu);
                _isEscapeShowed = false;
            }
        }
    }


    private void SpawnWave()
    {
        foreach (var position in _spawnPositions)
        {
            _enemyPool.Add(Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Count)], position.position, Quaternion.identity));

            _enemyPool[_enemyPool.Count - 1].Initialize(this);

            //_currentCountEnemies++;
        }
    }


    public void CheckWinCondition()
    {
        Debug.Log(_enemyPool.Count);

        if(_enemyPool.Count == 0)
        {
            PlayerWin();
        }

    }

    public void ShowGameOverPanel(Image showPanel)
    {
        DOTween.Clear();

        DOTween.Sequence()
                .Append(showPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -Screen.height / 2), _timeToShow))
                .AppendInterval(_timeToShow)
                .AppendCallback(() => PauseTween(0));

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    public void ClosePanel(Image closePanel)
    {
        DOTween.Clear();

        PauseTween(1);

        DOTween.Sequence()
                .Append(closePanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, Screen.height), _timeToShow));

        Debug.Log("Close");

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }


    private void PlayerLose()
    {
        _lastText.text = "You lose";

        _playerInfo.LoseCount++;

        _isGameOverPanelShowed = true;
        ShowGameOverPanel(_gameOverPanel);

    }

    private void PlayerWin()
    {
        _lastText.text = "Congratulations, you win";

        _playerInfo.WinCount++;


        _isGameOverPanelShowed = true;
        ShowGameOverPanel(_gameOverPanel);
    }

    public void BackToMainMenu()
    {
        PlayerData playerData = new PlayerData();

        playerData.LoseCount = _playerInfo.LoseCount;

        playerData.WinCount = _playerInfo.WinCount;

        SaveManager.Save(SaveManager.SAVE_DATA, playerData);

        DOTween.Clear();

        Time.timeScale = 1;

        SceneManager.LoadScene(_mainMenuScene);
    }

    public void Recicle(EnemyAI enemy)
    {
        _enemyPool.Remove(enemy);

        Destroy(enemy.gameObject);

    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    private void PauseTween(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!_gameOverPanel || _escapeMenu)
        {
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;
        }
    }


}
