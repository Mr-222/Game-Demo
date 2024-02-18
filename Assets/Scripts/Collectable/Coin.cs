using UnityEngine;
using System;

public class Coin : MonoBehaviour, ICollectible
{
    public static event Action OnCoinCollected;
    
    public void Collect()
    {
        Debug.Log("Coin Collected");
        Destroy(this.gameObject);
        OnCoinCollected?.Invoke();
    }
}
