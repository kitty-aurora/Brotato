using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private string sceneToLoad; // 开始按钮要跳转的场景名称（需在Build Settings中添加）
        private Image _buttonImage;                 // 按钮背景图片组件
        private TextMeshProUGUI _buttonText;        // 按钮文字组件（TMP）
        private Vector3 _originalScale;             // 初始缩放大小
        private Color _originalImageColor;          // 初始背景颜色
        private Color _originalTextColor;           // 初始文字颜色

        void Start()
        {
            // 初始化组件引用
            _buttonImage = GetComponent<Image>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
            
            // 记录初始状态
            _originalScale = transform.localScale;
            _originalImageColor = _buttonImage.color;
            _originalTextColor = _buttonText.color;
        }

        // 鼠标移入时触发
        public void OnPointerEnter(PointerEventData eventData)
        {
            _buttonImage.color = Color.white;      // 背景变白
            _buttonText.color = Color.black;      // 文字变黑
            transform.localScale = _originalScale * 1.1f; // 放大1.1倍
        }

        // 鼠标移出时触发
        public void OnPointerExit(PointerEventData eventData)
        {
            _buttonImage.color = _originalImageColor; // 恢复原背景色
            _buttonText.color = _originalTextColor;   // 恢复原文字色
            transform.localScale = _originalScale;   // 恢复原缩放
        }

        // 鼠标点击时触发
        public void OnPointerClick(PointerEventData eventData)
        {
            // 仅“开始”按钮执行场景跳转（需在Inspector中设置sceneToLoad）
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}