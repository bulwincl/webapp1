namespace WebApplication1.Services
{
    public interface IIncrementalService
    {
        public int Number { get; }

        public void Increment();
    }
}
