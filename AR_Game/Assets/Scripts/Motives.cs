using System.Collections.Generic;

[System.Serializable]
public class Motives {
    public List<Motive> motives;
}

[System.Serializable]
public class Cellcolor {
    public int r;
    public int g;
    public int b;
}

[System.Serializable]
public class Cell {
    public string id;
    public List<Cellcolor> cellcolor;
}

[System.Serializable]
public class Motive {
    public string name;
    public int level;
    public List<Cell> cells;
}
