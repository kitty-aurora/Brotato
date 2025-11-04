using System;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace UI.Difficulty
{
    public class DifficultyUISelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private DifficultyData _difficultyData;

        private Image _backImage;
        private Image _avatar;

        // 场景中详情面板的控件
        private Image _difficultyDetailAvatar;
        private TextMeshProUGUI _difficultyDetailName;
        private TextMeshProUGUI _difficultyDetailDescribe;

        private Color _color;

        private void Awake()
        {
            _backImage = GetComponent<Image>();
            _color = _backImage.color;
            _avatar = transform.Find("Avatar")?.GetComponent<Image>();

            // 从单例管理器获取详情面板控件引用
            if (DifficultyUIManager.Instance)
            {
                _difficultyDetailAvatar = DifficultyUIManager.Instance.difficultyDetailAvatar;
                _difficultyDetailName = DifficultyUIManager.Instance.difficultyDetailName;
                _difficultyDetailDescribe = DifficultyUIManager.Instance.difficultyDetailDescribe;
            }
            else
            {
                Debug.LogError("场景中未找到 DifficultyUIManager！请确保已添加并配置。");
            }
        }

        // Start is called before the first frame update


        public void OnPointerEnter(PointerEventData eventData)
        {
            _backImage.color = Color.black;
            _backImage.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
            _avatar.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
            RenewUI(_difficultyData);
        }

        private void RenewUI(DifficultyData data)
        {
            if (data == null) return;
            // 鼠标悬停时更新详情面板（复用 SetDifficultyData 的逻辑，或单独实现）
            _difficultyDetailAvatar.sprite = _avatar.sprite;
            _difficultyDetailName.text = data.name;
            _difficultyDetailDescribe.text = data.describe;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _backImage.color = _color;
            _backImage.rectTransform.localScale = Vector3.one;
            _avatar.rectTransform.localScale = Vector3.one;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // 开始游戏
            Debug.Log("todo: 开始游戏");
        }

        public void SetDifficultyData(DifficultyData difficultyData)
        {
            if (difficultyData == null) return;
            _difficultyData = difficultyData;
            _avatar.sprite = Resources.Load<SpriteAtlas>("Image/UI/危险等级").GetSprite(_difficultyData.name);
         
        }
    }
}