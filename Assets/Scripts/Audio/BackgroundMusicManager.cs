using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource audioSource; // Assign AudioSource in the inspector
    public AudioClip gameIntro; // 游戏介绍音乐
    public AudioClip ghostsNormal; // 幽灵正常状态背景音乐
    public AudioClip ghostsScared; // 幽灵惊恐状态背景音乐
    public AudioClip ghostDead; // 幽灵死亡背景音乐

    void Start()
    {
        // 播放游戏的介绍音乐
        audioSource.clip = gameIntro;
        audioSource.Play();

        // 在游戏介绍音乐结束后，播放幽灵的正常状态音乐
        Invoke("PlayGhostsNormalMusic", gameIntro.length); // 使用游戏介绍音乐的长度来设定切换时间
    }

    void PlayGhostsNormalMusic()
    {
        // 切换到幽灵正常状态的音乐
        audioSource.clip = ghostsNormal;
        audioSource.Play();
    }

    // 你可以根据需要添加类似的函数，用来切换到其他状态的音乐：
    // 比如幽灵惊恐状态或幽灵死亡时的音乐
    void PlayGhostsScaredMusic()
    {
        audioSource.clip = ghostsScared;
        audioSource.Play();
    }

    void PlayGhostDeadMusic()
    {
        audioSource.clip = ghostDead;
        audioSource.Play();
    }
}
