using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprites;

namespace Week4Lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Viewport mapViewport;
        Viewport originalViewPort;
        private Texture2D _txCharacter;
        private SimpleSprite CharacterSprite;
        private Texture2D _txBackGround;
        private SimpleSprite BackGroundSprite;
        Vector2 CharacterPosition = new Vector2(10, 10);
        Vector2 LipPosition = new Vector2(10, 10);
        private Texture2D _txLips;
        private SimpleSprite LipsSprite;
        private SpriteFont GameFont;
        private SimpleSprite frameSprite;
        Point previousMousePosition;
        float scale = 0.1f;
        public Game1()
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
            // Sample the original Viewport
            IsMouseVisible = true;
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();
            originalViewPort = GraphicsDevice.Viewport;
            GraphicsDevice.Viewport = originalViewPort;
            // Create the map viewport
            mapViewport.Bounds = new Rectangle(0, 0,
                (int)(originalViewPort.Bounds.Width * scale),
                (int)(originalViewPort.Bounds.Height * scale));
            mapViewport.X = 0;
            mapViewport.Y = 0;
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
            _txBackGround = Content.Load<Texture2D>(@"Textures\backgroundImage");
            BackGroundSprite = new SimpleSprite(_txBackGround, Vector2.Zero);
            _txCharacter = Content.Load<Texture2D>(@"Textures\body2");
            CharacterSprite = new SimpleSprite(_txCharacter, originalViewPort.Bounds.Center.ToVector2());
            _txLips = Content.Load<Texture2D>(@"Textures\lips");
            LipsSprite = new SimpleSprite(_txLips, originalViewPort.Bounds.Center.ToVector2() + new Vector2(100,-100));
            frameSprite = new 
                SimpleSprite(Content.Load<Texture2D>(@"Textures\Frame")
                , Vector2.Zero);
            GameFont = Content.Load<SpriteFont>(@"GameFont");
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float speed = 5.0f;
            //float scale = 0.01f;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Vector2 perviousPos = CharacterSprite.Position;
            Vector2 perviousLipPosition = LipsSprite.Position;
            
            // TODO: Add your update logic here
            // Main character Movement

            MouseState ms = Mouse.GetState();
            if(mapViewport.Bounds.Contains(ms.Position))
            {
                if(ms.LeftButton == ButtonState.Pressed && previousMousePosition != ms.Position)
                {
                    Point p = ms.Position - previousMousePosition;
                    mapViewport.X += p.X;
                    mapViewport.Y += p.Y;
                    //mapViewport.Bounds.Offset(p);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                CharacterSprite.Move(new Vector2(-1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                CharacterSprite.Move(new Vector2(1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                CharacterSprite.Move(new Vector2(0, -1) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                CharacterSprite.Move(new Vector2(0, 1) * speed);
            if (!GraphicsDevice.Viewport.Bounds
                .Contains(CharacterSprite.BoundingRect))
                CharacterSprite.Move(perviousPos - CharacterSprite.Position);

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                LipsSprite.Move(new Vector2(-1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                LipsSprite.Move(new Vector2(1, 0) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                LipsSprite.Move(new Vector2(0, -1) * speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                LipsSprite.Move(new Vector2(0, 1) * speed);
            if (!GraphicsDevice.Viewport.Bounds
                .Contains(LipsSprite.BoundingRect))
                LipsSprite.Move(perviousLipPosition - LipsSprite.Position);

            previousMousePosition = ms.Position;
            LipsSprite.text = LipsSprite.Position.ToString();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            CharacterPosition = CharacterSprite.Position;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            BackGroundSprite.draw(spriteBatch,originalViewPort.Bounds);
            CharacterSprite.text = CharacterSprite.Position.ToString();
            CharacterSprite.draw(spriteBatch, GameFont);
            LipsSprite.text = LipsSprite.Position.ToString();
            LipsSprite.draw(spriteBatch, GameFont);
            //CharacterSprite.draw(spriteBatch);
            //spriteBatch.Draw(_txBackGround, originalViewPort.Bounds, Color.White);

            //spriteBatch.Draw(_txCharacter, CharacterPosition, Color.White);
            spriteBatch.End();
            
            GraphicsDevice.Viewport = mapViewport;
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            frameSprite.draw(spriteBatch, mapViewport.Bounds);
            BackGroundSprite.draw(spriteBatch,mapViewport.Bounds);
            CharacterSprite.draw(spriteBatch,scale);
            LipsSprite.draw(spriteBatch, scale);
            spriteBatch.End();
            // TODO: Add your drawing code here
            GraphicsDevice.Viewport = originalViewPort;
            base.Draw(gameTime);
        }
    }
}
