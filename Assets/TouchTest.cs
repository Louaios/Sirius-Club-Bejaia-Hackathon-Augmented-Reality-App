using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchTest : MonoBehaviour
{
    public GameObject plasticpopup;
    public GameObject glasspopup;
    public GameObject metalpopup;
    private bool plasticbool = true;
    private bool glassbool = true;
    private bool metalbool = true;

    public TMP_Text textMeshPro;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

            // Check if the touch phase is "Began" (touch just started)
            if (Input.GetMouseButtonDown(0))
            {
                // Create a ray from the camera to the touch position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Perform a raycast
                if (Physics.Raycast(ray, out hit, 100))
                {
                    // Print the name and world position of the object hit by the raycast
                    Debug.Log("Clicked on object: " + hit.transform.name);
                    Debug.Log("World position: " + hit.point); // World position of the hit point

                    // Check if the object hit is plastic, glass, or metal and handle accordingly
                    HandleObjectHit(hit);
                }
            }
        
    }

    // Method to handle the object hit by the raycast
    private void HandleObjectHit(RaycastHit hit)
    {
        // Convert the world position to a string format
        string positionString = $"Position: {hit.point.ToString("F2")}";

        // Check if the object hit is plastic
        if (hit.transform.tag == "plastic" && plasticbool)
        {
            // Update UI for plastic
            UpdateUI(hit, plasticpopup, $"Plastic, Recyclable!\n{positionString}");
            plasticbool = false;
            ResetConditionVariablesExcept("plastic");
        }
        // Check if the object hit is glass
        else if (hit.transform.tag == "glass" && glassbool)
        {
            // Update UI for glass
            UpdateUI(hit, glasspopup, $"Glass, Recyclable!\n{positionString}");
            glassbool = false;
            ResetConditionVariablesExcept("glass");
        }
        // Check if the object hit is metal
        else if (hit.transform.tag == "metal" && metalbool)
        {
            // Update UI for metal
            UpdateUI(hit, metalpopup, $"Metal, Recyclable!\n{positionString}");
            metalbool = false;
            ResetConditionVariablesExcept("metal");
        }

        // Destroy information popups and reset the corresponding condition variables
        if (hit.transform.tag == "Info")
        {
            Destroy(hit.transform.gameObject);
            plasticbool = true;
        }
        else if (hit.transform.tag == "glassInfo")
        {
            Destroy(hit.transform.gameObject);
            glassbool = true;
        }
        else if (hit.transform.tag == "metalInfo")
        {
            Destroy(hit.transform.gameObject);
            metalbool = true;
        }
    }

    // Method to update the UI based on the object clicked
    private void UpdateUI(RaycastHit hit, GameObject popup, string message)
    {
        // Instantiate the popup at the object's position with an offset
        Vector3 pos = hit.transform.position;
        pos.z += 0.25f;
        pos.y += 0.25f;
        Instantiate(popup, pos, transform.rotation);

        // Update the UI text
        textMeshPro.text = message;
    }

    // Method to reset the condition variables except for the current one
    private void ResetConditionVariablesExcept(string currentType)
    {
        if (currentType != "plastic")
        {
            plasticbool = true;
        }
        if (currentType != "glass")
        {
            glassbool = true;
        }
        if (currentType != "metal")
        {
            metalbool = true;
        }
    }
}
