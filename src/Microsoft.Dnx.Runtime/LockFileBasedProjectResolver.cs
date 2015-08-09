// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Dnx.Runtime.DependencyManagement;
using NuGet;

namespace Microsoft.Dnx.Runtime
{
    public class LockFileBasedProjectResolver : IProjectResolver
    {
        private readonly Dictionary<string, string> _searchPath;
        private readonly Dictionary<string, Project> _projectCache;

        public LockFileBasedProjectResolver(LockFile lockFile, string projectDirectory, params Project[] additionalProjects)
        {
            // TODO: Should there be duplicate project reference, exception will be thrown here
            //       Make the error message more friendly.
            _searchPath = lockFile.ProjectLocations.ToDictionary(
                keySelector: location => location.Name,
                elementSelector: location => Path.GetFullPath(Path.Combine(projectDirectory, location.RelativePath)));

            _projectCache = new Dictionary<string, Project>();
            foreach (var project in additionalProjects)
            {
                _projectCache[project.Name] = project;
                _searchPath[project.Name] = project.ProjectFilePath;
            }
        }

        public IEnumerable<string> SearchPaths
        {
            get
            {
                // TODO: Reconsider the performance impact, should we cache it?
                return _searchPath.Values.Select(path => Path.GetDirectoryName(path));
            }
        }

        public bool TryResolveProject(string name, out Project project)
        {
            if (_projectCache.TryGetValue(name, out project))
            {
                return true;
            }

            string projectPath;
            if (!_searchPath.TryGetValue(name, out projectPath))
            {
                return false;
            }

            // TODO: Consider concurrent situation
            if (Project.TryGetProject(projectPath, out project))
            {
                _projectCache[name] = project;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
