using UnityEngine;

public class Target : MonoBehaviour
{
    public float minMoveSpeed = 0.5f; // Vitesse minimale de l'oscillation
    public float maxMoveSpeed = 1.5f; // Vitesse maximale de l'oscillation
    public float dropSpeed = 2f; // Vitesse de descente après impact

    private Vector3 startPosition; // Position initiale
    private float height; // Hauteur de la cible
    public bool isHit = false; // Indique si la cible a été touchée
    private Collider targetCollider;

    private float moveSpeed; // Vitesse de montée/descente spécifique à cette cible
    private float phaseOffset; // Décalage de phase aléatoire

    void Start()
    {
        startPosition = transform.position; // Sauvegarder la position initiale
        targetCollider = GetComponent<Collider>(); // Référence au collider

        // Calculer la hauteur de la cible
        if (targetCollider is BoxCollider boxCollider)
        {
            height = boxCollider.size.y * transform.localScale.y;
        }
        else
        {
            height = transform.localScale.y;
        }

        // Générer des valeurs aléatoires pour chaque cible
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed); // Vitesse unique
        phaseOffset = Random.Range(0f, Mathf.PI * 2f); // Décalage de phase unique
    }

    void Update()
    {
        if (!isHit)
        {
            // Mouvement continu avec décalage de phase unique
            float offset = Mathf.Sin(Time.time * moveSpeed + phaseOffset) * (height / 2);
            transform.position = new Vector3(startPosition.x, startPosition.y - (height / 2) - offset, startPosition.z);
        }
        else
        {
            // Descente sous le sol après impact
            Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y - height, startPosition.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, dropSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Caillou"))
        {
            // Désactiver le mouvement continu et les collisions
            isHit = true;
            targetCollider.enabled = false;
        }
    }
}
