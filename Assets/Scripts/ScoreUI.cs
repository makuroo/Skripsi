using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Image _uiFillImage;

    [SerializeField] private TMP_Text _textUI;

    [SerializeField] private int _initialValue;

    private void Start()
    {
        _uiFillImage.fillAmount = _initialValue/100f;
    }

    public void UpdateUI(int value)
    {
        DOVirtual.Float(_uiFillImage.fillAmount, value / 100f, .5f, (x) =>
        {
            _uiFillImage.fillAmount = x;
        });

    }
}
