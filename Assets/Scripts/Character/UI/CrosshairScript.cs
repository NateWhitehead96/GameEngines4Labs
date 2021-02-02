using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairScript : InputMonoBehaviour
{
    public Vector2 mouseSensitivity;

    public bool Inverted = false;

    public Vector2 CurrentAimPos { get; private set; }

    [SerializeField, Range(0, 1)]
    private float CrosshairHorizontalPercentage = 0.25f;
    private float MaxHorizontalDeltaConstrain;
    private float MinHorizontalDeltaConstrain;
    private float HorizontalOffset;

    [SerializeField, Range(0, 1)]
    private float CrosshairVerticalPercentage = 0.25f;
    private float MaxVerticalDeltaConstrain;
    private float MinVerticalDeltaConstrain;
    private float VerticalOffset;

    private Vector2 CorsshairStartingPos;
    private Vector2 CurrentLookDelta;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.CursorActive)
        {
            AppEvents.Invoke_OnMousecursorEnable(false);
        }

        CorsshairStartingPos = new Vector2(Screen.width / 2f, Screen.height / 2f);

        HorizontalOffset = (Screen.width * CrosshairHorizontalPercentage) / 2;
        MinHorizontalDeltaConstrain = -(Screen.width / 2f) + HorizontalOffset;
        MaxHorizontalDeltaConstrain = (Screen.width / 2f) - HorizontalOffset;

        VerticalOffset = (Screen.height * CrosshairVerticalPercentage) / 2f;
        MinVerticalDeltaConstrain = -(Screen.height / 2f) + VerticalOffset;
        MaxVerticalDeltaConstrain = (Screen.height / 2f) + VerticalOffset;

    }



    private new void OnEnable()
    {
        base.OnEnable();
        GameInput.ThirdPerson.Look.performed += OnLook;
    }

    private void OnLook(InputAction.CallbackContext delta)
    {
        Vector2 mouseDelta = delta.ReadValue<Vector2>();

        CurrentLookDelta.x += mouseDelta.x * mouseSensitivity.x;
        if (CurrentLookDelta.x >= MaxHorizontalDeltaConstrain || CurrentLookDelta.x <= MinHorizontalDeltaConstrain)
        {
            CurrentLookDelta.x -= mouseDelta.x * mouseSensitivity.x;
        }
        CurrentLookDelta.y += mouseDelta.y * mouseSensitivity.y;
        if (CurrentLookDelta.y >= MaxVerticalDeltaConstrain || CurrentLookDelta.y <= MinVerticalDeltaConstrain)
        {
            CurrentLookDelta.y -= mouseDelta.y * mouseSensitivity.y;
        }

    }

    private new void OnDisable()
    {
        base.OnDisable();
        GameInput.ThirdPerson.Look.performed -= OnLook;
    }

    private void Update()
    {
        float crosshairXPos = CorsshairStartingPos.x + CurrentLookDelta.x;
        float crosshairYPos = Inverted ? CorsshairStartingPos.y - CurrentLookDelta.y : CorsshairStartingPos.y + CurrentLookDelta.y;
        CurrentAimPos = new Vector2(crosshairXPos, crosshairYPos);

        transform.position = CurrentAimPos;

    }
}
