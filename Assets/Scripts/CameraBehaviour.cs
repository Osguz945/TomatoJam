using UnityEngine;

public class CameraBehavio : MonoBehaviour
{
    // Variables para la sensibilidad y los límites de rotación
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Referencia al cuerpo del jugador (el objeto que contiene la cámara)

    float xRotation = 0f; // Almacena la rotación vertical actual de la cámara

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla y lo oculta
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Obtener la entrada del ratón
        // Input.GetAxis("Mouse X") da el movimiento horizontal del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        // Input.GetAxis("Mouse Y") da el movimiento vertical del ratón
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 2. Rotación Vertical (Eje X de la cámara)

        // Acumula la rotación vertical. El signo es negativo porque 
        // mover el ratón hacia arriba (positivo en Y) debe inclinar la cámara hacia abajo (negativo en X)
        xRotation -= mouseY;

        // Clampa la rotación para que no se pueda girar 360 grados verticalmente (evita volteos de cabeza)
        xRotation = Mathf.Clamp(xRotation, -90f, 50f);

        // Aplica la rotación a la cámara (que es el objeto que tiene este script)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 3. Rotación Horizontal (Eje Y del jugador)

        // Rota el cuerpo del jugador horizontalmente
        // Esto permite que el jugador gire y se mueva en la dirección en que mira
        playerBody.Rotate(Vector3.up * mouseX);
    }
}