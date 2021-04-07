using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public static PauseManager Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UnPauseGame();
    }

    private void OnDestroy()
    {
        UnPauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        //Time.timeScale = 0;
        var pauseables = FindObjectsOfType<MonoBehaviour>().OfType<IPauseable>();

        foreach (IPauseable pausable in pauseables)
        {
            pausable.PauseMenu();
        }

        Time.timeScale = 0;
        AppEvents.Invoke_OnMousecursorEnable(true);
    }

    public void UnPauseGame()
    {
        var pauseables = FindObjectsOfType<MonoBehaviour>().OfType<IPauseable>();

        foreach (IPauseable pausable in pauseables)
        {
            pausable.UnPauseMenu();
        }

        Time.timeScale = 1;
        AppEvents.Invoke_OnMousecursorEnable(false);
    }
}

interface IPauseable
{
    void PauseMenu();
    void UnPauseMenu();
}
