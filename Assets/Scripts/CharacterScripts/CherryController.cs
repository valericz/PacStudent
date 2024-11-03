using UnityEngine;
using System.Collections;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab;       // Cherry prefab
    public float spawnInterval = 10f;     // Spawn interval (10 seconds)
    public float moveSpeed = 2f;          // Cherry movement speed
    private GameObject currentCherry;     // Current Cherry instance
    private Vector3 spawnPosition;        // Cherry spawn position
    private Vector3 targetPosition;       // Cherry target position
    private Vector3 centerPoint = Vector3.zero; // Level center point
    public int cherryOrderInLayer = 10;   // Dynamic order in layer for Cherry

    void Start()
    {
        StartCoroutine(SpawnCherryRoutine());
    }

    IEnumerator SpawnCherryRoutine()
    {
        while (true)
        {
            // Wait for the specified spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Ensure the previous cherry is destroyed
            if (currentCherry != null)
            {
                Destroy(currentCherry);
                yield return new WaitForEndOfFrame(); // Ensure destruction completes
            }

            // Set random spawn and target positions
            SetSpawnAndTargetPositions();

            // Instantiate the new cherry
            currentCherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);

            // Dynamically set Order in Layer for the Cherry
            currentCherry.GetComponent<SpriteRenderer>().sortingOrder = cherryOrderInLayer;

            // Start the movement coroutine
            StartCoroutine(MoveCherryThroughCenter(currentCherry));
        }
    }

    void SetSpawnAndTargetPositions()
    {
        // Randomly choose a side to spawn the cherry
        float randomValue = Random.Range(-8f, 8f); // Random range based on screen boundaries

        int side = Random.Range(0, 4); // 0: top, 1: bottom, 2: left, 3: right
        switch (side)
        {
            case 0: // Top
                spawnPosition = new Vector3(randomValue, 8f, 0f);
                break;
            case 1: // Bottom
                spawnPosition = new Vector3(randomValue, -8f, 0f);
                break;
            case 2: // Left
                spawnPosition = new Vector3(-8f, randomValue, 0f);
                break;
            case 3: // Right
                spawnPosition = new Vector3(8f, randomValue, 0f);
                break;
        }

        // Set the target position to the opposite side, ensuring it crosses the center point
        targetPosition = -spawnPosition;
    }

    IEnumerator MoveCherryThroughCenter(GameObject cherry)
    {
        // Move cherry from spawn position to the center point
        Vector3 journeyStart = spawnPosition;
        Vector3 journeyEnd = centerPoint;
        float journeyLength = Vector3.Distance(journeyStart, journeyEnd);
        float journeyProgress = 0f;

        while (cherry != null && journeyProgress < 1f)
        {
            journeyProgress += Time.deltaTime * moveSpeed / journeyLength;
            cherry.transform.position = Vector3.Lerp(journeyStart, journeyEnd, journeyProgress);
            yield return null;
        }

        // Move cherry from the center point to the target position (outside camera view)
        journeyStart = centerPoint;
        journeyEnd = targetPosition;
        journeyLength = Vector3.Distance(journeyStart, journeyEnd);
        journeyProgress = 0f;

        while (cherry != null && journeyProgress < 1f)
        {
            journeyProgress += Time.deltaTime * moveSpeed / journeyLength;
            cherry.transform.position = Vector3.Lerp(journeyStart, journeyEnd, journeyProgress);
            yield return null;
        }

        // Destroy the cherry once it has reached the target position outside camera view
        if (cherry != null)
        {
            Destroy(cherry);
        }
    }
}
