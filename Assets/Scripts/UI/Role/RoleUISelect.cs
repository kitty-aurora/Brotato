using Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.Role

{
    public class RoleUISelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        
    
        private Image _backImage;
        private Image _avatar;

        // 缓存详情面板里的控件（自动寻找）
        private Image _roleDetailAvatar;
        private TextMeshProUGUI _roleDetailRoleName;
        private TextMeshProUGUI _roleDetailRoleDescribe;
        private TextMeshProUGUI _recordText;

        private RoleData _roleData;
        private Color _color;

        private void Awake()
        {
            _backImage = GetComponent<Image>();
            _color = _backImage.color;
            _avatar = transform.Find("Avatar")?.GetComponent<Image>();

            // 自动找到详情面板里的 UI 组件（按名称查找）
            var roleDetailPanel = GameObject.Find("RoleDetailPanel");
            var roleRecordPanel = GameObject.Find("RoleRecordPanel");
        
            if (roleDetailPanel != null && roleRecordPanel != null)
            {
                _roleDetailAvatar = roleDetailPanel.transform.Find("Avatar/Avatar_Role")?.GetComponent<Image>();
                _roleDetailRoleName = roleDetailPanel.transform.Find("RoleName")?.GetComponent<TextMeshProUGUI>();
                _roleDetailRoleDescribe = roleDetailPanel.transform.Find("RoleDescribe")?.GetComponent<TextMeshProUGUI>();
                _recordText = roleRecordPanel.transform.Find("Record_Text").GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogWarning("未找到角色详情面板（RoleDetailPanel）！");
            }
        }

        public void SetRoleData(RoleData data)
        {
            _roleData = data;
            // 设置角色头像
            if (_avatar != null)
            {
                var sprite = Resources.Load<Sprite>(data.avatarImagePath);
                if (sprite != null)
                    _avatar.sprite = sprite;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _backImage.color = Color.black;
            _backImage.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
            _avatar.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
            RenewRoleUI(_roleData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _backImage.color = _color;
            _backImage.rectTransform.localScale = Vector3.one;
            _avatar.rectTransform.localScale = Vector3.one;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var roleUIManager = FindObjectOfType<UIManager>();
            if (roleUIManager != null)
            {
                roleUIManager.ShowWeaponPanel();
            }
            else
            {
                Debug.LogError("未找到RoleUIManager实例！");
            }
        }


        private void RenewRoleUI(RoleData data)
        {
            if (data == null) return;

            if (_roleDetailAvatar != null)
                _roleDetailAvatar.sprite = Resources.Load<Sprite>(data.avatarImagePath);

            if (_roleDetailRoleName != null)
                _roleDetailRoleName.text = data.name;

            if (_roleDetailRoleDescribe != null)
                _roleDetailRoleDescribe.text = data.describe;

            if (_recordText != null)
                _recordText.text = data.record;
        }
    }
}