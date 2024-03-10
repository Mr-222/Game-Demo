using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    
    private Camera overlayCamera;
    private CardDeck deck;
    private List<Card> hand = new List<Card>(4);
    private int curr = -1;
    
    public UnityEvent<CardType> OnSkillTriggered;
    
    void Awake()
    {
        // Ensure there's only one CardManager instance (Singleton Pattern)
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // Don't destroy this object when loading new scenes
    }
    
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
            hand[i].transform.position = overlayCamera.ScreenToWorldPoint(
                new Vector3((i + 1) * Screen.width / 10 - Screen.width / 20, Screen.height / 8, 1)
            );
        }
    }

    private void TriggerCard(int index)
    {
        OnSkillTriggered.Invoke(hand[index].type);
        
        // Other cards move left
        for (int i = index + 1; i < hand.Count; i++)
            hand[i].transform.position = overlayCamera.ScreenToWorldPoint(
                new Vector3(i * Screen.width / 10 - Screen.width / 20, Screen.height / 8, 1)
                );
        
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
        hand[hand.Count - 1].transform.position = overlayCamera.ScreenToWorldPoint(
            new Vector3(Screen.width / 10 * hand.Count - Screen.width / 20, Screen.height / 8, 1)
            );
    }

    public void CancelCard()
    {
        if (curr == -1)
            return;
        
        hand[curr].transform.position -= Vector3.up * .5f;
        curr = -1;
    }

    public void ChangeCurrSkill(int prev, int curr)
    {
        if (prev != -1)
            hand[prev].transform.position -= Vector3.up * .5f;
        this.curr = curr;
        hand[this.curr].transform.position += Vector3.up * .5f;
    }
    
    public void UseSkill()
    {
        TriggerCard(curr);
    }
}
