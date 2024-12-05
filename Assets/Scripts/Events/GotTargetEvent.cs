using VitalRouter;

public struct GotTargetEvent : ICommand
{
    public ColorType ColorType { get; }
    public float Multiplier { get; }
    public float CurrentBet { get; }

    public GotTargetEvent(ColorType colorType, float multiplier, float currentBet)
    {
        CurrentBet = currentBet;
        ColorType = colorType;
        Multiplier = multiplier;
    }
}