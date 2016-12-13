using System.Xml;
public class Movement_Card : Card
{
    //Amount extra or fewer spaces that you can move
    public int movementAmt = 0;
    //0 Means player will engage in combat when landing on a zombie,
    //1 Means Player will not engage
    public int evade = 0;
    //However many spaces a player can push another player
    public int pushAmt = 0;
    //Certain cards decrease this in exchange for other benefits
    public int accuracy = 0;
    //0 Means can move in any direction, 1 means can only move in 1 direction
    public int fixedDirection = 0;
}