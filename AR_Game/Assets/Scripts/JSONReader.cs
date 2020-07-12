using UnityEngine;
     
public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile; 
 
    void Start()
    {
        jsonFile = Resources.Load<TextAsset> ("motives");
        Motives motivesInJson = JsonUtility.FromJson<Motives>(jsonFile.text);
        Debug.Log("motivesInJson: " + motivesInJson);
        foreach (Motive motive in motivesInJson.motives) {
            Debug.Log("Found motive: " + motive.name + ", level: " + motive.level + ", cell: " + motive.cells);
            foreach(Cell cell in motive.cells) {
                Debug.Log("Found cell id: " + cell.id);
                foreach(Cellcolor color in cell.cellcolor) {
                    Debug.Log("Color r: " + color.r);
                    Debug.Log("Color g: " + color.g);
                    Debug.Log("Color b: " + color.b);
                }
            }
        }
    }
}

