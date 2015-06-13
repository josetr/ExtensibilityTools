﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using MadsKristensen.ExtensibilityTools.Settings;
using MadsKristensen.ExtensibilityTools.VSCT.Commands;
using MadsKristensen.ExtensibilityTools.VSCT.Generator;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace MadsKristensen.ExtensibilityTools
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Version, IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideOptionPage(typeof(ExtensibilityOptions), "Extensibility Tools", "General", 101, 101, true, new[] { "pkgdef", "vsct" })]
    [ProvideCodeGenerator(typeof(VsctCodeGenerator), VsctCodeGenerator.GeneratorName, VsctCodeGenerator.GeneratorDescription, true, ProjectSystem = ProvideCodeGeneratorAttribute.CSharpProjectGuid)]
    [ProvideCodeGenerator(typeof(VsctCodeGenerator), VsctCodeGenerator.GeneratorName, VsctCodeGenerator.GeneratorDescription, true, ProjectSystem = ProvideCodeGeneratorAttribute.VisualBasicProjectGuid)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [Guid(GuidList.guidExtensibilityToolsPkgString)]
    public sealed class ExtensibilityToolsPackage : Package
    {
        public const string Version = "0.1";
        public static ExtensibilityOptions Options;

        protected override void Initialize()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
            {
                Options = (ExtensibilityOptions)GetDialogPage(typeof(ExtensibilityOptions));

            }), DispatcherPriority.ApplicationIdle, null);

            AddCustomToolCommand.Initialize(this);
            SignBinaryCommand.Initialize(this);

            base.Initialize();
        }
    }
}
