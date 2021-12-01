using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGraphicsHandler : MonoBehaviour {
    public Texture2D defaultCursor;
    public Texture2D reinforceCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Start() {
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

    public void StartReinforceCursor() {
        Cursor.SetCursor(reinforceCursor, hotSpot, cursorMode);
    }

    public void StopReinforceCursor() {
        Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

    /* public void TargetStart() {
         Cursor.visible = false;
     }

     public void TargetEnd() {
         Cursor.visible = true;
     }*/
}
