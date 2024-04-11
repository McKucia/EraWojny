using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    [SerializeField] Texture2D hoverCursorIcon;
    [SerializeField] Texture2D normalCursorIcon;

    public void MouseEnter()
    {
        Cursor.SetCursor(hoverCursorIcon, Vector2.zero, CursorMode.Auto);
    }

    public void MouseExit()
    {
        Cursor.SetCursor(normalCursorIcon, Vector2.zero, CursorMode.Auto);
    }

}