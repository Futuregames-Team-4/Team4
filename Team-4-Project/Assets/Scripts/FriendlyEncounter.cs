using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyEncounter : MonoBehaviour
{
    [SerializeField]
    GameObject textObject;

    private void Awake()
    {
        textObject.SetActive(false);
    }
    public void ConversationStarter ()
    {
        textObject.SetActive (true);
    }

    public void EndConversation ()
    {
        Debug.Log("Leaving.");
        textObject.SetActive(false);
    }
}
