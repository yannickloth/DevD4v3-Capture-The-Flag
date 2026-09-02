namespace SampSharp
{
    /// <remarks>Change drivers: CD-22 (hosting/deployment spec), CD-01 (open.mp/SampSharp platform API)</remarks>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("SampSharp.SourceGenerator", "1.0.0.0")]
    public static class Entrypoint
    {
        private static readonly global::CTF.Host.Startup _startup = new();
        private static SampSharp.OpenMp.Core.StartupContext _context;
        /// <remarks>Change drivers: CD-22 (hosting/deployment spec), CD-01 (open.mp/SampSharp platform API)</remarks>
        [global::System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute]
        public static void Initialize(SampSharp.OpenMp.Core.SampSharpInitParams inf)
        {
            _context = new SampSharp.OpenMp.Core.StartupContext(inf);
            _context.InitializeUsing(_startup);
        }

        /// <remarks>Change drivers: CD-22 (hosting/deployment spec), CD-01 (open.mp/SampSharp platform API)</remarks>
        public static void Main()
        {
            SampSharp.OpenMp.Core.StartupContext.MainInfoProvider();
        }
    }
}
