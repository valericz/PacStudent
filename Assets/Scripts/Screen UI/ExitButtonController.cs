using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonController : MonoBehaviour
{
    // 退出按钮点击事件
    public void LoadStartScene()
    {
        Debug.Log("Button Pressed");
        SceneManager.LoadScene("StartScene");

    }
}
