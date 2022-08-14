public enum BoardSpaceType {
    CASTLE,
    DESERT,
    SIETCH
}

public class BoardSpace {
    public string Name {get;set;}
    public string Description {get;set;}
    public BoardSpaceType Type {get;set;}
    public BoardSpace Next;
    public BoardSpace? AltNext;
}
