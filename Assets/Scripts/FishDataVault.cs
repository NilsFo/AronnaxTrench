using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDataVault : MonoBehaviour
{
    public readonly int FISH_ID_MAX = 4;

    public TextAsset fishDataCSV;
    private string[] rows;
    public List<Sprite> fishSprites;
    public List<Gradient> fishGradients;

    // Start is called before the first frame update
    void Start()
    {
        ReadData();
    }

    public void ReadData()
    {
        rows = fishDataCSV.text.Split('\n');
    }

    public string[] GetAllData(int id)
    {
        if (rows == null)
        {
            ReadData();
        }

        // print("RL: " + rows.Length + ". Req: "+id);
        string row = rows[id + 1];
        return row.Split(';');
    }

    public string Get(int id, int index)
    {
        string d = GetAllData(id)[index];
        //print(d);
        return d;
    }

    public string GetName(int id)
    {
        return GetAllData(id)[1];
    }

    public Sprite GetSprite(int id)
    {
        return fishSprites[id];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetMinDepth(int id)
    {
        return float.Parse(GetAllData(id)[2]);
    }

    public float GetMaxDepth(int id)
    {
        return float.Parse(GetAllData(id)[3]);
    }

    public List<int> GetAllFishForDepth(float depth)
    {
        List<int> ids = new List<int>();

        for (int i = 0; i < FISH_ID_MAX; i++)
        {
            float minDepth = GetMinDepth(i);
            float maxDepth = GetMaxDepth(i);
            if (depth >= minDepth && depth <= maxDepth)
            {
                for (int j = 0; j < GetRarity(i); j++)
                {
                    ids.Add(i);
                }
            }
        }

        return ids;
    }

    public int GetRarity(int id)
    {
        return int.Parse(GetAllData(id)[4]);
    }

    public float GetSpeedX(int id)
    {
        return float.Parse(GetAllData(id)[4]);
    }

}
