using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Immutability
{
    internal class Mutability
    {
        public void ShowMutability()
        {
            ImmutableField immutableField = new ImmutableField();
            MutableField mutableField = new MutableField();

            var surface = Task.Run(() =>
            {
                var circle = mutableField.PI * 10 * 10;
                mutableField.PI = 10;
                var square = mutableField.PI * mutableField.PI;
            });

            var length = Task.Run(() =>
            {
                var lengthValue = mutableField.PI * 20;
            });

            var surfaceImmutable = Task.Run(() =>
            {
                var circleSurface = immutableField.Immutabile_PI * 10 * 10;
            });


            Task.WaitAll(surface, length, surfaceImmutable);
        }
    }
}