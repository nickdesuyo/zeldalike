using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room : MonoBehaviour
{
    public GameObject virtualCamera;
    public string roomName;
    public bool roomHasName;
    public GameObject roomNameBox;
    public TextMeshProUGUI roomNameBoxText;


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Activate enemies and breakables

            virtualCamera.SetActive(true);

            if (roomHasName)
            {
                StartCoroutine(DisplayRoomName());
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Deactivate enemies and breakables

            virtualCamera.SetActive(false);
            if (roomHasName)
            {
                //how to prevent overlapping of coroutines when leaving a named room right after entering it?
                StopCoroutine(DisplayRoomName());
            }

        }
    }

    private IEnumerator DisplayRoomName()
    {
        roomNameBox.SetActive(true);
        roomNameBoxText.text = roomName;
        roomNameBoxText.color = new Color(roomNameBoxText.color.r, roomNameBoxText.color.b, roomNameBoxText.color.g, 1);

        int totalVisibleCharacters = 0;

        while (totalVisibleCharacters <= roomNameBoxText.textInfo.characterCount)
        {
            roomNameBoxText.maxVisibleCharacters = totalVisibleCharacters;
            totalVisibleCharacters++;
            yield return new WaitForSeconds(.08f);
        }
        
        yield return new WaitForSeconds(1.2f);
        
        while (roomNameBoxText.color.a > 0)
        {
            roomNameBoxText.color = new Color(roomNameBoxText.color.r, roomNameBoxText.color.b, roomNameBoxText.color.g, roomNameBoxText.color.a - .1f);
            yield return new WaitForSeconds(.1f);
        }

        roomNameBox.SetActive(false);
    }

    

    
}
