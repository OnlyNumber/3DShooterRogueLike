using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _winTXT;

    [SerializeField]
    private TMP_Text _loseTXT;

    [SerializeField]
    private string _gameScene;

    [SerializeField]
    private PlayerInfoSO _playerInfo;

    private void Start()
    {
        _winTXT.text = "Win: " + _playerInfo.WinCount;

        _loseTXT.text = "Lose: " + _playerInfo.LoseCount;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
