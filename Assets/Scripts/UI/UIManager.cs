using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using TMPro;
using UI.Role;
using UI.Weapon;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
      
        [Header("角色列表容器")] public Transform roleListParent;
        [Header("角色卡片预制体")] public GameObject rolePrefab;
     
        [Header("武器列表容器")] public Transform weaponListParent;
        [Header("武器卡片预制体")] public GameObject weaponPrefab;
        
        private TextMeshProUGUI _titleText;
        public GameObject recordPanel;
        public GameObject weaponSelectPanel;

        public GameObject roleList;
        public GameObject weaponList;

        // 角色数据
        private List<RoleData> _roleList = new();

        // 武器数据
        private List<WeaponData> _weaponList = new();

        private void Awake()
        {
            // 从 JSON 文件加载角色数据
            var roleTextAsset = Resources.Load<TextAsset>("Data/role");
            _roleList = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
            // 加载武器数据
            var weaponTextAsset = Resources.Load<TextAsset>("Data/weapon");
            _weaponList = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);
            
            _titleText = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            foreach (var roleData in _roleList)
            {
                // 1. 实例化卡片
                var roleGo = Instantiate(rolePrefab, roleListParent);
                // 2. 传入数据
                var roleSelect = roleGo.GetComponent<RoleUISelect>();
                roleSelect.SetRoleData(roleData);
            }
            
          
        }

        public void ShowWeaponPanel()
        {
            
            _titleText.text = "武器选择";
        
            recordPanel.SetActive(false);
            roleList.SetActive(false);

            weaponSelectPanel.SetActive(true);
            weaponList.SetActive(true);
            
            foreach (var weaponData in _weaponList)
            {
                var weaponGo = Instantiate(weaponPrefab, weaponListParent);
                var weaponSelect = weaponGo.GetComponent<WeaponUISelect>();
                weaponSelect.SetWeaponData(weaponData);
            }
        }
    }
}