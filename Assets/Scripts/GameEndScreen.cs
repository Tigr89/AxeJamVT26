using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
