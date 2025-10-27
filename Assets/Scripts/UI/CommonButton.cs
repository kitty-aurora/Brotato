using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommonButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image _image;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = Color.white;
        _text.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = Color.black;
        _text.color = Color.white;
    }
}
