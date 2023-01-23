using System.Collections.Generic;
using System.Linq;

namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceDetailsResponse
    {
        public ConcordanceDetailsResponseStatement NextStatement { get; set; }

        public ConcordanceDetailsResponseStatement PreviousStatement { get; set; }

        public ConcordanceDetailsResponseStatement Statement { get; set; }

        public List<string> AllSoundFiles
        {
            get
            {
                var soundFiles = new List<string>();
                AddSoundFiles(PreviousStatement);
                AddSoundFiles(Statement);
                AddSoundFiles(NextStatement);
                return soundFiles;

                void AddSoundFiles(ConcordanceDetailsResponseStatement statement)
                {
                    if (statement?.SoundFiles?.Any() == true)
                    {
                        soundFiles.AddRange(statement.SoundFiles);
                    }
                }
            }
        }
    }
}