using System.Xml;
public class Attack_Card : Card
{
    //Divide this by 100 to get percentage
    public int accuracy = 0;
    //If 1 hit adjacent if 0 don't
    public int adjacent = 0;
    //How many times it can return to the hand, decrease by 1 every time it is played
    public int numReturns = 0;
    //If 0, the player continues the egnagement phase
    //If 1, the player leaves the engagement phase and continues movement
    public int leaveEngagement = 0;
    //How many extra times the player can attack before the zombie
    public int priorityAttack = 0;
}
