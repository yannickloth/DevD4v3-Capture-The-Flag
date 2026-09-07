namespace SampSharp
{
    [ChangeDriversAttribute(ChangeDriver.Ecs, ChangeDriver.Hosting)]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("SampSharp.SourceGenerator", "1.0.0.0")]
    public static class Entrypoint
    {
        [ChangeDriversAttribute(ChangeDriver.Ecs, ChangeDriver.Hosting)]
        private static readonly global::CTF.Host.Ecs.Startup _startup = new();

        [ChangeDriversAttribute(ChangeDriver.Ecs, ChangeDriver.Hosting)]
        private static SampSharp.OpenMp.Core.StartupContext _context;
        [ChangeDriversAttribute(ChangeDriver.Ecs, ChangeDriver.Hosting)]
        [global::System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute]
        public static void Initialize(SampSharp.OpenMp.Core.SampSharpInitParams inf)
        {
            _context = new SampSharp.OpenMp.Core.StartupContext(inf);
            _context.InitializeUsing(_startup);
        }

        [ChangeDriversAttribute(ChangeDriver.Ecs, ChangeDriver.Hosting)]
        public static void Main()
        {
            SampSharp.OpenMp.Core.StartupContext.MainInfoProvider();
        }
    }
}
