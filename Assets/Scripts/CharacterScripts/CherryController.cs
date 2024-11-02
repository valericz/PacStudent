using UnityEngine;
using System.Collections;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab;       // Cherry 的预制体
    public float spawnInterval = 10f;     // 生成时间间隔
    public float moveSpeed = 2f;          // Cherry 移动速度
    private GameObject currentCherry;     // 当前 Cherry 的实例
    private Vector3 spawnPosition;        // Cherry 的生成位置
    private Vector3 targetPosition;       // Cherry 的目标位置
    private Vector3 centerPoint = Vector3.zero; // 屏幕中心点

    void Start()
    {
        StartCoroutine(SpawnCherryRoutine());
    }

    IEnumerator SpawnCherryRoutine()
    {
        while (true)
        {
            // 等待 spawnInterval 秒
            yield return new WaitForSeconds(spawnInterval);

            // 确保旧的 Cherry 被销毁
            if (currentCherry != null)
            {
                Destroy(currentCherry);
            }

            // 设置生成位置和目标位置，使其穿越中心点
            SetSpawnAndTargetPositions();

            // 创建新的 Cherry 实例
            currentCherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);

            // 启动 Cherry 的移动协程
            StartCoroutine(MoveCherry(currentCherry));
        }
    }

    void SetSpawnAndTargetPositions()
    {
        // 随机选择 Cherry 从屏幕的左侧或右侧生成
        float randomY = Random.Range(-4f, 4f); // Y 轴随机高度 (根据实际需要调整)

        if (Random.Range(0, 2) == 0)
        {
            spawnPosition = new Vector3(-10f, randomY, 0f);  // 从屏幕左侧生成
            targetPosition = new Vector3(10f, -randomY, 0f);  // 目标在屏幕右侧，对称位置
        }
        else
        {
            spawnPosition = new Vector3(10f, randomY, 0f);   // 从屏幕右侧生成
            targetPosition = new Vector3(-10f, -randomY, 0f); // 目标在屏幕左侧，对称位置
        }
    }

    IEnumerator MoveCherry(GameObject cherry)
    {
        float journeyLength = Vector3.Distance(spawnPosition, targetPosition);
        float startTime = Time.time;

        while (cherry != null && Vector3.Distance(cherry.transform.position, targetPosition) > 0.1f)
        {
            // 跟踪时间，以线性方式移动 Cherry
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            cherry.transform.position = Vector3.Lerp(spawnPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        // 当 Cherry 超出屏幕视野后销毁
        if (cherry != null)
        {
            Destroy(cherry);
        }
    }
}
