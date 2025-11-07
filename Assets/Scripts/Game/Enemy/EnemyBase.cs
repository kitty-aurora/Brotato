using Model;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyBase : MonoBehaviour

    {
        private SpriteRenderer _spriteRenderer;
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        private Animator _animator;

        public float attackTimer = 0; // 攻击定时器
        public bool isContact = false; // 是否接触到玩家
        private bool isCooling = false; // 攻击冷却

        [SerializeField] public EnemyData enemyData;

        public float hp; // 血量
        public float damage; // 攻击力
        public float speed = 3; // 移动速度
        public float attackTime; // 攻击间隔
        public int provideExp = 1; // 经验值

        public float skillTimer = 0; // 技能计时器 
        public bool skilling = false; // 技能是否正在释放


        public void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GetComponent<Animator>().SetBool(IsMove, true);
        }

        public void Start()
        {
            // 检查enemyData是否未赋值（在Inspector中配置）
            if (enemyData == null)
            {
                Debug.LogError("EnemyBase组件的enemyData未赋值！请在Inspector中指定EnemyData对象", this);
            }
            else
            {
                Debug.Log($"Enemy {name} 初始化，enemyData已赋值", this);
            }
        }

        public void Update()
        {
            Move(); // 移动
            HandleAttack(); // 封装后的攻击+冷却逻辑
           // UpdateSkill();
        }

        // 自动移动
        public void Move()
        {
            Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
            transform.Translate(direction * (speed * Time.deltaTime));
            TurnAround();
        }

        // 自动转向
        public void TurnAround()
        {
            var playerX = Player.Instance.transform.position.x;
            var enemyX = transform.position.x;
            // 玩家在怪物右边
            if (playerX - enemyX >= 0.1)
            {
                _spriteRenderer.flipX = false;
            }
            // 玩家在怪物左边
            else if (playerX - enemyX < 0.1)
            {
                _spriteRenderer.flipX = true;
            }
        }

        private void HandleAttack()
        {
            // 处理攻击冷却计时
            if (isCooling)
            {
                attackTimer -= Time.deltaTime;
                if (!(attackTimer <= 0)) return;
                attackTimer = 0;
                isCooling = false;
                return;
            }

            // 满足攻击条件时执行攻击
            if (!isContact) return;
            Attack();
            isCooling = true;
            attackTimer = enemyData.attackTime;
        }

        public void SetElite()
        {
            if (enemyData == null) return; // 空值保护

            enemyData.hp *= 2;
            enemyData.damage *= 2;

            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(255 / 255f, 113 / 255f, 113 / 255f);
            }
        }


        // ReSharper disable Unity.PerformanceAnalysis
        private void UpdateSkill()
        {
            // 检查enemyData是否存在且技能时间有效
            if (enemyData == null || enemyData.skillTime < 0)
            {
                Debug.Log("UpdateSkill: enemyData为空或技能时间无效，跳过技能逻辑", this);
                return;
            }

            // 判定是否冷却
            if (skillTimer <= 0)
            {
                // 检查玩家实例是否存在
                if (!Player.Instance)
                {
                    Debug.Log("UpdateSkill: 玩家实例不存在，跳过技能触发", this);
                    return;
                }

                // 判断距离
                float dis = Vector2.Distance(transform.position, Player.Instance.transform.position);
                Debug.Log($"UpdateSkill: 与玩家距离为{dis}，技能范围为{enemyData.range}", this);
                if (dis <= enemyData.range)
                {
                    // 发动技能
                    Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
                    LaunchSkill(dir);
                    skillTimer = enemyData.skillTime;
                    Debug.Log("UpdateSkill: 发动技能", this);
                }
            }
            else
            {
                skillTimer -= Time.deltaTime;
                if (skillTimer < 0)
                {
                    skillTimer = 0;
                }
            }
        }

        public void LaunchSkill(Vector2 dir)
        {
            // 技能逻辑（可自行实现）
        }
        

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            isContact = true;
            Debug.Log("OnTriggerEnter2D: 接触到玩家", this);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            isContact = false;
            Debug.Log("OnTriggerExit2D: 离开玩家接触", this);
        }


        // 攻击
        // ReSharper disable Unity.PerformanceAnalysis
        public void Attack()
        {
            if (isCooling || enemyData == null)
            {
                Debug.Log("Attack: 攻击冷却中或enemyData为空，跳过攻击", this);
                return;
            }

            // 攻击玩家（确保玩家存在）
            if (Player.Instance)
            {
                Debug.Log("Attack: 对玩家发起攻击", this);
                // Player.Instance.Injured(enemyData.damage); // 按需启用
            }

            // 进入攻击冷却
            isCooling = true;
            attackTimer = enemyData.attackTime;
            Debug.Log($"Attack: 进入攻击冷却，冷却时间{enemyData.attackTime}", this);
        }

        // 受伤
        public void Injured(float attack)
        {
            if (enemyData == null)
            {
                Debug.Log("Injured: enemyData为空，跳过受伤逻辑", this);
                return;
            }

            // 判断是否死亡
            if (enemyData.hp - attack <= 0)
            {
                enemyData.hp = 0;
                Dead();
                Debug.Log("Injured: 敌人死亡", this);
            }
            else
            {
                enemyData.hp -= attack;
                Debug.Log($"Injured: 敌人剩余血量{enemyData.hp}", this);
            }
        }

        // 死亡
        public void Dead()
        {
            Debug.Log("Enemy死亡", this);
            // 增加玩家经验值（按需启用）
            // if (GameManager.Instance != null)
            // {
            //     GameManager.Instance.exp = enemyData.provideExp * GameManager.Instance.propData.expMuti;
            //     GamePanel.Instance?.RenewExp();
            //
            //     // 掉落金币
            //     if (GameManager.Instance.moeny_prefab != null)
            //     {
            //         Instantiate(GameManager.Instance.moeny_prefab, transform.position, Quaternion.identity);
            //     }
            // }
            //
            // // 销毁自己
            // Destroy(gameObject);
        }
    }
}