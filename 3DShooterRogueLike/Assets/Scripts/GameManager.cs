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
    private float _timeToShow;

    [SerializeField]
    private TMP_Text _lastText;

    [SerializeField]
    private string _mainMenuScene;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private int _currentCountEnemies = 0;

    private void Start()
    {
        _player.GetComponent<HealthHandler>().OnDeath += PlayerLose;

        SpawnWave();


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

    private void ShowGameOverPanel()
    {
        DOTween.Sequence()
                .Append(_gameOverPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, _timeToShow));

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    private void PlayerLose()
    {
        _lastText.text = "You lose";

        _playerInfo.LoseCount++;

        ShowGameOverPanel();

    }

    private void PlayerWin()
    {
        _lastText.text = "Congratulations, you win";

        _playerInfo.WinCount++;

        ShowGameOverPanel();
    }

    public void BackToMainMenu()
    {
        PlayerData playerData = new PlayerData();

        playerData.LoseCount = _playerInfo.LoseCount;

        playerData.WinCount = _playerInfo.WinCount;

        SaveManager.Save(SaveManager.SAVE_DATA, playerData);

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

}
