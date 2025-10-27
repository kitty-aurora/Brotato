using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * 给每个role设置头像，信息等
 */
public class RoleUIManager : MonoBehaviour
{
    private RoleData _roleData;

    public GameObject _rolePrefab;

    // Role List
    private List<RoleData> roleList = new();
    public Transform _roleList;

    private void Awake()
    {
        // 从josn文件中读取roleList
        var roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleList = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (RoleData roleData in roleList)
        {
            // 1. 实例化角色预制体
            var role = Instantiate(_rolePrefab, _roleList.transform);
            // 2. 显示头像, roleList完成渲染
            SetRoleAvatar(role, roleData);
            
            // 3. 传递角色其他数据给上级UI 
            // RoleUISelect roleUISelect = role.GetComponent<RoleUISelect>();
            // SetRoleAvatar(role, roleData);
        }
    }

    public void SetRoleAvatar(GameObject role, RoleData roleData)
    {
        Image roleImage = role.GetComponentInChildren<Image>();
        roleImage.sprite = Resources.Load<Sprite>(roleData.avatarImagePath);
    }
}