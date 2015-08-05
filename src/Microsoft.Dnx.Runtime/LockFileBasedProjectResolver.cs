// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Dnx.Runtime.DependencyManagement;

namespace Microsoft.Dnx.Runtime
{
    public class LockFileBasedProjectResolver : IProjectResolver
    {
        private readonly string _baseDirectory;
        private readonly LockFile _lockFile;
        private readonly ConcurrentDictionary<string, Project> _projectsCache;

        public LockFileBasedProjectResolver(LockFile lockFile, string projectDirectory, Project root)
        {
            _lockFile = lockFile;
            _projectsCache = new ConcurrentDictionary<string, Project>();
            _projectsCache[root.Name] = root;

            _baseDirectory = projectDirectory;
        }

        public IEnumerable<string> SearchPaths
        {
            get { throw new NotImplementedException(); }
        }

        public bool TryResolveProject(string name, out Project project)
        {
            project = _projectsCache.GetOrAdd(name, key =>
            {
                var library = _lockFile.ProjectLibraries.Where(lib => lib.Name == key)
                                                        .FirstOrDefault();

                if (library == null)
                {
                    return null;
                }
                else
                {
                    var path = Path.Combine(_baseDirectory, library.Path);
                    Project result;
                    if (Project.TryGetProject(path, out result))
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            });

            return project != null;
        }
    }
}
