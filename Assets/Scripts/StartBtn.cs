using Cinemachine;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        FindObjectOfType<LevelGenerator>().levelSpeed = 13.0f;
        var CinemacinePoint = FindObjectOfType<CinemacinePoint>();
        CinemacinePoint.offset = new Vector3(0, 2.0f, -2.0f);
        CinemacinePoint.target.gameObject.SetActive(true);
        var vCam = FindObjectOfType<CinemachineVirtualCamera>();
        var noise =  vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_FrequencyGain = 4.0f;
    }

    private void Update()
    {
        if (transform.position.z < -8.0f)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
