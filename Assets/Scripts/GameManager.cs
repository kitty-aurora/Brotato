using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("当前角色数据")] public RoleData currentRole;
    [Header("当前武器")] public List<WeaponData> currentWeapons = new List<WeaponData>();
    [Header("当前难度")]  public DifficultyData currentDifficulty;
  
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
}