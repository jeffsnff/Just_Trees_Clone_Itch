using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Just_Trees_Clone.Objects.Helper;

namespace Just_Trees_Clone.Objects.Player
{
  internal class Player
  {
    public float XCoord { get; private set; }
    public float YCoord { get; private set; }
    private float Speed { get; set; }
    public PlayerState State { get; private set; }
    private bool LeftMouseClicked { get; set; } = false;
    private bool RightButtonClicked { get; set; } = false;
    public int NumberOfLogs { get; set; }

    public bool ChopDamageApplied { get; private set; }

    private bool _isChopping;

    private Texture2D _playerSprite;
    private int _animationTimer, _animationIndex, _animationRow;
    private SpriteEffects _spriteFlip = SpriteEffects.None;

    public Player(ContentManager content)
    {
      XCoord = 0;
      YCoord = 0;
      Speed = 5;
      State = PlayerState.Idel;
      NumberOfLogs = 0;

      _playerSprite = content.Load<Texture2D>("Sprites/Player/playerSpriteSheet");
      _animationIndex = 0;
      _animationTimer = 0;
      _animationRow = 0;
    }

    public bool ChopTree()
    {
      if(State == PlayerState.Chopping && _animationIndex == 4 && !ChopDamageApplied)
      {
        ChopDamageApplied = true;
        return true;
      }
      return false;
    }

    public void RemoveLogs()
    {
      NumberOfLogs--;
    }

    public Rectangle GetBounds()
    {
      return new Rectangle((int) XCoord+60, (int) YCoord+45, 35, 35);
    }

    private void Move()
    {
      KeyboardState currentKeyboardState = Keyboard.GetState();
      MouseState currentMouseState = Mouse.GetState();

      if (_isChopping)
      {
        State = PlayerState.Chopping;
      }

      if (State == PlayerState.Idel)
      {
        if (currentKeyboardState.IsKeyDown(Keys.D))
        {
          _spriteFlip = SpriteEffects.None;
          XCoord += Speed;
          State = PlayerState.Walking;
        }
        if (currentKeyboardState.IsKeyDown(Keys.A))
        {
          _spriteFlip = SpriteEffects.FlipHorizontally;
          XCoord -= Speed;
          State = PlayerState.Walking;
        }
        if (currentKeyboardState.IsKeyDown(Keys.W))
        {
          YCoord -= Speed;
          State = PlayerState.Walking;
        }
        if (currentKeyboardState.IsKeyDown(Keys.S))
        {
          YCoord += Speed;
          State = PlayerState.Walking;
        }
      }

      // Chopping Logic
      if (!LeftMouseClicked && currentMouseState.LeftButton == ButtonState.Pressed)
      {
        State = PlayerState.Chopping;
        _isChopping = true;
        ChopDamageApplied = false;
        _animationIndex = 0;
        _animationTimer = 0;
        LeftMouseClicked = true;
      }
      
      // Feeding Logic
      if (!RightButtonClicked && currentMouseState.RightButton == ButtonState.Pressed)
      {
        State = PlayerState.Feeding;
        RightButtonClicked = true;
      }

      if (LeftMouseClicked && currentMouseState.LeftButton == ButtonState.Released)
      {
        LeftMouseClicked = false;
      }

      if (RightButtonClicked && currentMouseState.RightButton == ButtonState.Released)
      {
        RightButtonClicked = false;
      }

      _animationTimer++;
      if (_animationTimer >= 5)
      {
        _animationIndex++;
        if (State == PlayerState.Idel && _animationIndex >= 4)
        {
          _animationIndex = 0;
        }
        if (State == PlayerState.Walking && _animationIndex >= 8)
        {
          _animationIndex = 0;
        }
        if (State == PlayerState.Chopping && _animationIndex >= 6)
        {
          _animationIndex = 0;
          _isChopping = false;
          State = PlayerState.Idel;
        }


        if (State == PlayerState.Idel)
        {
          _animationRow = 0;
        }
        if (State == PlayerState.Walking)
        {
          _animationRow = 1;
        }
        if (State == PlayerState.Chopping)
        {
          _animationRow = 2;
        }

        _animationTimer = 0;
      }
    }
    public void Update()
    {
      State = PlayerState.Idel;
      Move();
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D boundingBoxTexture, SpriteFont gameFont)
    {
      // TODO: Draw Sprite
      spriteBatch.DrawString(gameFont, "State: "+State, new Vector2(XCoord, YCoord - 32), Color.White);
      spriteBatch.DrawString(gameFont, "Num Logs: "+NumberOfLogs, new Vector2(XCoord+80, YCoord - 32), Color.White);
      spriteBatch.Draw(boundingBoxTexture, GetBounds(), Color.Red);



      spriteBatch.Draw
        (
          _playerSprite,
          new Vector2(XCoord, YCoord),
          new Rectangle(_animationIndex * 79, _animationRow * 79, 79, 79),
          Color.White,
          0.0f,
          new Vector2(0, 0),
          2.0f,
          _spriteFlip,
          0
        );



      spriteBatch.DrawString(gameFont, "Left Clicked: "+LeftMouseClicked, new Vector2(XCoord, YCoord + 84), Color.White);
    }
  }
}
