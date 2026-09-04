namespace SampSharp
{
    /// <remarks>Change drivers: CD-32 (root; ECS runtime: unmanaged entrypoint/startup); CD-22 (hosting/deployment spec) → CD-32</remarks>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("SampSharp.SourceGenerator", "1.0.0.0")]
    public static class Entrypoint
    {
        /// <remarks>Change drivers: CD-32 (root; ECS runtime: unmanaged entrypoint/startup); CD-22 (hosting/deployment spec) → CD-32</remarks>
        private static readonly global::CTF.Host.Ecs.Startup _startup = new();

        /// <remarks>Change drivers: CD-32 (root; ECS runtime: unmanaged entrypoint)</remarks>
        private static SampSharp.OpenMp.Core.StartupContext _context;
        /// <remarks>Change drivers: CD-32 (root; ECS runtime: unmanaged entrypoint/startup); CD-22 (hosting/deployment spec) → CD-32</remarks>
        [global::System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute]
        public static void Initialize(SampSharp.OpenMp.Core.SampSharpInitParams inf)
        {
            _context = new SampSharp.OpenMp.Core.StartupContext(inf);
            _context.InitializeUsing(_startup);
        }

        /// <remarks>Change drivers: CD-32 (root; ECS runtime: unmanaged entrypoint/startup); CD-22 (hosting/deployment spec) → CD-32</remarks>
        public static void Main()
        {
            SampSharp.OpenMp.Core.StartupContext.MainInfoProvider();
        }
    }
}
