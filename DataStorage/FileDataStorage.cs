using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataStorage
{
    public class FileDataStorage<TObject> where TObject : class, IStorable
    {
        private string BaseFolder;

        public FileDataStorage(string subfolder = "")
        {
            if (!String.IsNullOrWhiteSpace(subfolder))
            {
                BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "BudgetsStorage", typeof(TObject).Name, subfolder);
            }
            else
            {
                BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "BudgetsStorage", typeof(TObject).Name);
            }
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public async Task AddOrUpdateAsync(TObject obj)
        {
            string stringObject = JsonSerializer.Serialize(obj);

            string FilePath = Path.Combine(BaseFolder, obj.Guid.ToString("N"));

            using (StreamWriter sw = new StreamWriter(FilePath, false))
            {
                await sw.WriteAsync(stringObject);
            }
        }

        public async Task<TObject> GetAsync(Guid guid)
        {
            string stringObject = null;

            string FilePath = Path.Combine(BaseFolder, guid.ToString("N"));

            if (!File.Exists(FilePath))
                return null;

            using (StreamReader sr = new StreamReader(FilePath))
            {
                stringObject = await sr.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<TObject>(stringObject);
        }

        public async Task<List<TObject>> GetAllAsync()
        {
            var res = new List<TObject>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObject = null;

                using (StreamReader sr = new StreamReader(file))
                {
                    stringObject = await sr.ReadToEndAsync();
                }

                res.Add(JsonSerializer.Deserialize<TObject>(stringObject));
            }

            return res;
        }

        public void Delete(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                Directory.Delete(BaseFolder, true);
                return;
            }

            string FilePath = Path.Combine(BaseFolder, guid.ToString("N"));

            if (!File.Exists(FilePath))
                return;

            File.Delete(FilePath);
        }
    }
}
