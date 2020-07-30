using UnityEngine;
     
public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile; 
    public Motives motivesInJson;

    void Awake() {
        Debug.Log("start");
        jsonFile = Resources.Load<TextAsset>("motives");
        motivesInJson = JsonUtility.FromJson<Motives>(jsonFile.text);
        Debug.Log("done");
        Debug.Log(motivesInJson);
    }
}

