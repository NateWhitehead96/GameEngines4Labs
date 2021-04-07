using UnityEngine;

public abstract class GameHUDWidget : MonoBehaviour
{
    public void EnableWidget()
    {
        gameObject.SetActive(true);
    }

    public void DisableWidget()
    {
        gameObject.SetActive(false);
    }
}