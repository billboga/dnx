﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;

namespace Microsoft.Dnx.Runtime.Helpers
{
    public static class ProjectPathHelper
    {
        public static string ResolveRootDirectory(string projectPath)
        {
            var di = new DirectoryInfo(projectPath);

            while (di.Parent != null)
            {
                var globalJsonPath = Path.Combine(di.FullName, GlobalSettings.GlobalFileName);

                if (File.Exists(globalJsonPath))
                {
                    return di.FullName;
                }

                di = di.Parent;
            }

            // If we don't find any files then make the project folder the root
            return projectPath;
        }
    }
}
