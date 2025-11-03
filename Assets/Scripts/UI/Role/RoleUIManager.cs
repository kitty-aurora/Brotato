using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleUIManager : MonoBehaviour
{
    [Header("角色卡片预制体")] public GameObject rolePrefab;

    [Header("角色列表容器")] public Transform roleListParent;


    private TextMeshProUGUI _titleText;
    private GameObject _recordPanel;
    private GameObject _weaponSelectPanel;

    private GameObject _roleList;
    private GameObject _weaponList;

    // 角色数据
    private List<RoleData> roleList = new();

    // 武器数据
    private List<WeaponData> weaponList = new();

    private void Awake()
    {
        // 从 JSON 文件加载角色数据
        var roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleList = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
        // 加载武器数据
        var weaponTextAsset = Resources.Load<TextAsset>("Data/weapon");
        weaponList = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);

        _recordPanel = GameObject.Find("RoleRecordPanel");
        _weaponSelectPanel = GameObject.Find("WeaponSelectPanel");
        _titleText = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        foreach (var roleData in roleList)
        {
            // 1. 实例化卡片
            var roleGO = Instantiate(rolePrefab, roleListParent);

            // 2. 传入数据
            var roleSelect = roleGO.GetComponent<RoleUISelect>();
            roleSelect.SetRoleData(roleData);
        }
    }

    public void ShowWeaponPanel()
    {
        _titleText.text = "武器选择";
        
        _recordPanel.SetActive(false);
        _roleList.SetActive(false);
        
        _weaponSelectPanel.SetActive(true);
        _weaponList.SetActive(true);
        
       
    }
}