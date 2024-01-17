using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bullet_hell_test
{
    public class Bullet
    {
        public Vector2 pos;
        int lifespan;
        public Texture2D texture;
        long speed;
        public float rad;
        bool freindly;
        public bool dead = false;
        public Bullet(Texture2D sprite, Vector2 spawnpos, float rotation, bool player)
        {
            this.pos = spawnpos;
            this.rad = rotation;
            this.freindly = player;
            this.lifespan = 0;
            this.texture = sprite;
            this.speed = 10;
        }
        public void Update()
        {
            Vector2 direction = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));
            direction.Normalize();
            pos += direction * speed;
            lifespan++;
            switch (texture)
            {
                default:
                    if (lifespan == 100) dead = true;
                    break;
            }
        }
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D triangle;
        private Texture2D laser;
        private Texture2D torpedo;
        Vector2 shipPos = new(100, 300);
        List<Bullet> _bullets = new List<Bullet>();

        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            triangle = Content.Load<Texture2D>("Triangle");
            laser = Content.Load<Texture2D>("lazer");
            torpedo = Content.Load<Texture2D>("torpedp");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Escape))
                Exit();
            foreach (Bullet bullet in _bullets)//make for loop, remember if remove i--;
            {
                bullet.Update();
            }
            if (keyboard.IsKeyDown(Keys.Left)) shipPos.X-= 2;
            if (keyboard.IsKeyDown(Keys.Right)) shipPos.X+= 2;
            if (keyboard.IsKeyDown(Keys.Up)) shipPos.Y-= 3;
            if (keyboard.IsKeyDown(Keys.Down)) shipPos.Y++;
            if (keyboard.IsKeyDown(Keys.Space)) _bullets.Add(new Bullet(laser, shipPos, 0, true));
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkViolet);
            _spriteBatch.Begin();
            _spriteBatch.Draw(triangle, new Rectangle(shipPos.ToPoint(), new Point(100,100)), Color.White);
            foreach (Bullet bullet in _bullets)
            {
                _spriteBatch.Draw(bullet.texture, new Rectangle(bullet.pos.ToPoint(), new Point(10, 10)), null, Color.White, bullet.rad, new Vector2(laser.Width/2,laser.Height/2),SpriteEffects.None,0);
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}