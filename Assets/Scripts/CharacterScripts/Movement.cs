using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    public float speed = 5f;  // 移动速度
    private Vector3[] pathPoints; // 路径点数组
    private int currentPointIndex = 0;  // 当前目标点的索引
    private AudioSource audioSource;  // 音频源组件

    void Start()
    {
        // 获取AudioSource组件
        audioSource = GetComponent<AudioSource>();

        // 定义PacStudent的移动路径点，按照你提供的坐标设置
        pathPoints = new Vector3[] {
            new Vector3(-4.00f, 4.36f, 0),   // 左上角
            new Vector3(-0.26f, 4.36f, 0),   // 右上角
            new Vector3(-0.26f, 0.36f, 0),   // 右下角
            new Vector3(-4.00f, 0.36f, 0)    // 左下角
        };
    }

    void Update()
    {
        // 帧率无关的移动
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pathPoints[currentPointIndex], step);

        // 播放音效
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        // 检查是否到达当前目标点
        if (Vector3.Distance(transform.position, pathPoints[currentPointIndex]) < 0.1f)
        {
            // 到达目标点后，切换到下一个路径点
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
        }
    }
}
