using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; 
    public Vector2 hotspot = Vector2.zero; 

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }
}