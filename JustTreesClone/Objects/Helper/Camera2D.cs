using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Just_Trees_Clone.Objects.Helper
{
  internal class Camera2D
  {
    public Vector2 Position { get; set; }
    public float Zoom { get; set; } = 1f;
    private readonly Viewport _viewPort;

    public Camera2D(Viewport viewport)
    {
      _viewPort = viewport;
    }

    public Matrix GetTransform()
    {
      return Matrix.CreateTranslation(
        -Position.X, -Position.Y, 0) *
          Matrix.CreateScale(Zoom) *
            Matrix.CreateTranslation(
              _viewPort.Width / 2f,
              _viewPort.Height / 2f,
              0
              );
    }

    public void Follow(Vector2 target)
    {
      Position = target;
    }

    public void FollowClamped(Vector2 target, int worldWidth, int worldHeight)
    {
      float halfViewPortWidth = _viewPort.Width / 2f / Zoom;
      float halfViewPortHeight = _viewPort.Height / 2f / Zoom;

      float minX = halfViewPortWidth;
      float maxX = worldWidth - halfViewPortWidth;

      float minY = halfViewPortHeight;
      float maxY = worldHeight - halfViewPortHeight;

      // TODO : Need to clamp to map size
      Position = new Vector2(
        target.X,
        target.Y
        );
    }
  }
}
