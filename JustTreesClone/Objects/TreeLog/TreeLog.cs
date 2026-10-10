using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Just_Trees_Clone.Objects.TreeLog;

public class TreeLog
{
    private int XCoord { get; set; }
    private int YCoord { get; set; }
    private Texture2D Sprite { get; set; }

    public TreeLog(int xCoord, int yCoord)
    {
        XCoord = xCoord;
        YCoord = yCoord;
    }

    public Rectangle GetBounds()
    {
        return new Rectangle(XCoord, YCoord, 25, 35);
    }

    public void Update()
    {
        
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D boundingBox)
    {
        spriteBatch.Draw(boundingBox, GetBounds(), Color.Brown);
    }
}