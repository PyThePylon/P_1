using UnityEngine;

public class Throwables : MonoBehaviour
{
    public GameObject throwablePrefab; 
    public Transform throwPoint; 
    public float throwForce = 10f; 
    public float noiseRadius = 5f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Throw();
        }
    }

    void Throw()
    {
        GameObject throwable = Instantiate(throwablePrefab, throwPoint.position, throwPoint.rotation);

        Rigidbody rb = throwable.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }

        NoiseMaker noiseMaker = throwable.AddComponent<NoiseMaker>();
        noiseMaker.noiseRadius = noiseRadius;
    }
}
