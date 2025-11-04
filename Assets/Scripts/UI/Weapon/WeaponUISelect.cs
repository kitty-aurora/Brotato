using System;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Weapon
{
    public class WeaponUISelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private WeaponData _weaponData;

        private Image _backImage;
        private Image _avatar;
        
       // 场景中详情面板的控件
        private Image _weaponDetailAvatar;
        private TextMeshProUGUI _weaponDetailName;
        private TextMeshProUGUI _weaponDetailDescribe;
        
        private Color _color;

        private void Awake()
        {
            _backImage = GetComponent<Image>();
            _color = _backImage.color;
            _avatar = transform.Find("Avatar")?.GetComponent<Image>();

            // 从单例管理器获取详情面板控件引用
            if (WeaponUIManager.Instance)
            {
                _weaponDetailAvatar = WeaponUIManager.Instance.weaponDetailAvatar;
                _weaponDetailName = WeaponUIManager.Instance.weaponDetailName;
                _weaponDetailDescribe = WeaponUIManager.Instance.weaponDetailDescribe;
            }
            else
            {
                Debug.LogError("场景中未找到 WeaponUIManager！请确保已添加并配置。");
            }
        }

        // Start is called before the first frame update


        public void OnPointerEnter(PointerEventData eventData)
        {
            _backImage.color = Color.black;
            _backImage.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
            _avatar.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
            RenewUI(_weaponData);
        }

        private void RenewUI(WeaponData data)
        {
            if (data == null) return;
            // 鼠标悬停时更新详情面板（复用 SetWeaponData 的逻辑，或单独实现）
            _weaponDetailAvatar.sprite = Resources.Load<Sprite>(data.avatarImagePath);
            _weaponDetailName.text = data.name;
            _weaponDetailDescribe.text = data.describe;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _backImage.color = _color;
            _backImage.rectTransform.localScale = Vector3.one;
            _avatar.rectTransform.localScale = Vector3.one;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // 开始选择难度
            var uiManager = FindAnyObjectByType<UIManager>();
            uiManager.ShowDifficultyPanel();
        }

        public void SetWeaponData(WeaponData weaponData)
        {
            if (weaponData == null) return;
    
            var sprite = Resources.Load<Sprite>(weaponData.avatarImagePath);
            if (!sprite)
                Debug.LogError($"❌ 找不到图片资源: {weaponData.avatarImagePath}");
            else
                Debug.Log($"✅ 图片加载成功: {sprite.name}");

            _avatar.sprite = sprite; // ✅ 推荐用子物体 Avatar 的 Imag

            _weaponData = weaponData;
        }
    }
}