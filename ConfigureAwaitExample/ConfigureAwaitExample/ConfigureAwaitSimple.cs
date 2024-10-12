using System.Threading.Tasks;

namespace ConfigureAwaitExample
{
    public class ConfigureAwaitSimple
    {
        public async Task SumNumbers()
        {
            int sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                sum += i;
                await Task.Delay(10).ConfigureAwait(false);
            }
        }
        
        public async Task RunExample()
        {
            await Task.Run(() => SumNumbers()).ConfigureAwait(false);
        }
        
    }
}