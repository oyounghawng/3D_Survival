public class PlayerManager
{
    public PlayerBehaviour playerBehaviour;
    private PlayerInventory playerInventory;

    public PlayerInventory Inventory 
    {
        get { return playerInventory; } 
        set { playerInventory = value; } 
    }


}