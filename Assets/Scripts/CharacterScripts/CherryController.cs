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
            // 等待指定的时间间隔
            yield return new WaitForSeconds(spawnInterval);

            // 确保旧的 Cherry 被销毁
            if (currentCherry != null)
            {
                Destroy(currentCherry);
                currentCherry = null; // 确保引用清除，避免重复检查
                yield return new WaitForEndOfFrame(); // 等待一帧确保销毁完成
            }

            // 设置生成位置和目标位置，使 Cherry 穿越中心点
            SetSpawnAndTargetPositions();

            // 创建新的 Cherry 实例
            currentCherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);

            // 启动 Cherry 的分段移动协程
            StartCoroutine(MoveCherryThroughCenter(currentCherry));
        }
    }

    void SetSpawnAndTargetPositions()
    {
        // 随机选择 Cherry 从屏幕四边生成
        float randomValue = Random.Range(-8f, 8f); // 随机生成位置范围，根据需要调整

        int side = Random.Range(0, 4); // 0:上, 1:下, 2:左, 3:右
        switch (side)
        {
            case 0: // 上方
                spawnPosition = new Vector3(randomValue, 8f, 0f); // 从屏幕上方生成
                break;
            case 1: // 下方
                spawnPosition = new Vector3(randomValue, -8f, 0f); // 从屏幕下方生成
                break;
            case 2: // 左侧
                spawnPosition = new Vector3(-8f, randomValue, 0f); // 从屏幕左侧生成
                break;
            case 3: // 右侧
                spawnPosition = new Vector3(8f, randomValue, 0f); // 从屏幕右侧生成
                break;
        }

        // 目标位置：以中心点为对称轴
        targetPosition = -spawnPosition;
    }

    IEnumerator MoveCherryThroughCenter(GameObject cherry)
    {
        // Step 1: 从生成位置移动到中心点
        float journeyLengthToCenter = Vector3.Distance(spawnPosition, centerPoint);
        float startTime = Time.time;

        while (cherry != null && Vector3.Distance(cherry.transform.position, centerPoint) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLengthToCenter;
            cherry.transform.position = Vector3.Lerp(spawnPosition, centerPoint, fractionOfJourney);
            yield return null;
        }

        // Step 2: 从中心点移动到目标位置
        float journeyLengthToTarget = Vector3.Distance(centerPoint, targetPosition);
        startTime = Time.time;

        while (cherry != null && Vector3.Distance(cherry.transform.position, targetPosition) > 0.1f)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLengthToTarget;
            cherry.transform.position = Vector3.Lerp(centerPoint, targetPosition, fractionOfJourney);
            yield return null;
        }

        // 当 Cherry 到达目标位置后自动销毁
        if (cherry != null)
        {
            Destroy(cherry);
            currentCherry = null; // 确保 currentCherry 引用被清空
        }
    }
}
