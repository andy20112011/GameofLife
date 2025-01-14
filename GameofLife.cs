global using static GameofLife.GameSettings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;


namespace GameofLife;

public class GameofLife : Game
{
    Texture2D _cellTexture;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private BasicEffect _lineEffect;

    public GameofLife()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = GameSettings.WIDTH * GameSettings.CellSize;
        _graphics.PreferredBackBufferHeight = GameSettings.HEIGHT * GameSettings.CellSize;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        IsFixedTimeStep = true;

        // Set the desired target frame time (e.g., 16.667ms for ~60 FPS)
        // TimeSpan.FromSeconds(1f / targetFps): Creates a TimeSpan object from a
        // fraction of a second representing your desired target frame time, where targetFPS represents your desired frame per second.
        this.TargetElapsedTime = TimeSpan.FromSeconds(1f / 3f); 
    }

    private Board board;
    protected override void Initialize()
    {
        // The Initialize method is called after the constructor but before the main game loop (Update/Draw).
        // This is where you can query any required services and load any non-graphic related content.
        board = new Board();
        board.Clear();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // The LoadContent method is used to load your game content. It is called only once per game, within the Initialize method, before the main game loop starts.
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load the cell texture
        _cellTexture = Content.Load<Texture2D>("Cell");

        // Initialize a basic effect for drawing lines;
        _lineEffect = new BasicEffect(GraphicsDevice);
        _lineEffect.VertexColorEnabled = true;  // enable vertex coloring
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        board.Step();
        base.Update(gameTime);
    }

    private void DrawLine(Vector2 start, Vector2 end, Color color)
    {
        //Draw lines using basic effect.
        VertexPositionColor[] vertices = new VertexPositionColor[2];
        vertices[0] = new VertexPositionColor(new Vector3(start, 0), color);
        vertices[1] = new VertexPositionColor(new Vector3(end, 0), color);
        _lineEffect.World = Matrix.Identity;
        _lineEffect.View = Matrix.Identity;
        _lineEffect.Projection = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0f, 1f);

        foreach (var pass in _lineEffect.CurrentTechnique.Passes)
        {
            pass.Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
        }

    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        // Draw Grid lines
        for (int i = 0; i <= HEIGHT; i++)
        {
            // Draw Horizontal lines
            float y = i * board.cellSize;
            DrawLine(new Vector2(0, y), new Vector2(GraphicsDevice.Viewport.Width, y), Color.Gray);
        }
        for (int j = 0; j <= WIDTH; j++)
        {
            // Draw Vertical Lines
            float x = j * board.cellSize;
            DrawLine(new Vector2(x, 0), new Vector2(x, GraphicsDevice.Viewport.Height), Color.Gray);
        }

        _spriteBatch.Begin();
        _spriteBatch.Draw(_cellTexture, new Vector2(0, 0), Color.White);
        for (int row = 0; row < board.rows; row++)
        {
            for (int col = 0; col < board.columns; col++)
            {
                if (board.cells[row, col])
                {
                    int x = col * board.cellSize;
                    int y = row * board.cellSize;
                    _spriteBatch.Draw(_cellTexture, new Vector2(x, y), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
                }
            }
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
