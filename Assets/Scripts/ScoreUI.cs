using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Image _uiFillImage;

    [SerializeField] private TMP_Text _textUI;

    private void Start()
    {
        _uiFillImage.fillAmount = 0;
        _textUI.text = 0.ToString();
    }

    public void UpdateUI(int value)
    {
        _textUI.text = value.ToString();

        DOVirtual.Float(_uiFillImage.fillAmount, value / 100f, .5f, (x) =>
        {
            _uiFillImage.fillAmount = x;
        });

    }
}
