using System.Collections;
using UnityEngine;

public class PelletController : MonoBehaviour
{
    public Sprite normalPelletSprite;  // 普通 Pellet 的 sprite
    public Sprite powerPelletSprite;   // Power Pellet 的 sprite
    public float flashInterval = 0.5f; // 切换 sprite 的间隔时间

    private SpriteRenderer spriteRenderer;
    private bool isFlashing = false;   // 是否正在闪烁

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("PelletController: 未找到 SpriteRenderer 组件！");
            return;
        }

        // 初始化 Pellet 为普通或 Power 状态
        spriteRenderer.sprite = normalPelletSprite;

        // 启动闪烁效果
        StartFlashing();
    }

    void StartFlashing()
    {
        // 使用协程定时切换 sprite 实现闪烁效果
        isFlashing = true;
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        while (isFlashing)
        {
            // 切换到 Power Pellet 的 sprite
            spriteRenderer.sprite = powerPelletSprite;
            yield return new WaitForSeconds(flashInterval);

            // 切换回普通 Pellet 的 sprite
            spriteRenderer.sprite = normalPelletSprite;
            yield return new WaitForSeconds(flashInterval);
        }
    }

    void StopFlashing()
    {
        // 停止闪烁
        isFlashing = false;
        StopCoroutine(FlashRoutine());
        // 重置为普通 Pellet 的 sprite
        spriteRenderer.sprite = normalPelletSprite;
    }
}
