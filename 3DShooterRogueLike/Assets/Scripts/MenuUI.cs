using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

    [SerializeField]
    private string _saveString;

    [SerializeField]
    private RectTransform _insructions;

    private PlayerData _playerData = new PlayerData();

    private void Start()
    {
        _playerData = SaveManager.Load<PlayerData>(SaveManager.SAVE_DATA);

        _playerInfo.LoseCount = _playerData.LoseCount;

        _playerInfo.WinCount = _playerData.WinCount;

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

    public void ShowInstructions()
    {
        _insructions.DOScale(1, 1);
    }

    public void CloseInstructions()
    {
        _insructions.DOScale(0, 1);
    }

    public void ResetData()
    {
        _playerInfo.LoseCount = 0;

        _playerInfo.WinCount = 0;
    }

    private void OnApplicationQuit()
    {
        _playerData.LoseCount = _playerInfo.LoseCount;

        _playerData.WinCount = _playerInfo.WinCount;

        SaveManager.Save(SaveManager.SAVE_DATA, _playerData);
    }

    


}
