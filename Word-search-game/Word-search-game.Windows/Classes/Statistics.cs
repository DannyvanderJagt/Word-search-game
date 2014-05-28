using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Word_search_game.Classes
{
    class Statistics
    {
        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        StorageFile storageFile;
        string storageFileValues;
        string[] values;
        string file = "statistics.txt";
        public String[] scores;


        public async Task readFile()
        {
            storageFile = await storageFolder.CreateFileAsync(file, CreationCollisionOption.OpenIfExists);
            storageFileValues = await Windows.Storage.FileIO.ReadTextAsync(storageFile);

            values = storageFileValues.Split(';');
            scores = values;
        }

        public async Task writeToFile(string name, int score)
        {
            readFile();

            storageFile = await storageFolder.CreateFileAsync("statistics.txt", CreationCollisionOption.OpenIfExists);

            using (IRandomAccessStream randomAccessStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (IOutputStream outputStream = randomAccessStream.GetOutputStreamAt(0))
                {
                    DataWriter dataWriter = new Windows.Storage.Streams.DataWriter(outputStream);
                    dataWriter.WriteString(storageFileValues + name + ":" + score + ";");
                    await dataWriter.StoreAsync();
                    dataWriter.DetachStream();
                    await outputStream.FlushAsync();
                }
            }
        }
    }
}
