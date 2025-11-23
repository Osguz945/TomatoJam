using UnityEngine;
using UnityEngine.UI; // Necesario para acceder a los componentes de la interfaz de usuario (Text, Image, etc.)

public class InteractButton : MonoBehaviour
{
    [Header("Configuración del Jugador y Distancia")]
    // Referencia al objeto del jugador (debe ser el objeto padre, el 'cuerpo')
    public Transform playerTransform;

    public Transform ticketPosition;
    // Distancia máxima a la que el texto será visible
    public float activationDistance = 3f;

    [Header("Componente de la Interfaz de Usuario")]
    // Referencia al componente del Canvas (Text o TextMeshPro) que queremos mostrar
    public GameObject textObject;

    void Start()
    {
        // Asegúrate de que el texto está oculto al inicio del juego
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
        else
        {
            Debug.LogError("El objeto de texto (textObject) no está asignado en el Inspector.");
        }

        // Si la referencia al jugador está vacía, intenta buscar el objeto etiquetado como "Player"
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("El objeto del jugador no está asignado y no se encontró ningún objeto con la etiqueta 'Player'.");
            }
        }
    }

    void Update()
    {
        // Si no tenemos el jugador, no podemos calcular la distancia
        if (playerTransform == null) return;

        // 1. Calcular la distancia entre este objeto vacío y el jugador
        float distance = Vector3.Distance(ticketPosition.position, playerTransform.position);

        // 2. Comprobar si la distancia es menor o igual a la distancia de activación
        if (distance <= activationDistance)
        {
            // El jugador está cerca: activamos el texto
            if (!textObject.activeSelf) // Solo lo activamos si está inactivo para ahorrar rendimiento
            {
                textObject.SetActive(true);
                // Opcional: Puedes añadir aquí código para un efecto de "fade-in"
            }
        }
        else
        {
            // El jugador está lejos: desactivamos el texto
            if (textObject.activeSelf) // Solo lo desactivamos si está activo
            {
                textObject.SetActive(false);
                // Opcional: Puedes añadir aquí código para un efecto de "fade-out"
            }
        }
    }
}