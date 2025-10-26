using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * 给每个role设置头像，信息等
 */
public class RoleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public Image _backImage;
    public Image _avatar;
    public Button _button;

    private RoleData roleData;
    
    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _avatar = transform.GetChild(0).GetComponent<Image>();
        _button = GetComponent<Button>();
    }

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
        // todo 可以在这里把角色头像放大
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = Color.gray;
    }

    /*
     * 点鸡后调用
     */
    public void RenewUI(RoleData data)
    {
        // 未解锁
        if (data.unlock.Equals(1))
        {
            
        }
        // 解锁
        else
        {
            // 修改详情页面
            
        }
        
    }
}
