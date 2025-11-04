// 放在 UI.Weapon 命名空间下

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Weapon
{
    public class WeaponUIManager : MonoBehaviour
    {
        // 单例实例（确保场景中只有一个 WeaponUIManager）
        public static WeaponUIManager Instance;
        
        // 场景中详情面板的控件（在Inspector中手动赋值）
        public Image weaponDetailAvatar;       // 拖入场景中的 Avatar_Weapon
        public TextMeshProUGUI weaponDetailName; // 拖入场景中的 WeaponName
        public TextMeshProUGUI weaponDetailDescribe; // 拖入场景中的 WeaponDescribe

        private void Awake()
        {
            // 确保单例唯一
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
    }
}