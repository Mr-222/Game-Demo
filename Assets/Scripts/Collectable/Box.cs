using UnityEngine;
using System;

public class Box : MonoBehaviour, ICollectible
{
    public static event Action OnCoinCollected;
    
    public void Collect()
    {
        Destroy(this.gameObject);
        OnCoinCollected?.Invoke();
    }
}

