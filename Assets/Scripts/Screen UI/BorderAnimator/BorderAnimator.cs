using UnityEngine;
using UnityEngine.UI;

public class BorderAnimator : MonoBehaviour
{
    public Image borderImage;  // 把你的 border 对象的 Image 组件拖到这个槽里

    // 定义颜色数组：紫色、粉色、蓝色
    private Color[] colors = new Color[]
    {
        new Color(0.6f, 0f, 1f),  // 紫色
        new Color(1f, 0.4f, 0.8f),  // 粉色
        new Color(0f, 0.6f, 1f)  // 蓝色
    };

    public float transitionSpeed = 2f; // 控制颜色变化速度

    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float t = 0f;

    void Update()
    {
        // 检查 borderImage 是否已分配
        if (borderImage == null)
        {
            Debug.LogWarning("Border Image is not assigned!");
            return;
        }

        // 在两种颜色之间平滑过渡
        borderImage.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);

        // 增加 t 直到达到 1
        t += Time.deltaTime * transitionSpeed;

        // 当 t 达到 1 时，切换到下一组颜色
        if (t >= 1f)
        {
            t = 0f;
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % colors.Length;  // 循环颜色数组
        }
    }
}
