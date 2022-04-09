﻿using FFConvert.Domain;
using FFConvert.DomainServices;
using FFConvert.Properties;

namespace FFConvert.Steps
{
    internal class CollectInputFiles : BaseStep
    {
        public override bool TryExecute(State state)
        {
            if (state.Arguments.InputFileContainsWildCard())
            {
                string directory = FileSystem.GetWorkingDirectoryFromInputFile(state.Arguments.FileName);
                var files = FileSystem.GetFilesMatchingWildCard(directory, Path.GetFileName(state.Arguments.FileName));

                state.AddFiles(files);

                if (!state.HasInputFiles())
                {
                    AddIssue(Resources.ErrorFilesNotFound);
                }
            }
            else
            {
                var singleFile = Path.GetFullPath(state.Arguments.FileName);

                if (File.Exists(singleFile))
                {
                    state.InputFiles.Add(singleFile);
                }
                else
                {
                    AddIssue(Resources.ErrorFileNotExists);
                }
            }

            return AreNoIssues();
        }
    }
}