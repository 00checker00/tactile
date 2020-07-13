using UnityEngine;
     
public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile; 

    public Motives readJSONMotives()
    {
        jsonFile = Resources.Load<TextAsset> ("motives");
        Motives motivesInJson = JsonUtility.FromJson<Motives>(jsonFile.text);
        return motivesInJson;

        /*
        Debug.Log("motivesInJson: " + motivesInJson);
        foreach (Motive motive in motivesInJson.motives) {
            Debug.Log("Motive: " + motive.name + ", Level: " + motive.level);
            foreach(Cell cell in motive.cells) {
                foreach(string cellid in cell.cellids) 
                    Debug.Log("CellID: " +  cellid);
                foreach(int colorValue in cell.cellcolor)
                    Debug.Log("Color: " + colorValue);
            }
            foreach(Brick brick in motive.bricks) {
                Debug.Log("BrickID: " + brick.brickid + "width: " + brick.width + "height: " + brick.height);
                foreach(int color in brick.brickcolor) 
                    Debug.Log("Color: " + color);
            }
        }*/
    }
}

