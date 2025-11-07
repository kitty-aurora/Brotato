using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
    
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        public float moveSpeed = 5f;
        public GameObject visual; // 引用包含SpriteRenderer的视觉对象（如角色的Sprite子对象）
    
        private Animator _animator;
        private SpriteRenderer _spriteRenderer; // 修正：改为SpriteRenderer类型，而非object

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                // 防止重复创建多个Player实例
                Destroy(gameObject);
                Debug.LogWarning("重复创建了Player实例，已自动销毁多余实例");
            }
            
            // 获取Animator组件（注意：如果Animator在visual子对象上，需要用visual.GetComponent<Animator>()）
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Player对象上没有找到Animator组件！");
            }
        }
        void Start()
        {
            // 确保visual不为空，再获取SpriteRenderer
            if (visual != null)
            {
                _spriteRenderer = visual.GetComponent<SpriteRenderer>();
                // 容错：如果没找到SpriteRenderer，打印错误提示
                if (_spriteRenderer == null)
                {
                    Debug.LogError("visual对象上没有找到SpriteRenderer组件！");
                }
            }
            else
            {
                Debug.LogError("请在Inspector中给visual字段赋值（拖入包含Sprite的对象）！");
            }

           
        }

        private void Update()
        {
            MoveAndFlip();
        }

        private void MoveAndFlip()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var movement = new Vector2(horizontal * moveSpeed * Time.deltaTime, vertical * moveSpeed * Time.deltaTime);
            transform.Translate(movement);

            // 翻转逻辑：先判断spriteRenderer是否存在，避免空引用错误
            if (_spriteRenderer)
            {
                if (horizontal > 0.1f)
                {
                    _spriteRenderer.flipX = false;
                }
                else if (horizontal < -0.1f)
                {
                    _spriteRenderer.flipX = true;
                }
            }

            // 移动动画控制
            bool isMoving = Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f;
            if (_animator) // 避免animator为空时出错
            {
                _animator.SetBool(IsMove, isMoving);
            }
        }
    }
}