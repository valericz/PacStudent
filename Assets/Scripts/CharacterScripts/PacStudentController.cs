using UnityEngine;

public class PacStudentController : MonoBehaviour
{
<<<<<<< Updated upstream
    public float moveSpeed = 5f;  // 控制移动速度
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    void Start()
    {
        // 获取 Animator 和 SpriteRenderer 组件
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
=======
    public float moveSpeed = 5f;
    public AudioClip moveAudioClip;         // Audio clip for regular movement
    public AudioClip eatPelletAudioClip;    // Audio clip for eating a pellet

    private string lastInput = null;
    private string currentInput = null;
    private bool isLerping = false;
    private Vector3 startPos;
    private Vector3 endPos;
    private float lerpProgress;
    private Animator animator;
    private AudioSource audioSource;

    private float pelletEatInterval = 0.6f; // Extended interval for playing eat pellet sound
    private float pelletEatTimer = 0f;      // Timer for eat pellet sound

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Start looping the move audio
        PlayMoveAudio();
>>>>>>> Stashed changes
    }

    void Update()
    {
<<<<<<< Updated upstream
        // 玩家按下空格触发死亡动画
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Die(); // 调用 Die 函数
=======
        // Capture continuous input
        if (Input.GetKey(KeyCode.W)) lastInput = "up";
        else if (Input.GetKey(KeyCode.A)) lastInput = "left";
        else if (Input.GetKey(KeyCode.S)) lastInput = "down";
        else if (Input.GetKey(KeyCode.D)) lastInput = "right";

        // Set animation parameters
        SetAnimationParameters();

        // Attempt to start a new movement if not currently lerping
        if (!isLerping)
        {
            if (!string.IsNullOrEmpty(lastInput))
            {
                Vector3 direction = GetDirection(lastInput);
                if (CanMove(direction))
                {
                    currentInput = lastInput;
                    StartLerp(direction);
                }
                else if (!string.IsNullOrEmpty(currentInput) && CanMove(GetDirection(currentInput)))
                {
                    StartLerp(GetDirection(currentInput));
                }
            }
>>>>>>> Stashed changes
        }

        if (!isDead)
        {
<<<<<<< Updated upstream
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
=======
            ContinueLerp();
>>>>>>> Stashed changes
        }

        // Play pellet eating sound at intervals while moving
        pelletEatTimer += Time.deltaTime;
        if (pelletEatTimer >= pelletEatInterval && isLerping)
        {
            PlayEatPelletSound();
            pelletEatTimer = 0f; // Reset timer
        }
    }

    void SetAnimationParameters()
    {
        // Reset all directional parameters
        animator.SetBool("WalkUp", false);
        animator.SetBool("WalkDown", false);
        animator.SetBool("WalkLeft", false);
        animator.SetBool("WalkRight", false);

        // Set the appropriate animation parameter based on input direction
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

    void HandleSpriteDirection(float horizontal, float vertical)
    {
<<<<<<< Updated upstream
        if (horizontal != 0)
=======
        // Convert input string to corresponding direction vector
        switch (input)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        isDead = true;  // 设置死亡状态为 true
        animator.SetBool("isDead", true);  // 触发死亡动画

        // 停止所有移动
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
    }
=======
        // Logic to check if movement in the given direction is allowed (stubbed for now)
        return true; // Allow all directions temporarily
    }

    void StartLerp(Vector3 direction)
    {
        // Initialize lerping values for smooth movement
        startPos = transform.position;
        endPos = startPos + direction;
        lerpProgress = 0f;
        isLerping = true;
    }

    void ContinueLerp()
    {
        // Perform lerping to smoothly move from start to end position
        lerpProgress += Time.deltaTime * moveSpeed;
        transform.position = Vector3.Lerp(startPos, endPos, lerpProgress);

        // Once reaching the target grid position
        if (lerpProgress >= 1f)
        {
            isLerping = false;
            transform.position = endPos;

            // When input stops, ensure PacStudent aligns to the nearest grid
            if (!Input.anyKey)
            {
                currentInput = "";  // Stop current movement direction
                lastInput = "";     // Stop any new direction
                SetAnimationParameters(); // Reset all direction animation parameters
                audioSource.Stop(); // Stop audio when completely stopped
            }
        }
    }

    void PlayMoveAudio()
    {
        // Start looping the move audio if it's not already playing
        if (!audioSource.isPlaying || audioSource.clip != moveAudioClip)
        {
            audioSource.clip = moveAudioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void PlayEatPelletSound()
    {
        // Debug: Log to confirm the eat pellet sound is being triggered
        Debug.Log("Playing eat pellet sound!");
        audioSource.PlayOneShot(eatPelletAudioClip, 1.0f); // Full volume for debugging
    }
>>>>>>> Stashed changes
}
