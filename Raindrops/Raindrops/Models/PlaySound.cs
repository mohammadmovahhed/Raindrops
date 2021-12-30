using System.IO;
using System.Reflection;

namespace Raindrops.Models
{
    public class PlaySound
    {
        public static void Play(string name)
        {
            string path = string.Format("Raindrops.Resources."+ name);
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream(path);
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(audioStream);

            audio.Play();
        }
    }
}
