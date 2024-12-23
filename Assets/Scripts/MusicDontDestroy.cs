using UnityEngine;

public class MusicDontDestroy : MonoBehaviour
{
    private static MusicDontDestroy Instance { get; set; }

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
