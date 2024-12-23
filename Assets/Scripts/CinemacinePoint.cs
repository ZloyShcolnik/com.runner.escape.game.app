using UnityEngine;

public class CinemacinePoint : MonoBehaviour
{
    public FoxController target;
    public Vector3 offset;

    private void Update()
    {
        transform.position = new Vector3(0, 0, target.transform.position.z) + offset;
    }
}
