using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 5f;  // 控制移动速度
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    void Start()
    {
        // 获取 Animator 和 SpriteRenderer 组件
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 玩家按下空格触发死亡动画
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Die(); // 调用 Die 函数
        }

        if (!isDead)
        {
            // 获取玩家的水平和垂直输入
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 将输入值传递给 Animator 以控制动画切换
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);

            // 控制角色移动
            Vector3 movement = new Vector3(horizontal, vertical, 0f);
            transform.position += movement * moveSpeed * Time.deltaTime;

            // 处理角色朝向
            HandleSpriteDirection(horizontal, vertical);
        }
    }

    void HandleSpriteDirection(float horizontal, float vertical)
    {
        if (horizontal != 0)
        {
            spriteRenderer.flipX = horizontal < 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (vertical > 0)
        {
            spriteRenderer.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (vertical < 0)
        {
            spriteRenderer.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    // 定义 Die 方法
    void Die()
    {
        isDead = true;  // 设置死亡状态为 true
        animator.SetBool("isDead", true);  // 触发死亡动画

        // 停止所有移动
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
    }
}
