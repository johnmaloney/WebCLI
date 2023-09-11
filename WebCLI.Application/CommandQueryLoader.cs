using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using WebCLI.Core;
using System.Linq;

namespace WebCLI.Application
{
    /// <summary>
    /// Application level object that takes a list of commands and queries to load into the systems repositories.
    /// </summary>
    public class CommandQueryLoader
    {
        #region Fields

        private readonly List<string> folderLocations;
        private readonly ICommandRepository commands;
        private readonly IQueryRepository queries;

        #endregion

        #region Properties



        #endregion

        #region Methods

        public CommandQueryLoader(List<string> folderLocations, ICommandRepository commands, IQueryRepository queries)
        {
            this.folderLocations = folderLocations;
            this.commands = commands;
            this.queries = queries;
        }

        public async Task BuildOutCommandRepository()
        {
            if (this.commands == null) { 
                throw new NotSupportedException("The Command Repository must be assigned."); }
            if (this.queries == null) { 
                throw new NotSupportedException("The Queries Repository must be assigned."); }

            var files = new List<string>();
            // Get the files from the Folder Locations //
            foreach (var location in this.folderLocations)
            { 
                files.AddRange(await FindJsonFiles(location));
            }

            // Deserialize either a list or a single Criteria object //
            var criterias = new List<ICriteria>();
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    var data = JsonConvert.DeserializeObject<List<Criteria>>
                        (await File.ReadAllTextAsync(file));

                    if (data != null)
                        criterias.AddRange(data);
                    else
                    {
                        var singleData = JsonConvert.DeserializeObject<Criteria>
                        (await File.ReadAllTextAsync(file));

                        if (singleData != null)
                            criterias.Add(singleData);
                    }
                }
            }
            foreach(var criteria in criterias)
            {
                // Generate and Add Commands to the Repository //
                this.commands.AddCommandDelegate(criteria.Identifier,
                    (context) =>
                    {
                        return criteria.GetPipeline(null);
                    });
            }
        }

        private async Task<List<string>> FindJsonFiles(string folderPath)
        {
            List<string> jsonFileNames = new List<string>();

            // Parse whether this is a folder or a file //
            if (folderPath.IsDirectoryPath())
            {
                try
                {
                    DirectoryInfo directory = new DirectoryInfo(folderPath);
                    foreach (FileInfo file in directory.GetFiles("*.json"))
                    {
                        jsonFileNames.Add(Path.GetFullPath(file.FullName));
                    }
                    foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                    {
                        jsonFileNames.AddRange(await FindJsonFiles(subDirectory.FullName));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    // Assuming this is a single file reference //
                    var file = new FileInfo(folderPath);
                    if (file != null)
                        jsonFileNames.Add(file.FullName);
                    else
                        throw new NotSupportedException($"The file : [{file}] was not discovered, make sure the file is at the path specified");

                }
                catch (Exception)
                {
                    throw;
                }
            }            
            return jsonFileNames;
        }

        

        #endregion
    }
}
