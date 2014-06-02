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

        public Statistics()
        {
            localFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task add(String name, int score)
        {
            System.Diagnostics.Debug.WriteLine("before data!" + this.data);
            this.data = this.data + name + "," + score + ":";

            System.Diagnostics.Debug.WriteLine("new data!" + this.data);

            StorageFile file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, this.data);
            System.Diagnostics.Debug.WriteLine("Added!"+this.data);
            await read();
        }

        public String result = "";
        async public Task read()
        {
            try
            {
                StorageFile file = await localFolder.GetFileAsync(filename);
                string text = await FileIO.ReadTextAsync(file);

                result = text;
                System.Diagnostics.Debug.WriteLine("text"+text);
                this.data = text;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Not found!");
                //LocalOutputTextBlock.Text = "Local Counter: <not found>";
            }
        }

        /*static public Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        static public Dictionary<String, int> scores = new Dictionary<String, int>();

        public static Dictionary<String, int> get()
        {
            scores = (Dictionary<String, int>) localSettings.Values["scores"];

            int count = (int) localSettings.Values["count"];
            for (int i = 0; i < count; i++)
            {
                scores.Add((String)localSettings.Values[("name" + i)], (int)localSettings.Values[("score" + i)]);
            }
            return scores;
        }

        public static void add(String name, int score){
            scores.Add(name, score);
            System.Diagnostics.Debug.WriteLine("added length:"+scores.Count);
            Statistics.save();
        }

        public static void save()
        {
            System.Diagnostics.Debug.WriteLine("save length:" + scores.Count);
            int count = 0;
            foreach (KeyValuePair<string, int> entry in scores)
            {
                localSettings.Values[("name"+count)] = entry.Key;
                localSettings.Values[("score"+count)] = entry.Value;
                count++;
            }
            System.Diagnostics.Debug.WriteLine("save count:" + count);
            localSettings.Values["count"] = count;
        }*/







    }
}
