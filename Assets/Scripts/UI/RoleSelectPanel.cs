using System.Collections.Generic;
using Model;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class RoleSelectPanel : MonoBehaviour
{
    private static RoleSelectPanel instance;

    private List<RoleData> roleList = new List<RoleData>();
    private TextAsset roleTextAsset;

    public Transform _roleList;
    public GameObject _rolePrefab;
    public  TextMeshPro _roleNameText;
        
    private void Awake()
    {
        instance = this;

        roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleList = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (RoleData roleData in roleList)
        {
          Instantiate(_rolePrefab, _roleList.transform);
          
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}