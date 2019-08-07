using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadableObject : Interactable
{
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public string dialogToBeDisplayed;
    


    // Start is called before the first frame update
    void Start()
    {
        dialogText.text = dialogToBeDisplayed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerInRange && PlayerMovement.isFacingUp)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerInRange = false;
            context.Raise();
            dialogBox.SetActive(false);
        }
    }
}
