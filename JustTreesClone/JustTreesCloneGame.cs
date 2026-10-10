using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Just_Trees_Clone.Objects.Tree;
using Just_Trees_Clone.Objects.Spirit;
using Just_Trees_Clone.Objects.Player;
using Just_Trees_Clone.Objects.Helper;

namespace JustTreesClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class JustTreesCloneGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D _boundBoxTexture;
        private List<Tree> _trees;
        private List<Spirit> _treeSpirits;
        private List<Player> _players;
        private Camera2D _camera2D;
        private SpriteFont _gameFont;

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

            _trees = new List<Tree>();
            _treeSpirits = new List<Spirit>();
            _players = new List<Player>();
            
            
            for (int num = 0; num < 15; num++)
            {
                _trees.Add(new Tree());
            }
            _treeSpirits.Add(new Spirit(Content));
            _players.Add(new Player(Content));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Use this.Content to load your game content here
            _gameFont = Content.Load<SpriteFont>("GameFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
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

            // TODO: Add your update logic here
            foreach(Player player in _players)
            {
                player.Update();
                _camera2D.FollowClamped(
                    new Vector2(
                        player.XCoord,
                        player.YCoord),
                    9000,
                    9000
                );
            }

            for(int i = 0; i < _trees.Count; i++)
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
                    _trees.RemoveAt(i);
                }
            }

            foreach (Spirit treeSpirit in _treeSpirits)
            {
                treeSpirit.Update();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            spriteBatch.Begin(transformMatrix: _camera2D.GetTransform());

            foreach (Tree tree in _trees)
            {
                spriteBatch.DrawString(_gameFont, "Health: "+tree.Health, new Vector2(tree.XCoord, tree.YCoord-20), Color.White);
                tree.Draw(spriteBatch, _boundBoxTexture);
            }

            foreach (Spirit treeSpirit in _treeSpirits)
            {
                spriteBatch.Draw(_boundBoxTexture, treeSpirit.GetBounds(), Color.Fuchsia);
            }
            
            foreach(Player player in _players)
            {
                player.Draw(spriteBatch, _boundBoxTexture, _gameFont);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}