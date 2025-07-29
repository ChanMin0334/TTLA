[System.Serializable]
public class Stats
{
    public float Atk;
    public float Hp;
    public float AtkSpeed;
    public float Speed;
    public float GetStatValue(string type)
    {
        switch (type)
        {
            case "Atk":
                return Atk;
            case "Hp":
                return Hp;
            case "AtkSpeed":
                return AtkSpeed;
            case "Speed":
                return Speed;
            default:
                UnityEngine.Debug.LogWarning("Unknown stat type: " + type);
                return 0f;
        }
    }
}
