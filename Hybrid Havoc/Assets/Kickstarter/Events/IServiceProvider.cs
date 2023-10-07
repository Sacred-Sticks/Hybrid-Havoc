using System;

namespace Kickstarter.Events
{
    public interface IServiceProvider
    {
        public void ImplementService(EventArgs args);
    }
}
