using System;
using System.IO;
using System.Threading.Tasks;

namespace ERPSystem.Utils
{
    public class BackupUtility
    {
        private readonly string _backupDirectory;

        public BackupUtility(string backupDirectory)
        {
            _backupDirectory = backupDirectory;
            // Ensure the backup directory exists
            if (!Directory.Exists(_backupDirectory))
            {
                Directory.CreateDirectory(_backupDirectory);
            }
        }

        // Method to create a backup of a file
        public async Task BackupFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new ArgumentException("Invalid file path.");
            }

            var fileName = Path.GetFileName(filePath);
            var backupFilePath = Path.Combine(_backupDirectory, $"{DateTime.Now:yyyyMMddHHmmss}_{fileName}");

            try
            {
                // Copy the file to the backup directory
                await Task.Run(() => File.Copy(filePath, backupFilePath, overwrite: true));
                Console.WriteLine($"Backup created: {backupFilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating backup: {ex.Message}");
                throw;
            }
        }

        // Method to create a backup of a directory
        public async Task BackupDirectoryAsync(string sourceDirectory)
        {
            if (string.IsNullOrEmpty(sourceDirectory) || !Directory.Exists(sourceDirectory))
            {
                throw new ArgumentException("Invalid source directory.");
            }

            var backupDirectory = Path.Combine(_backupDirectory, $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(sourceDirectory)}");

            try
            {
                // Create the backup directory
                Directory.CreateDirectory(backupDirectory);

                // Copy all files in the source directory to the backup directory
                var files = Directory.GetFiles(sourceDirectory);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var backupFilePath = Path.Combine(backupDirectory, fileName);
                    await Task.Run(() => File.Copy(file, backupFilePath, overwrite: true));
                }

                // Copy all subdirectories
                var subdirectories = Directory.GetDirectories(sourceDirectory);
                foreach (var subdirectory in subdirectories)
                {
                    await BackupDirectoryAsync(subdirectory);
                }

                Console.WriteLine($"Backup created: {backupDirectory}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating backup: {ex.Message}");
                throw;
            }
        }
    }
}
