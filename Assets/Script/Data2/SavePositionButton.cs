using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SavePositionButton : MonoBehaviour
{
   
    public Button saveButton; // Reference to the Button component
    public Transform objectToSave; // Reference to the object whose position you want to save

    private void Start()
    {

        saveButton.onClick.AddListener(() =>
        {
            SaveObjectPosition();
            SaveScenes();
        });
    }

    private void SaveObjectPosition()
    {
        PlayerPrefs.SetFloat("SavedPositionX", objectToSave.position.x);
        PlayerPrefs.SetFloat("SavedPositionY", objectToSave.position.y);
    }
    

    [System. Serializable]
    public class SceneData
    {
        public string sceneName;
        public bool isSceneActive;
    }

    public List<SceneData> savedScenes = new List<SceneData>();

    public void SaveScenes()
    {
        savedScenes.Clear();

        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            SceneData data = new SceneData
            {
                sceneName = scene.name,
                isSceneActive = scene.isLoaded
            };

            savedScenes.Add(data);
        }

        string json = JsonUtility.ToJson(this, true);
        string filepath = Application.persistentDataPath + "/SceneData.json";
        Debug.Log(filepath);
        File.WriteAllText(filepath, json);
        Debug.Log("Scenes saved to sceneData.json");
    }

}
