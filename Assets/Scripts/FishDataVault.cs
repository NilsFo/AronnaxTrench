using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class FishDataVault : MonoBehaviour
{
    public static readonly int FISH_ID_MAX = 10;

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
        if (rows == null || rows.Length == 0)
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
        return float.Parse(GetAllData(id)[2], CultureInfo.InvariantCulture);
    }

    public float GetMaxDepth(int id)
    {
        return float.Parse(GetAllData(id)[3], CultureInfo.InvariantCulture);
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
        return int.Parse(GetAllData(id)[4], CultureInfo.InvariantCulture);
    }

    public float GetSpeedX(int id)
    {
        return float.Parse(GetAllData(id)[5], CultureInfo.InvariantCulture);
    }

    public float GetAliveTime(int id)
    {
        return float.Parse(GetAllData(id)[9], CultureInfo.InvariantCulture);
    }

    public float GetMagnitudeY(int id)
    {
        return float.Parse(GetAllData(id)[8], CultureInfo.InvariantCulture);
    }

    public float GetMagnitudeX(int id)
    {
        string[] s =GetAllData(id);
        //print("s:" + s[7]);
        float f = float.Parse(s[7],CultureInfo.InvariantCulture);
        //print("f:" + f);
        return f;
    }

    public float GetJitterY(int id)
    {
        return float.Parse(GetAllData(id)[6], CultureInfo.InvariantCulture);
    }



}
