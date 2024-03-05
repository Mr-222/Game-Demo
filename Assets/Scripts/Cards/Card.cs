using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    string cardName;
    
    void Start()
    {
        var tmp = gameObject.GetComponentInChildren<TextMeshPro>();
        cardName = tmp.text;
    }
}
