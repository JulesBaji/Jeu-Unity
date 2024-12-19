using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject prefab; // Référence au prefab à instancier
    public float distanceFromCamera = 2f; // Distance devant la caméra

    private float currentThrowForce = 0f; // Force actuelle en accumulation
    public float maxThrowForce = 20f; // Force maximale du lancer
    public float throwForceRate = 5f; // Vitesse d'accumulation de la force

    void Update()
    {
        // Si le clic gauche est maintenu, on charge la force
        if (Input.GetMouseButton(0))
        {
            ChargeThrow();
        }

        // Appuyer sur le clic gauche pour instancier et lancer
        if (Input.GetMouseButtonUp(0))
        {
            InstantiateAndThrow();
        }
    }

    void ChargeThrow()
    {
        // Accumuler la force, mais ne pas dépasser la force maximale
        if (currentThrowForce < maxThrowForce)
        {
            currentThrowForce += throwForceRate * Time.deltaTime; // Accumulation de la force
        }
    }

    void InstantiateAndThrow()
    {
        // Obtenir la position de la caméra
        Camera mainCamera = Camera.main;

        if (mainCamera != null && prefab != null)
        {
            // Calculer la position devant la caméra
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

            // Instancier le caillou
            GameObject caillouInstance = Instantiate(prefab, spawnPosition, mainCamera.transform.rotation);

            // Appliquer la force immédiatement
            Rigidbody rb = caillouInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Appliquer la force accumulée dans la direction de la caméra
                Vector3 throwDirection = mainCamera.transform.forward;
                rb.AddForce(throwDirection * currentThrowForce, ForceMode.VelocityChange);
            }

            // Réinitialiser la force pour le prochain lancer
            currentThrowForce = 0f;
        }
        else
        {
            Debug.LogError("Aucun prefab assigné ou caméra principale introuvable !");
        }
    }
}
