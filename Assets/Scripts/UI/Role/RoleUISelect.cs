using System.Collections;
using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleUISelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 角色
    public Image _backImage;
    public Image _avatar;

    // 上面的控制面板
    public Button _button;
    public Image _roleDetailAvatar;
    public TextMeshProUGUI _roleDetailRoleName;
    public TextMeshProUGUI _roleDetailRoleDescribe;

    // 缓存 给面板传递角色头像
    private RoleData _roleData;
    // Start is called before the first frame update
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = Color.black;
        _backImage.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
        _avatar.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
        RenewUI(_roleData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = new Color(35 / 255f, 35 / 255f, 35 / 255f);
        _backImage.rectTransform.localScale = Vector3.one;
        _avatar.rectTransform.localScale = Vector3.one;
    }

    /*
     * 点鸡后调用
     */
    public void RenewUI(RoleData data)
    {
        _roleDetailAvatar.sprite = Resources.Load<Sprite>(data.avatarImagePath);
        _roleDetailRoleName.text = data.name;
        _roleDetailRoleDescribe.text = data.describe;
        // // 未解锁
        // if (data.unlock.Equals(1))
        // {
        //     
        // }
        // // 解锁
        // else
        // {
        //     // 修改详情页面
        //     
        // }
    }

    
}
