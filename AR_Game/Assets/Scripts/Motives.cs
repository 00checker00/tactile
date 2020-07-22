using System.Collections.Generic;

// Klassen zum Auslesen der JSON-Datei in eine Datenstruktur

// Zellen des Grids mit der gleichen Farbe
[System.Serializable]
public class Cell {
    public List<string> cellids;
    public List<int> cellcolor;
}

// Eigenschaften der zu verwendenen Bausteine
[System.Serializable]
public class Brick {
    public string brickid;
    public List<int> brickcolor;
    public int width;
    public int height;
}

// Motiv mit Name, Level, Zellen im Grid und Bausteinen
[System.Serializable]
public class Motive {
    public string name;
    public int level;
    public List<Cell> cells;
    public List<Brick> bricks;
}

// Liste aller Motive 
[System.Serializable]
public class Motives {
    public List<Motive> motives;
}


