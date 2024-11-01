using UnityEngine;
using UnityEngine.SceneManagement;  // 需要这个命名空间来加载场景

public class LevelButtonController : MonoBehaviour
{
    // Level 1 按钮的点击事件
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");  // 确保场景名称与项目中 Level1 场景的名称一致
    }
}
