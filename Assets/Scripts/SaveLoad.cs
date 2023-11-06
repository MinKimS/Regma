using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public Vector3 PlayerRespawnPosition;
}

public class SaveLoad : MonoBehaviour
{
    private SaveData savedata = new SaveData();

    private string Save_Data_Directory;
    private string Save_Data_File = "/SaveFile.txt";
    private RespawnManager thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        Save_Data_Directory = Application.dataPath + "/Saves/";
        if(!Directory.Exists(Save_Data_Directory))
            Directory.CreateDirectory(Save_Data_Directory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SaveData()
    {
        thePlayer = FindObjectOfType<RespawnManager>();

        if (thePlayer != null)
        {
            savedata.PlayerRespawnPosition = thePlayer.transform.position;

            string json = JsonUtility.ToJson(savedata);
            File.WriteAllText(Save_Data_Directory + Save_Data_File, json);
            Debug.Log(json);
            Debug.Log("재시작");
        }
        else
        {
            Debug.LogWarning("RespawnManager를 찾을 수 없습니다.");
        }
    }


}
