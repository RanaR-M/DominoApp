
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ToggleSwitch : MonoBehaviour , IPointerDownHandler
{
    [SerializeField]
    private bool _isOn = false;
    public bool isOn
    {
        get
        {
            return _isOn;
        }
    }
    [SerializeField]
    private RectTransForm toggleIndicator;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Color onColor;
    [SerializeField]
    private Color offColor;
    private float offX;
    private float OnX;
    [SerializeField]
    private float tweenTime = 0.25f;

    public delegate void ValueChanged(bool value);
    public event ValueChanged valueChanged;
    // Start is called before the first frame update
    void Start()
    {
        offX = toggleIndicator.anchoredPosition.x;
        OnX = backgroundImage.rectTransform.rect.width - toggleIndicator.rect.width;
    }
    private void OnEnable()
    {
        Toggle(isOn);
    }
    private void Toggle (bool value)
    {
        if (value != isOn)
        {
            _isOn = value;
            ToggleColor(isOn);
            MoveIndicator(isOn);
            if (valueChanged != null)
                valueChanged(isOn);
        }
    }
    private void ToggleColor(bool value)
    {
        if (value)
            backgroundImage.DOColor(onColor, tweenTime);
        else
            backgroundImage.DOColor(offColor, tweenTime);
    }
    private void MoveIndicator(bool value)
    {
        if (value)
            toggleIndicator.DOAnchorPosX(OnX, tweenTime);
        else
            toggleIndicator.DOAnchorPosX(offX, tweenTime);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn);
    }
}
