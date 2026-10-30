using Microsoft.Xna.Framework.Media;

namespace JustTreesClone.Objects.Music
{
  public class MusicTrack
  {
    public Song Song { get; }
    public string Name { get; }

    public MusicTrack(Song song, string name)
    {
      Song = song;
      Name = name;
    }
  }
}
