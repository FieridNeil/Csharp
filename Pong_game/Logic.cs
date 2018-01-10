using System;
using System.Reflection;
using System.Windows.Forms;
class Logic : UI
{
    // Clock to update the UI
    Timer update = new Timer();

    // Clock to compute the changes on screen if any
    Timer animate = new Timer();

    Timer test = new Timer();

    public Logic()
    {
        
        update.Tick += Update_Tick;
        update.Interval = 3;

        animate.Tick += Animate_Tick;
        animate.Interval = 10;

        test.Tick += test_Tick;
        test.Interval = 50;

        start.Click += Start_Click;
        reset.Click += Reset_Click;

        // Need this to detect inputs
        this.KeyPreview = true;
        (game_area as Control).KeyDown += Button_Control_Down;
        (game_area as Control).KeyUp += Button_Control_Up;

        // Prevent the flickering
        typeof(Panel).InvokeMember("DoubleBuffered",
    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
    null, game_area, new object[] { true });
    }


    // Redraw the canvas
    private void Update_Tick(object sender, EventArgs e)
    {

        game_area.Invalidate();
    }

    // Update every tick.interval
    private void Animate_Tick(object sender, EventArgs e)
    {

        pluck.location.X += (int)pluck.speedX;
        pluck.location.Y += (int)pluck.speedY;

        if (paddle1.moveup_keypress && paddle1.location.Y > game_area.Top)
        {
            paddle1.location.Y -= paddle1.speed;
        }
        else if (paddle1.movedown_keypress && paddle1.location.Y + paddle1.height < game_area.Bottom)
        {
            paddle1.location.Y += paddle1.speed;
        }

        if (paddle2.moveup_keypress && paddle2.location.Y > game_area.Top)
        {
            paddle2.location.Y -= paddle2.speed;
        }else if (paddle2.movedown_keypress && paddle2.location.Y + paddle2.height < game_area.Bottom)
        {
            paddle2.location.Y += paddle2.speed;
        }

        if(pluck.location.X < game_area.Left || pluck.location.X + pluck.diameter > game_area.Right)
        {
            pluck.speedX = -pluck.speedX;
        }else if(pluck.location.Y < game_area.Top || pluck.location.Y + pluck.diameter > game_area.Bottom)
        {
            pluck.speedY = -pluck.speedY;
        }

        Console.WriteLine(pluck.speedX);
        Console.WriteLine(pluck.speedY);
        
        /*
        Console.WriteLine(paddle1.moveup_keypress);
        Console.WriteLine(paddle1.movedown_keypress);
        Console.WriteLine(paddle2.moveup_keypress);
        Console.WriteLine(paddle2.movedown_keypress);
        */
    }

    private void test_Tick(object sender, EventArgs e)
    {
        pluck.speedX *= 1.01;
        pluck.speedY *= 1.01;
    }
    private void Start_Click(object sender, EventArgs e)
    {
        // Set focus to game_area so key inputs will work
        game_area.Focus();

        // Start clocks
        update.Start();
        animate.Start();
        //test.Start();
        Console.WriteLine(game_area.Top);
        Console.WriteLine(game_area.Bottom);
        Console.WriteLine(game_area.Height);
        Console.WriteLine(this.Height);
        Console.WriteLine(this.ClientSize.Height);

    }

    private void Reset_Click(object sender, EventArgs e)
    {
        // Reset the game UI
        GamePlayUI();

        // Stop both clocks
        update.Stop();
        animate.Stop();
        test.Stop();
        // Redraw the UI
        game_area.Invalidate();
    }

    // Handle key inputs
    private void Button_Control_Down(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W:
                paddle1.moveup_keypress = true;
                break;

            case Keys.S:
                paddle1.movedown_keypress = true;
                break;

            case Keys.Up:
                paddle2.moveup_keypress = true;
                break;

            case Keys.Down:
                paddle2.movedown_keypress = true;
                break;
        }
    }

    private void Button_Control_Up(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W:
                paddle1.moveup_keypress = false;
                break;

            case Keys.S:
                paddle1.movedown_keypress = false;
                break;

            case Keys.Up:
                paddle2.moveup_keypress = false;
                break;

            case Keys.Down:
                paddle2.movedown_keypress = false;
                break;
        }
    }

}
