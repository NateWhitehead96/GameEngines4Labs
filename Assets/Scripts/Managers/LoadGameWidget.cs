using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameWidget : MenuWidget
{
    [SerializeField] private bool Debug;
    private GameDataList GameData;

    [SerializeField] GameObject SaveSlotPrefab;
    [SerializeField] private RectTransform LoadItemPanel;
    [SerializeField] TMP_InputField NewGameInputfield;
    public string SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        if(Debug)
        {
            SaveDebugData();
        }
        WipeChildren();
        LoadGameData();
    }

    private void WipeChildren()
    {
        foreach (Transform saveSlot in LoadItemPanel)
        {
            Destroy(saveSlot.gameObject);
        }
        LoadItemPanel.DetachChildren();
    }

    private void SaveDebugData()
    {
        GameDataList dataList = new GameDataList();
        dataList.SaveFileNames.AddRange(new List<string> { "Save1", "Save2", "Save3" });
        PlayerPrefs.SetString("FileSaveData", JsonUtility.ToJson(dataList));
    }

    private void LoadGameData()
    {
        if (!PlayerPrefs.HasKey("FileSaveData")) return;

        string jsonString = PlayerPrefs.GetString("FileSaveData");
        GameData = JsonUtility.FromJson<GameDataList>(jsonString);

        if (GameData.SaveFileNames.Count <= 0) return;
        foreach (string saveName in GameData.SaveFileNames)
        {
            SaveSlotWidget widget = Instantiate(SaveSlotPrefab, LoadItemPanel).GetComponent<SaveSlotWidget>();
            widget.Initialize(this, saveName);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void CreateNewGame()
    {
        if (string.IsNullOrEmpty(NewGameInputfield.ToString())) return;
        GameManager.Instance.SetActiveSave(NewGameInputfield.ToString());
        SceneManager.LoadScene(SceneToLoad);
    }
}
[Serializable]
class GameDataList
{
    public List<string> SaveFileNames = new List<string>();
}
