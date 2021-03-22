using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveSlotWidget : MonoBehaviour
{
    private string SaveName;

    private GameManager Manager;
    private LoadGameWidget loadWidget;

    [SerializeField] private TMP_Text SaveNameText;

    private void Awake()
    {
        Manager = GameManager.Instance;
    }

    public void Initialize(LoadGameWidget parentwidget, string saveName)
    {
        loadWidget = parentwidget;
        SaveName = saveName;
        SaveNameText.text = saveName;
    }

    public void SelectSave()
    {
        Manager.SetActiveSave(SaveName);
        loadWidget.LoadScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
