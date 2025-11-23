using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Puedes ajustar el tiempo máximo de vida del proyectil si no colisiona
    public float lifetime = 5f;

    void Start()
    {
        // Destruir el proyectil después de un tiempo si no colisiona con nada
        Destroy(gameObject, lifetime);
    }

    // Se llama cuando el collider del proyectil toca otro collider
    private void OnCollisionEnter(Collision collision)
    {
        // ¡Destruir el propio proyectil inmediatamente al colisionar con CUALQUIER COSA!
        Destroy(gameObject);
    }
}