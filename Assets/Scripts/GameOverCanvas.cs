using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
    public static void Instant()
    {
        Instantiate(Resources.Load<GameObject>("GameOverCanvas"));
    }

    public void Restart()
    {
        SceneManager.LoadScene("game");
    }
}
