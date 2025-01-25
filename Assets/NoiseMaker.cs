using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float noiseRadius = 5f; 
    public float noiseDuration = 2f; 
    public AudioClip impactSound;
    private bool hasMadeNoise = false; 

    void OnCollisionEnter(Collision collision)
    {
        if (!hasMadeNoise)
        {
            hasMadeNoise = true;

            Collider[] colliders = Physics.OverlapSphere(transform.position, noiseRadius);

            foreach (Collider col in colliders)
            {
                AIController ai = col.GetComponent<AIController>();
                if (ai != null)
                {
                    ai.lastKnownPlayerPosition = transform.position; 
                    ai.SwitchState(ai.searchingState); 
                }
            }

            if (impactSound != null)
            {
                AudioSource.PlayClipAtPoint(impactSound, transform.position);
            }

            Destroy(gameObject, noiseDuration);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, noiseRadius);
    }

}
