using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _canvasGroup;
    [SerializeField] protected Button _button;

    public event UnityAction ButtonClick;

    protected virtual void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    protected virtual void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public virtual void Open()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _button.enabled = true;
    }

    public virtual void Close()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _button.enabled = false;
    }

    public void OnButtonClick()
    {
        ButtonClick?.Invoke();
    }
}
