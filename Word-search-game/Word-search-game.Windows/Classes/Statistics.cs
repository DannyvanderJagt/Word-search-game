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
        // data opslaan in de local folder van de app
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder ScoreFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public String[] scores = new String[]{};
        private StorageFile sampleFile;

        public async Task open()
        {
            System.Diagnostics.Debug.WriteLine("start sample");
            try
            {
                this.sampleFile = await ScoreFolder.CreateFileAsync("dataFile.txt", CreationCollisionOption.OpenIfExists);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("open error");
                System.Diagnostics.Debug.WriteLine(e);
            }
            System.Diagnostics.Debug.WriteLine("end stat.sample");
        }

        /// <summary>
        /// Ophalen van elke score dat in de local folder aanwezig is
        /// Deze weergeven in listbox
        /// </summary>
        public async Task read()
        {
            StorageFile sampleFile;
            String score;

                 //sampleFile  = await ScoreFolder.GetFileAsync("dataFile.txt");
                 try
                 {
                     score = await FileIO.ReadTextAsync(this.sampleFile);
                     this.scores = score.Split(';');
                 }
                 catch (Exception e)
                 {
                     System.Diagnostics.Debug.WriteLine("error2");
                     System.Diagnostics.Debug.WriteLine(e);
                 }
                 
          

         
            
        }

        public async Task write(String value)
        {
            System.Diagnostics.Debug.WriteLine("start stat.write");
          
                try
                {
                    await Windows.Storage.FileIO.WriteTextAsync(this.sampleFile, value);
                    /*if (this.sampleFile != null)
                    {
                        await Windows.Storage.FileIO.WriteTextAsync(this.sampleFile, value);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Write is niet gelukt!");
                    }*/
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("error2");
                    System.Diagnostics.Debug.WriteLine(e);
                }
           
          
        }








    }
}
