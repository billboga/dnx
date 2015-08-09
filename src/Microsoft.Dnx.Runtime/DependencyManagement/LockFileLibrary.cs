// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using NuGet;

namespace Microsoft.Dnx.Runtime.DependencyManagement
{
    public abstract class LockFileLibrary
    {
        public string Name { get; set; }

        public SemanticVersion Version { get; set; }
    }
}
