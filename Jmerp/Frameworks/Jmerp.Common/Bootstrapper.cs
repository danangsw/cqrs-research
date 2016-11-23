using Autofac;

namespace Jmerp.Commons
{
    public class Bootstrapper
    {
        protected static IContainer Container { get; private set; }

        protected static void SetAutofacContainer(IContainer container)
        {
            Container = container;
            IsBuilded = true;
        }

        public static bool IsBuilded { get; set; }

        public static T GetService<T>(string name = null)
        {
            return string.IsNullOrEmpty(name) ? Container.Resolve<T>() : Container.ResolveNamed<T>(name);
        }

        public static void Dispose()
        {
            Container.Dispose();
        }
    }
}
