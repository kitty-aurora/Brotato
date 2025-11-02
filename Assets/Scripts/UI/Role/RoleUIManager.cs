using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleUIManager : MonoBehaviour
{
    [Header("角色卡片预制体")]
    public GameObject rolePrefab;

    [Header("角色列表容器")]
    public Transform roleListParent;
    
    // 角色数据
    private List<RoleData> roleList = new();

    private void Awake()
    {
        // 从 JSON 文件加载角色数据
        var roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleList = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
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
}