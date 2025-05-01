
public class Character
{
    private float Health;
    public float health
    {
        get { return Health; }
        set {
            if (value < 0)
            {
                Health = 0;
            }
            else
            {
                Health = value;
            }
        }
    }
}
    
