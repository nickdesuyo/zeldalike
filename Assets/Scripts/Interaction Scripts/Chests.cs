using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chests : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public SignalObject raiseItem;
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerInRange && PlayerMovement.isFacingUp)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }

        }
    }

    private void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.currentItem = contents;
        playerInventory.AddItem(contents);
        raiseItem.Raise();
        isOpen = true;
        context.Raise();
        anim.SetBool("chestOpened", true);
    }

    private void ChestAlreadyOpen()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
        playerInventory.currentItem = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            playerInRange = true;
            context.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger && !isOpen)
        {
            playerInRange = false;
            context.Raise();
        }
    }
}
