using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DeleteSaveButton : MonoBehaviour
{

    public CoinNew[] collectibleObjectsToRespawn;

    public void DeleteSavedPosition()
    {
        PlayerPrefs.DeleteKey("SavedPositionX");
        PlayerPrefs.DeleteKey("SavedPositionY");
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerCoin");
        PlayerPrefs.Save();

        string filepath = Application.persistentDataPath + "/SceneData.json";
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
            Debug.Log("SceneData.json has been deleted.");
        }
    }
}
