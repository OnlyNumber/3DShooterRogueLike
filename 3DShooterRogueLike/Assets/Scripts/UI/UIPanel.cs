using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private float _timeToShow;

    public void ShowGameOverPanel()
    {
        DOTween.Clear();

        DOTween.Sequence()
                .Append(_rectTransform.DOAnchorPos(Vector2.zero, _timeToShow))
                .AppendInterval(_timeToShow)
                .AppendCallback(() => TimeLock(0));

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    public void ClosePanel()
    {
        DOTween.Clear();

        TimeLock(1);

        DOTween.Sequence()
                .Append(_rectTransform.DOAnchorPos(new Vector2(0, _rectTransform.rect.height), _timeToShow));

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }


    private void TimeLock(float timeScale)
    {

        Time.timeScale = timeScale;
        
        Debug.Log("Time: " + Time.timeScale);
    }
}
