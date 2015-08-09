// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using NuGet;

namespace Microsoft.Dnx.Runtime.DependencyManagement
{
    public class LockFileLibrary
    {
        public string Name { get; set; }

        public SemanticVersion Version { get; set; }

        public bool IsServiceable { get; set; }

        public string Sha512 { get; set; }

        public IList<string> Files { get; set; } = new List<string>();
    }

    public class LockFileItem
    {
        public string Path { get; set; }

        public IDictionary<string, string> Properties { get; } = new Dictionary<string, string>();

        public static implicit operator string (LockFileItem item) => item.Path;

        public static implicit operator LockFileItem(string path) => new LockFileItem { Path = path };

        public override string ToString() => Path;
    }
}
