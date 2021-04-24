using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDataVault : MonoBehaviour
{

    public TextAsset fishDataCSV;
    private string[] rows;

    // Start is called before the first frame update
    void Start()
    {
        rows = fishDataCSV.text.Split('\n');
    }

    public string[] GetAllData(int id)
    {
        if (rows == null)
        {
            rows = fishDataCSV.text.Split('\n');
        }
        return rows[id].Split(';');
    }

    public string GetName(int id)
    {
        return GetAllData(id)[1];
    }

    public Sprite GetSprite(int id)
    {
        // TODO implement
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
