namespace WebApplication1.Services
{
    public class FooService : IIncrementalService
    {
        private int _number;

        public int Number { get { return _number; } }

        public FooService()
        {

        }

        public void Increment()
        {
            _number = _number + 1;
        }
    }


}
