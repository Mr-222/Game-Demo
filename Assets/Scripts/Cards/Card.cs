using TMPro;
using UnityEngine;

public enum CardType
{
    Fire,
    Ice,
    Laser,
    Meteorite 
}

public class Card : MonoBehaviour
{
    private Texture2D mainTex;
    private CardType cardType;
    
    public CardType type
    {
        get { return cardType; }
        private set { cardType = value; }
    }
    
    public void Init(CardType type, Texture2D tex)
    {
        mainTex = tex;
        this.type = type;
    }

    public void UpdateFace()
    {
        GetComponentInChildren<TextMeshPro>().text = type.ToString();
        GetComponentInChildren<Renderer>().materials[1].mainTexture = mainTex;
    } 
}
