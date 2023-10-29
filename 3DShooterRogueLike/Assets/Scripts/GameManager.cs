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
    private PlayerInfoSO _playerInfo;

    [SerializeField]
    private UIPanel _gameOverPanel;

    [SerializeField]
    private UIPanel _escapeMenu;

    [SerializeField]
    private TMP_Text _lastText;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private int _currentCountEnemies = 0;

    private bool _isEscapeShowed;

    private bool _isGameOverPanelShowed = false;

    [SerializeField]
    private EnemySpawner _enemySpawner;

    private void Start()
    {
        _player.GetComponent<HealthHandler>().OnDeath += PlayerLose;

        _enemySpawner.Initialize(this);

        _enemySpawner.SpawnWave();
    }

    private void Update()
    {

        if (!_isGameOverPanelShowed)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !_isEscapeShowed)
            {
                Debug.Log("Show");

                _escapeMenu.ShowGameOverPanel();
                _isEscapeShowed = true;

            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _isEscapeShowed)
            {

                _escapeMenu.ClosePanel();
                _isEscapeShowed = false;
            }
        }
    }


    public void CheckWinCondition()
    {
        //Debug.Log(_enemySpawner.GetEnenmyCount());

        if(_enemySpawner.GetEnenmyCount() == 0)
        {
            PlayerWin();
        }

    }


    private void PlayerLose()
    {
        _lastText.text = "You lose";

        _playerInfo.LoseCount++;

        _isGameOverPanelShowed = true;
        _gameOverPanel.ShowGameOverPanel();

    }

    private void PlayerWin()
    {
        _lastText.text = "Congratulations, you win";

        _playerInfo.WinCount++;


        _isGameOverPanelShowed = true;
        _gameOverPanel.ShowGameOverPanel();
    }

    public void BackToMainMenu()
    {
        PlayerData playerData = new PlayerData();

        playerData.LoseCount = _playerInfo.LoseCount;

        playerData.WinCount = _playerInfo.WinCount;

        SaveManager.Save(StaticFields.SAVE_DATA, playerData);

        DOTween.Clear();

        Time.timeScale = 1;

        SceneManager.LoadScene(StaticFields.MAIN_MENU_SCENE);
    }

    public GameObject GetPlayer()
    {
        return _player;
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
