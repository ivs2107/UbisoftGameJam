using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera cmFreeCam;
    public IEnumerator Shake(float duration, float magnetude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnetude;
            float y = Random.Range(-1f, 1f) * magnetude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }


        transform.localPosition = originalPos;
    }

    public IEnumerator ProcessShake(float shakeIntensity = 5f, float shakeTiming = 0.5f, float amplitudeGain=0.5f)
    {
        Noise(0.5f, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeGain;
        cmFreeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequencyGain;
        //cmFreeCam.m_AmplitudeGain = amplitudeGain;
        // cmFreeCam.middleRig.Noise.m_AmplitudeGain = amplitudeGain;
        //cmFreeCam.bottomRig.Noise.m_AmplitudeGain = amplitudeGain;

        //cmFreeCam.m_FrequencyGain = frequencyGain;
        //cmFreeCam.middleRig.Noise.m_FrequencyGain = frequencyGain;
        // cmFreeCam.bottomRig.Noise.m_FrequencyGain = frequencyGain;

    }

}
