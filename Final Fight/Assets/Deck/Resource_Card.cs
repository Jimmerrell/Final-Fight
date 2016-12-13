using System.Xml;
public class Resource_Card : Card
{
    //Amt to inc/dec resources by
    public int resourceAmt = 0;
    //How many turns the card's effect will be active (if 0, it is permanent)
    //If this is negative, that indicates how many turns it takes to
    //initially go into effect
    public int turnsAffect = 0;
    //Num resources to steal from other players
    public int stealAmt = 0;
    //1 = Removes all resources from all players
    public int burnAll = 0;
    //0 = Player can play more cards
    //1 = Player can't play any more cards
    public int endTurn = 0;
    //0 = Don't draw
    //1 = Draw until full
    public int drawUntilFull = 0;
}
