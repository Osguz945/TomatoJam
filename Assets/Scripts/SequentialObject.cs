using UnityEngine;

public class SequentialObject : MonoBehaviour
{
    // Una referencia al controlador para poder llamarlo cuando colisione
    private SequenceController controller;

    // Se llama automáticamente cuando se agrega el script o en el Inspector
    public void SetController(SequenceController seqController)
    {
        controller = seqController;
    }

    // Usamos OnTriggerEnter para colisiones con triggers (más limpio)
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto con el que colisionamos tiene el Tag "Tomate"
        if (other.CompareTag("Tomate"))
        {
            // 1. Deshabilitar inmediatamente este objeto
            gameObject.SetActive(false);

            // 2. Notificar al controlador que pase al siguiente objeto
            if (controller != null)
            {
                controller.ObjetoDeshabilitado();
            }
        }
    }
}