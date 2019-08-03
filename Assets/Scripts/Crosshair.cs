using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairImage;

    void OnGUI()
    {
        if (crosshairImage != null)
        {
            Vector2 position = mousePosition();
            GUI.DrawTexture(new Rect(position.x, position.y, crosshairImage.width, crosshairImage.height), crosshairImage);
        }
    }

    private Vector2 screenCenter()
    {
        return new Vector2(
            (Screen.width / 2) - (crosshairImage.width / 2),
            (Screen.height / 2) - (crosshairImage.height / 2)
            );
    }

    private Vector2 mousePosition()
    {
        return new Vector2(
            (Screen.width - Input.mousePosition.x) - (crosshairImage.width / 2),
            (Screen.height - Input.mousePosition.y) - (crosshairImage.height / 2)
            );
    }
}
