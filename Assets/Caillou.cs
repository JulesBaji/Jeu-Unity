using UnityEngine;

public class Caillou : MonoBehaviour
{
    public float throwForce = 10f; // Force de lancement
    private Camera playerCamera;
    private Vector3 throwDirection;

    void Start()
    {
        // Référence à la caméra principale
        playerCamera = Camera.main;
        if (playerCamera != null)
        {
            throwDirection = playerCamera.transform.forward; // Direction du lancement
        }
    }

    public void Throw()
    {
        // Appliquer la force sur le Rigidbody du caillou
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        }
    }

    // Détecter la collision avec le sol
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Vérifie si la collision est avec le sol
        {
            Destroy(gameObject); // Détruire le caillou
        }
    }
}

