using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private string lastInput = null;
    private string currentInput = null;
    private bool isLerping = false;
    private Vector3 startPos;
    private Vector3 endPos;
    private float lerpProgress;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 捕获持续按键输入
        if (Input.GetKey(KeyCode.W)) lastInput = "up";
        else if (Input.GetKey(KeyCode.A)) lastInput = "left";
        else if (Input.GetKey(KeyCode.S)) lastInput = "down";
        else if (Input.GetKey(KeyCode.D)) lastInput = "right";

        // 设置动画参数
        SetAnimationParameters();

        // 当不在lerping时，尝试开始新的移动
        if (!isLerping)
        {
            if (lastInput != null && lastInput != "")
            {
                Vector3 direction = GetDirection(lastInput);
                if (CanMove(direction))
                {
                    currentInput = lastInput;
                    StartLerp(direction);
                }
                else if (currentInput != null && CanMove(GetDirection(currentInput)))
                {
                    StartLerp(GetDirection(currentInput));
                }
            }
        }
        else
        {
            ContinueLerp();
        }
    }

    void SetAnimationParameters()
    {
        // 重置所有方向参数
        animator.SetBool("WalkUp", false);
        animator.SetBool("WalkDown", false);
        animator.SetBool("WalkLeft", false);
        animator.SetBool("WalkRight", false);

        // 根据输入方向设置对应的动画参数
        switch (lastInput)
        {
            case "up":
                animator.SetBool("WalkUp", true);
                break;
            case "down":
                animator.SetBool("WalkDown", true);
                break;
            case "left":
                animator.SetBool("WalkLeft", true);
                break;
            case "right":
                animator.SetBool("WalkRight", true);
                break;
        }
    }

    Vector3 GetDirection(string input)
    {
        switch (input)
        {
            case "up": return Vector3.up;
            case "left": return Vector3.left;
            case "down": return Vector3.down;
            case "right": return Vector3.right;
            default: return Vector3.zero;
        }
    }

    bool CanMove(Vector3 direction)
    {
        // 检查方向是否可行走的逻辑（伪代码）
        // 例如：return !Physics.Raycast(transform.position, direction, 1f);
        return true; // 暂时允许所有方向
    }

    void StartLerp(Vector3 direction)
    {
        startPos = transform.position;
        endPos = startPos + direction;
        lerpProgress = 0f;
        isLerping = true;
    }

    void ContinueLerp()
    {
        lerpProgress += Time.deltaTime * moveSpeed;
        transform.position = Vector3.Lerp(startPos, endPos, lerpProgress);

        // 当达到目标网格时
        if (lerpProgress >= 1f)
        {
            isLerping = false;
            transform.position = endPos;

            // 当停止输入时，确保 PacStudent 对齐到最近的网格
            if (!Input.anyKey)
            {
                currentInput = "";  // 停止当前移动方向
                lastInput = "";     // 停止任何新方向
                SetAnimationParameters(); // 重置所有方向动画参数
            }
        }
    }
}
