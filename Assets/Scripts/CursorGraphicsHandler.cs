using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGraphicsHandler : MonoBehaviour {
    public Texture2D defaultCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Start() {
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

   /* public void TargetStart() {
        Cursor.visible = false;
    }

    public void TargetEnd() {
        Cursor.visible = true;
    }*/
}
