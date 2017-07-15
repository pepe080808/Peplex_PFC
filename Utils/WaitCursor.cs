using System;
using System.Windows.Input;

namespace Utils
{
    public class WaitCursor : IDisposable
    {
        private readonly Cursor _previousCursor;

        public WaitCursor()
        {
            _previousCursor = Mouse.OverrideCursor;

            Mouse.OverrideCursor = Cursors.Wait;
        }

        public void Dispose()
        {
            Reset();
        }

        public void Reset()
        {
            Mouse.OverrideCursor = _previousCursor;
        }
    }
}
