using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public delegate void MouseCursorEnable(bool enabled);

    public static event MouseCursorEnable MouseCursorEnabled;

    public static void Invoke_OnMousecursorEnable(bool enabled)
    {
        MouseCursorEnabled?.Invoke(enabled);
    }
}
