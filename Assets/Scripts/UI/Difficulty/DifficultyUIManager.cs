using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Difficulty
{
   public class DifficultyUIManager : MonoBehaviour
   {
      public static DifficultyUIManager Instance;
   
     
      // 场景中详情面板的控件（在Inspector中手动赋值）
      public Image difficultyDetailAvatar;       // 拖入场景中的 Avatar_Weapon
      public TextMeshProUGUI difficultyDetailName; // 拖入场景中的 WeaponName
      public TextMeshProUGUI difficultyDetailDescribe; // 拖入场景中的 WeaponDescribe

      private void Awake()
      {
         if (Instance == null)
            Instance = this;
         else
            Destroy(gameObject);
           
      }
   }
}
