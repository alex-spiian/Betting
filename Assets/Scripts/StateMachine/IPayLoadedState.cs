public interface IPayLoadedState<TPayload> : IInitializable
{
    public void OnEnter(TPayload payload);
}