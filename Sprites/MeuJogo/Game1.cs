using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MeuJogo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _idleSheet;
        private Texture2D _walkSheet;

        private Vector2 _playerPosition;
        private float _playerSpeed;

        private float _animationTimer;
        private int _currentFrame;
        private float _animationInterval; 

        private enum Direction { Down, Left, Right, Up }
        private Direction _currentDirection = Direction.Down;

        private bool _isMoving = false;

        private Rectangle sourceRect;
        private SpriteEffects spriteEffect = SpriteEffects.None;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _playerPosition = new Vector2(200, 200);
            _playerSpeed = 100f;
            _animationInterval = 150f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _idleSheet = Content.Load<Texture2D>("Idle");
            _walkSheet = Content.Load<Texture2D>("Walk");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboard = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            _isMoving = false;

            if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            {
                direction.Y -= 1;
                _currentDirection = Direction.Up;
                _isMoving = true;
            }
            if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            {
                direction.Y += 1;
                _currentDirection = Direction.Down;
                _isMoving = true;
            }
            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
                _currentDirection = Direction.Left;
                _isMoving = true;
            }
            if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
                _currentDirection = Direction.Right;
                _isMoving = true;
            }

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                _isMoving = true;
                _playerPosition += direction * _playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            _animationTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_animationTimer > _animationInterval)
            {
                _currentFrame++;
                _animationTimer = 0f;
            }
            
            int row = 0;
            spriteEffect = SpriteEffects.None;

            switch (_currentDirection)
            {
                case Direction.Down: row = 0; break;
                case Direction.Up: row = 1; break;
                case Direction.Right: row = 2; break;
                case Direction.Left:
                    row = 2; 
                    spriteEffect = SpriteEffects.FlipHorizontally; 
                    break;
            }

            if (_currentFrame > 3) _currentFrame = 0;

            sourceRect = new Rectangle(_currentFrame * 32, row * 32, 32, 32);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            Texture2D sheet = _isMoving ? _walkSheet : _idleSheet;
        
           _spriteBatch.Draw(sheet, _playerPosition, sourceRect, Color.White, 0f, Vector2.Zero, 4f, spriteEffect, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
