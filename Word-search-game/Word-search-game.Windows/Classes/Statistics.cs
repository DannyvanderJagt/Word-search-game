using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using Windows.Storage;

namespace Word_search_game.Classes
{
    class Statistics
    {
        public String data = "";
        private StorageFolder localFolder = null;
        const string filename = "sampleFile.txt";
        public Dictionary<String, int> scores = new Dictionary<String, int>();
        public Dictionary<String, int> sortedScores = new Dictionary<String, int>();

        public Statistics()
        {
            localFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task add(String name, int score)
        {
            await this.read();
            this.data = this.data + name + "," + score + ":";
            StorageFile file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, this.data);
            //await read();
        }

        async public Task read()
        {
            try
            {
                StorageFile file = await localFolder.GetFileAsync(filename);
                string text = await FileIO.ReadTextAsync(file);
                this.data = text;
                await this.convertStringToArray(text);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Not found!");
            }
        }

       async public Task convertStringToArray(String text)
        {
            this.scores = new Dictionary<String, int>();
            // Split the string.
            System.Diagnostics.Debug.WriteLine("text"+text);
            String[] parts = text.Split(':');
            System.Diagnostics.Debug.WriteLine("parts"+parts.Length);
            foreach (String part in parts)
            {
                System.Diagnostics.Debug.WriteLine("p" + part);
                if (String.IsNullOrEmpty(part) == false && String.IsNullOrWhiteSpace(part) == false)
                {
                    String[] args = part.Split(',');
                    this.scores.Add(args[0], Int32.Parse(args[1]));
                }

            }
            
            // Sort..
            foreach (KeyValuePair<string, int> score in this.scores.OrderByDescending(key => key.Value))
            {
                System.Diagnostics.Debug.WriteLine(score.Key + " : " + score.Value);
                this.sortedScores.Add(score.Key, score.Value);
            }

        }   
    }
}
