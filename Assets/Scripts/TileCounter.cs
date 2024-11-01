using UnityEngine;
using System.Collections.Generic;

public class TileCounter : MonoBehaviour
{
    void Start()
    {
        // 使用 Dictionary 来存储每种 tile 类型和其数量
        Dictionary<string, int> tileTypeCounts = new Dictionary<string, int>();

        // 获取场景中所有 tile 对象（假设它们都有一个统一的标签 "Tile"）
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            // 获取每个 tile 的名字前缀
            string tileName = tile.name.Split('_')[0]; // 获取前缀部分

            // 如果前缀已经存在于字典中，则计数加 1；否则添加到字典并初始化为 1
            if (tileTypeCounts.ContainsKey(tileName))
            {
                tileTypeCounts[tileName]++;
            }
            else
            {
                tileTypeCounts[tileName] = 1;
            }
        }

        // 输出每种 tile 的名称和数量
        foreach (KeyValuePair<string, int> entry in tileTypeCounts)
        {
            Debug.Log("Tile Type: " + entry.Key + ", Count: " + entry.Value);
        }

        // 输出 tile 类型的总数
        Debug.Log("Total unique tile types: " + tileTypeCounts.Count);
    }
}
