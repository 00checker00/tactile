using System.Collections.Generic;

[System.Serializable]
public class Motives {
    public List<Motive> motives;
}

[System.Serializable]
public class Cell {
    public string cellid; 
    public List<int> cellcolor;
}

[System.Serializable]
public class Brick    {
    public string brickid;
    public List<int> brickcolor;
    public int width;
    public int height;
}

[System.Serializable]
public class Motive {
    public string name;
    public int level;
    public List<Cell> cells;
    public List<Brick> bricks;
}
