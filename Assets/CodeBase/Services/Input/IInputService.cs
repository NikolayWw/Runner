namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        float MoveAxis { get; }
        bool TouchScreenPress { get; }
    }
}