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
        public String data = ""; // The data string that will be stored in the local storage.
        private StorageFolder localFolder = null; // The storage folder.
        const string filename = "sampleFile.txt"; // The file name.

        // Store all the scores is a list for easy processing.
        public List<KeyValuePair<String, int>> scores= new List<KeyValuePair<String, int>>();

        /*
         * Constructor.
         */
        public Statistics()
        {
            localFolder = ApplicationData.Current.LocalFolder;
        }

        /*
         * Add some data to the local storage.
         * @param String name - The name of the user.
         * @param int score - The score of the user.
         */
        public async Task add(String name, int score)
        {
            await this.read();
            this.data = this.data + name + "," + score + ":";
            StorageFile file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, this.data);
            await this.convertStringToArray();
        }

        /*
         * Read the local storage.
         */
        async public Task read()
        {
            try
            {
                StorageFile file = await localFolder.GetFileAsync(filename);
                string text = await FileIO.ReadTextAsync(file);
                this.data = text;
                await this.convertStringToArray();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Not found!");
            }
        }

       /*
        * Convert the data string from the storage to an List and sort the list bij value/score.
        */
       async public Task convertStringToArray()
        {
            String text = this.data;
            this.scores = new List<KeyValuePair<string, int>>();
            // Split the string.
            String[] parts = text.Split(':');

            // Looping.
            foreach (String part in parts)
            {
                if (String.IsNullOrEmpty(part) == false && String.IsNullOrWhiteSpace(part) == false)
                {
                    String[] args = part.Split(',');
                    KeyValuePair<string, int> kv = new KeyValuePair<string,int>(args[0], Int32.Parse(args[1]));
                    this.scores.Add(kv);
                }
            }
            
            // Sort..
            this.scores.Sort(delegate(KeyValuePair<String, int> x, KeyValuePair<String, int> y)
            {
                if (x.Value == y.Value)
                {
                    return 1;
                }
                else if(x.Value < y.Value)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
        }   
    }
}
