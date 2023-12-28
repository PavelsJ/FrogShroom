using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Menuscript : MonoBehaviour
{
    public Transform objectToSave;
    

    public void OnSettingsButton ()
    {
        SceneManager.LoadScene(2);
    }

    public void OnStartButton ()
    {
        SceneManager.LoadScene(3);
        
        TransformObjectToZero();
    }

    public void OnPlayButton ()
    {
        SceneManager.LoadScene(7);
        LoadScenes();
    }
    
    public void OnQuitButton ()
    {
        Application.Quit();
    }

    public void OnMenuButton ()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSkipButton()
    {
        SceneManager.LoadScene(4);

        TransformObjectToZero();
    }

    public void TransformObjectToZero()
    {
        GameObject.FindGameObjectWithTag("Frog").transform.position = new Vector2(0, 0);
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
    private HashSet<string> loadedScenes = new HashSet<string>();
    
    public void LoadScenes()
    {
        string filepath = Application.persistentDataPath + "/SceneData.json";
    
        if (File.Exists(filepath))
        {
            
            string json = File.ReadAllText(filepath);
            JsonUtility.FromJsonOverwrite(json, this);
            bool anySceneLoaded = false; // Track if at least one scene was loaded

            foreach (SceneData data in savedScenes)
            {
                Debug.Log("Scene Name: " + data.sceneName + ", Is Active: " + data.isSceneActive);

                if (data.isSceneActive && !loadedScenes.Contains(data.sceneName))
                { 
                    SceneManager.LoadScene(data.sceneName, LoadSceneMode.Additive);
                    loadedScenes.Add(data.sceneName);
                    anySceneLoaded = true;
                }
            }

            if (!anySceneLoaded)
            {
                Debug.LogWarning("No saved scenes to load.");
                
            }
        }
        else
        {
            Debug.LogWarning("sceneData.json not found. No scenes loaded.");
            SceneManager.LoadScene(9);
        }
    }
    
}
