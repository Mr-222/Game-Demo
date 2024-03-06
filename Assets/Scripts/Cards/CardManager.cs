using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private Camera overlayCamera;
    private CardDeck deck;
    private List<Card> hand = new List<Card>(4);
    private int curr = 1;
    
    void Start()
    {
        overlayCamera = GameObject.Find("Overlay Camera").GetComponent<Camera>();
        
        deck = new CardDeck();
        deck.Init();
        
        for (int i = 0; i < 4; i++)
        {
            CardType type = deck.DrawOne();
            
            // Instantiate a prefab, then set its text and texture
            GameObject cardObj = Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
            Texture2D tex = Resources.Load<Texture2D>("Cards/" + type.ToString());
            hand.Add(cardObj.GetComponent<Card>());
            hand[i].Init(type, tex);
            hand[i].UpdateFace();
            // Put in the lower left corner of the screen
            hand[i].transform.position = overlayCamera.ScreenToWorldPoint(new Vector3(
                Screen.width / 20 + i * Screen.width / 10, Screen.height / 10, 1)
            );
        }
        hand[0].transform.position += Vector3.up * .5f;
    }

    private void Update()
    {
        // Change the selected card
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hand[curr - 1].transform.position -= Vector3.up * .5f; 
            curr = 1;
            hand[curr - 1].transform.position += Vector3.up * .5f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hand[curr - 1].transform.position -= Vector3.up * .5f; 
            curr = 2;
            hand[curr - 1].transform.position += Vector3.up * .5f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hand[curr - 1].transform.position -= Vector3.up * .5f; 
            curr = 3;
            hand[curr - 1].transform.position += Vector3.up * .5f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hand[curr - 1].transform.position -= Vector3.up * .5f; 
            curr = 4;
            hand[curr - 1].transform.position += Vector3.up * .5f;
        }
        
        // Trigger selected card
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            TriggerCard(curr - 1);
        }
    }

    private void TriggerCard(int index)
    {
        // Other cards move left
        for (int i = index + 1; i < hand.Count; i++)
            hand[i].transform.position = overlayCamera.ScreenToWorldPoint(new Vector3(80 + (i - 1) * 110, 80, 1));
        
        // Destroy selected card, insert it into the deck
        var selectedCard = hand[index];
        deck.AddCard(selectedCard.type);
        hand.RemoveAt(index);
        Destroy(selectedCard.gameObject);
            
        // Draw a new card from deck
        CardType type = deck.DrawOne();
        GameObject cardObj = Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
        Texture2D tex = Resources.Load<Texture2D>("Cards/" + type.ToString());
        hand.Add(cardObj.GetComponent<Card>());
        hand[hand.Count - 1].Init(type, tex);
        hand[hand.Count - 1].UpdateFace();
        hand[hand.Count - 1].transform.position = overlayCamera.ScreenToWorldPoint(new Vector3(80 + (hand.Count - 1) * 110, 80, 1));
        
        curr = 1;
        hand[0].transform.position += Vector3.up * .5f;
    }
}
