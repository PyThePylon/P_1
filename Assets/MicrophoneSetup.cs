using UnityEngine;

public class MicrophoneSetup : MonoBehaviour
{
    public string microphoneDevice;
    private AudioClip micClip;
    private bool micInitialized = false;

    [Range(0, 1)]
    public float volumeThreshold = 0.02f; 
    private float[] audioData = new float[256];

    public AIController aiController;

    private float noiseCooldown = 2f;
    private float lastNoiseTime = -Mathf.Infinity;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneDevice = Microphone.devices[0];
            micClip = Microphone.Start(microphoneDevice, true, 1, 44100);
            micInitialized = true;
            Debug.Log("Microphone initialized: " + microphoneDevice);
        }
        else
        {
            Debug.LogError("No microphone detected!");
        }
    }

    void Update()
    {
        if (micInitialized && Microphone.IsRecording(microphoneDevice))
        {
            micClip.GetData(audioData, 0);

            float maxVolume = 0;
            foreach (float sample in audioData)
            {
                if (Mathf.Abs(sample) > maxVolume)
                {
                    maxVolume = Mathf.Abs(sample);
                }
            }

            if (maxVolume > volumeThreshold)
            {
                NotifyAI();
            }
        }
    }

    void NotifyAI()
    {
        if (Time.time >= lastNoiseTime + noiseCooldown && aiController != null)
        {
            Vector3 noisePosition = Camera.main.transform.position;
            aiController.lastKnownPlayerPosition = noisePosition;
            aiController.SwitchState(aiController.searchingState);
            lastNoiseTime = Time.time;
        }
    }

    void OnDestroy()
    {
        if (micInitialized)
        {
            Microphone.End(microphoneDevice);
        }
    }
}
