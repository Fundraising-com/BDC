using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.UnityExtensions;


namespace TestApplication
{
    class Bootstrapper : UnityBootstrapper
    {

        protected override DependencyObject CreateShell()
        {
            Shell view = Container.TryResolve<Shell>();
            // Application.Current.RootVisual = (Window)view;

            return view;
        }
        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
        }


    }
}
