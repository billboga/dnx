// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using NuGet;

namespace Microsoft.Dnx.Runtime.DependencyManagement
{
    public class LockFileProjectLocation
    {
        /// <summary>
        /// Constructor for deserialization
        /// </summary>
        public LockFileProjectLocation(string name, string relativePath)
        {
            Name = name;
            RelativePath = relativePath;
        }

        public LockFileProjectLocation(Project project, Project reference)
        {
            Name = reference.Name;
            RelativePath = PathUtility.GetRelativePath(project.ProjectFilePath, reference.ProjectFilePath, '/');
        }

        public string Name { get; set; }

        public string RelativePath { get; set; }
    }
}
