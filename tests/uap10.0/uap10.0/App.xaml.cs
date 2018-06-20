using System;
using System.Reflection;
using Windows.ApplicationModel.Activation;

namespace uap10_0
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args.Kind != ActivationKind.Launch)
            {
                throw new NotSupportedException($"Activation kind {args.Kind} not supported.");
            }

            base.OnActivated(args);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            new NUnitLite.AutoRun(typeof(App).GetTypeInfo().Assembly).Execute(Array.Empty<string>());
        }
    }
}
