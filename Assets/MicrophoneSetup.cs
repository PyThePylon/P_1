using UnityEngine;

public class MicrophoneSetup : MonoBehaviour
{
    public string microphoneDevice; 
    private AudioClip micClip;      
    private bool micInitialized = false;

    [Range(0, 1)]
    public float volumeThreshold = 0.1f; 
    private float[] audioData = new float[256]; 

    public GameObject movingCube; 
    public float moveSpeed = 3f;  

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
                MoveCubeTowardsPlayer();
            }
            else
            {
                StopCubeMovement();
            }
        }
    }

    void MoveCubeTowardsPlayer()
    {
        if (movingCube != null)
        {
            Vector3 playerPosition = Camera.main.transform.position;

            movingCube.transform.position = Vector3.MoveTowards(movingCube.transform.position, playerPosition, moveSpeed * Time.deltaTime);
        }
    }

    void StopCubeMovement()
    {
 
    }

    void OnDestroy()
    {
        if (micInitialized)
        {
            Microphone.End(microphoneDevice);
        }
    }
}
