using UnityEngine;

public class JumpSfx : MonoBehaviour
{
    public static void Instant()
    {
        var instance = Instantiate(Resources.Load<GameObject>("jumpSfx"));
        Destroy(instance, 1.0f);
    }
}
