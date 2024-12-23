using Cinemachine;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        FindObjectOfType<LevelGenerator>().levelSpeed = 1.0f;

        var vCam = FindObjectOfType<CinemachineVirtualCamera>();
        var noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_FrequencyGain = 0.43f;

        FindObjectOfType<FoxController>().gameObject.SetActive(false);
        GameOverCanvas.Instant();
    }
}
