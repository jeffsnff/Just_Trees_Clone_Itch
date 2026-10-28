using Just_Trees_Clone.Objects.Helper;
using Just_Trees_Clone.Objects.Player;
using Just_Trees_Clone.Objects.Spirit;
using Just_Trees_Clone.Objects.Tree;
using Just_Trees_Clone.Objects.TreeLog;
using JustTreesClone.Objects.Music;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace JustTreesClone
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class JustTreesCloneGame : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    private MusicPlayer _backgroundMusic;
    private Texture2D _boundBoxTexture;
    private List<Tree> _trees;
    private List<Spirit> _treeSpirits;
    private List<Player> _players;
    private List<TreeLog> _treeLogs;
    private Camera2D _camera2D;
    private SpriteFont _gameFont;
    private Color _treeSpiritColor;
    

    public JustTreesCloneGame()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here
      _boundBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
      _boundBoxTexture.SetData(new Color[] { Color.White });
      _camera2D = new Camera2D(GraphicsDevice.Viewport);
      _backgroundMusic = new MusicPlayer(Content);
      

      _trees = new List<Tree>();
      _treeSpirits = new List<Spirit>();
      _players = new List<Player>();
      _treeLogs = new List<TreeLog>();

      _treeSpiritColor = Color.Fuchsia;

      for (int num = 0; num < 2; num++)
      {
        _trees.Add(new Tree());
      }
      _treeSpirits.Add(new Spirit(Content));
      _players.Add(new Player(Content));
      

      base.Initialize();
    }

    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      _gameFont = Content.Load<SpriteFont>("GameFont");
      
      
    }

    protected override void UnloadContent()
    {
      // TODO: Unload any non ContentManager content here
    }

    protected override void Update(GameTime gameTime)
    {
      MouseState mouseState = Mouse.GetState();
      KeyboardState keyboardState = Keyboard.GetState();
      GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

      if (keyboardState.IsKeyDown(Keys.Escape) ||
          keyboardState.IsKeyDown(Keys.Back) ||
          gamePadState.Buttons.Back == ButtonState.Pressed)
      {
        try
        {
          Exit();
        }
        catch (PlatformNotSupportedException)
        {
          /* ignore */
        }
      }

      _backgroundMusic.Update(keyboardState);
      

      for (int playerIndex = 0; playerIndex < _players.Count; playerIndex++)
      {
        _players[playerIndex].Update();
        _camera2D.FollowClamped(
            new Vector2(
                _players[playerIndex].XCoord,
                _players[playerIndex].YCoord),
            9000,
            9000
        );

        // Feed the Tree Spirit
        for (int spiritIndex = 0; spiritIndex < _treeSpirits.Count; spiritIndex++)
        {
          if (_players[playerIndex].GetBounds().Intersects(_treeSpirits[spiritIndex].GetBounds()))
          {
            _treeSpiritColor = Color.Salmon;
            if (_players[playerIndex].State == PlayerState.Feeding && _players[playerIndex].NumberOfLogs > 0)
            {
              _treeSpirits[spiritIndex].Feed();
              _players[playerIndex].RemoveLogs();
            }
          }
          else
          {
            _treeSpiritColor = Color.Fuchsia;
          }
        }
      }

      for (int i = 0; i < _trees.Count; i++)
      {
        if (_trees[i].GetBounds().Intersects(_players[0].GetBounds()))
        {
          _trees[i].Update(Color.Yellow);
          if (_players[0].State == PlayerState.Chopping)
          {
            _trees[i].TakeDamage();
          }
        }
        else
        {
          _trees[i].Update(Color.LawnGreen);
        }

        if (!_trees[i].IsAlive)
        {
          for (int index = 0; index < 5; index++)
          {
            _treeLogs.Add(new TreeLog(_trees[i].XCoord + i, _trees[i].YCoord + i));
          }
          _trees.RemoveAt(i);
        }
      }

      for (int i = 0; i < _treeLogs.Count; i++)
      {
        if (_treeLogs[i].GetBounds().Intersects(_players[0].GetBounds()))
        {
          _players[0].NumberOfLogs++;
          _treeLogs.RemoveAt(i);
        }
      }

      foreach (Spirit treeSpirit in _treeSpirits)
      {
        treeSpirit.Update();
      }

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin(transformMatrix: _camera2D.GetTransform());
      foreach (Tree tree in _trees)
      {
        tree.Draw(spriteBatch, _boundBoxTexture, _gameFont);
      }

      foreach (TreeLog log in _treeLogs)
      {
        log.Draw(spriteBatch, _boundBoxTexture);
      }

      foreach (Spirit treeSpirit in _treeSpirits)
      {
        spriteBatch.DrawString(_gameFont, "Hunger Level: " + treeSpirit.HungerLevel, new Vector2(treeSpirit.XCoord, treeSpirit.YCoord - 25), Color.White);
        spriteBatch.Draw(_boundBoxTexture, treeSpirit.GetBounds(), _treeSpiritColor);
      }

      foreach (Player player in _players)
      {
        player.Draw(spriteBatch, _boundBoxTexture, _gameFont);
      }
      spriteBatch.End();

      spriteBatch.Begin();
      _backgroundMusic.Draw(spriteBatch, _gameFont, new Vector2(0,0));
      spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}