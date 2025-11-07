using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using TMPro;
using UI.Difficulty;
using UI.Role;
using UI.Weapon;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class UIManager : MonoBehaviour
    { 
        [Header("下一个场景标题")][SerializeField] private string sceneToLoad; 
        
        [Header("标题")] public TextMeshProUGUI titleText;
        
        [Header("角色列表容器")] public Transform roleListParent;
        [Header("角色卡片预制体")] public GameObject rolePrefab;
     
        [Header("武器列表容器")] public Transform weaponListParent;
        [Header("武器卡片预制体")] public GameObject weaponPrefab;
        
        [Header("难度列表容器")] public Transform difficultyListParent;
        [Header("难度卡片预制体")] public GameObject difficultyPrefab;
        
        [Header("面板位置")]
        public GameObject recordPanel;
        public GameObject weaponSelectPanel;
        public GameObject difficultySelectPanel;
        [Header("下方列表list")]
        public GameObject roleList;
        public GameObject weaponList;
        public GameObject difficultyList;
        // 角色数据
        private List<RoleData> _roleList = new();
        // 武器数据
        private List<WeaponData> _weaponList = new();
        // 难度数据
        private List<DifficultyData> _difficultyList = new();

        private void Awake()
        {
            // 从 JSON 文件加载角色数据
            var roleTextAsset = Resources.Load<TextAsset>("Data/role");
            _roleList = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
            // 加载武器数据
            var weaponTextAsset = Resources.Load<TextAsset>("Data/weapon");
            _weaponList = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);
            
            // 加载难度数据
            var difficultyTextAsset = Resources.Load<TextAsset>("Data/difficulty");
            _difficultyList = JsonConvert.DeserializeObject<List<DifficultyData>>(difficultyTextAsset.text);
            
        }

        private void Start()
        {
            foreach (var roleData in _roleList)
            {
                // 1. 实例化角色卡片
                var roleGo = Instantiate(rolePrefab, roleListParent);
                // 2. 传入数据
                var roleSelect = roleGo.GetComponent<RoleUISelect>();
                roleSelect.SetRoleData(roleData);
            }
        }

        public void ShowWeaponPanel()
        {
            
            titleText.text = "武器选择";
        
            recordPanel.SetActive(false);
            roleList.SetActive(false);

            weaponSelectPanel.SetActive(true);
            weaponList.SetActive(true);
            // 实例化武器卡片
            foreach (var weaponData in _weaponList)
            {
                var weaponGo = Instantiate(weaponPrefab, weaponListParent);
                var weaponSelect = weaponGo.GetComponent<WeaponUISelect>();
                weaponSelect.SetWeaponData(weaponData);
            }
        }

        public void ShowDifficultyPanel()
        {
            titleText.text = "难度选择";
            
            weaponList.SetActive(false);
                
            difficultySelectPanel.SetActive(true);
            difficultyList.SetActive(true);
            // 实例化难度卡片
            foreach (var difficulty in _difficultyList)
            {
                var difficultyGo = Instantiate(difficultyPrefab, difficultyListParent);
                var difficultySelect = difficultyGo.GetComponent<DifficultyUISelect>();
                difficultySelect.SetDifficultyData(difficulty);
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

    }
}