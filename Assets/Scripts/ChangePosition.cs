using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangePosition : MonoBehaviour
{
    
    public GameObject position1;

    public TMPro.TextMeshProUGUI ticketText;

    private Movement movementScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementScript = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && ticketText.gameObject.activeSelf)
        { 
            transform.position = position1.transform.position;
            movementScript.isMoveable = false;
        }

    }
}
