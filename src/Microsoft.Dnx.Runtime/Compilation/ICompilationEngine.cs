using System;
using System.Reflection;
using System.Runtime.Versioning;

namespace Microsoft.Dnx.Runtime.Compilation
{
    public interface ICompilationEngine : IDisposable
    {
        event Action<string> OnInputFileChanged;

        Assembly LoadProject(Project project, FrameworkName targetFramework, string configuration, string aspect, IAssemblyLoadContext loadContext);
    }
}
