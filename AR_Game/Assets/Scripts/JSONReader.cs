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
            Debug.Log("Motive: " + motive.name + ", Level: " + motive.level);
            foreach(Cell cell in motive.cells) {
                Debug.Log("Found CellID: " + cell.cellid);
                foreach(int color in cell.cellcolor) 
                    Debug.Log("Color: " + color);
            }
            foreach(Brick brick in motive.bricks) {
                Debug.Log("BrickID: " + brick.brickid + "width: " + brick.width + "height: " + brick.height);
                foreach(int color in brick.brickcolor) 
                    Debug.Log("Color: " + color);
            }
        }
    }
}

