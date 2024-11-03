using UnityEngine;

public class PacStudentController : MonoBehaviour
{
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
    }

    void Update()
    {
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
        }
        else
        {
            ContinueLerp();
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

    Vector3 GetDirection(string input)
    {
        // Convert input string to corresponding direction vector
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
}
