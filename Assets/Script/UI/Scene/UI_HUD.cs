
public class UI_HUD : UI_Scene
{
    public UI_Conditions conditions;

    private void Awake()
    {
        conditions = Util.FindChild<UI_Conditions>(gameObject);
    }
}
