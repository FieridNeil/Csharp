using System;
using System.Reflection;
using System.Windows.Forms;
class Logic : UI
{
    // Clock to update the UI
    Timer update = new Timer();

    // Clock to compute the changes on screen if any
    Timer animate = new Timer();
    public Logic()
    {
        
        update.Tick += Update_Tick;
        update.Interval = 3;

        animate.Tick += Animate_Tick;
        animate.Interval = 10;

        start.Click += Start_Click;
        reset.Click += Reset_Click;

        // Need this to detect inputs
        this.KeyPreview = true;
        (game_area as Control).KeyPress += Button_Control;

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
        pluck.location.X += pluck.speed;
        pluck.location.Y += pluck.speed;
    }

    private void Start_Click(object sender, EventArgs e)
    {
        // Set focus to game_area so key inputs will work
        game_area.Focus();

        // Start clocks
        update.Start();
        animate.Start();
    }

    private void Reset_Click(object sender, EventArgs e)
    {
        // Reset the game UI
        GamePlayUI();

        // Stop both clocks
        update.Stop();
        animate.Stop();

        // Redraw the UI
        game_area.Invalidate();
    }

    // Handle key inputs
    private void Button_Control(object sender, KeyPressEventArgs e)
    {
        switch (e.KeyChar)
        {
            case (char)Keys.W:
                paddle1.location.Y -= paddle1.speed;
                break;

            case (char)Keys.S:
                paddle1.location.Y += paddle1.speed;
                break;

            case (char)Keys.Up:
                paddle2.location.Y -= paddle2.speed;
                break;

            case (char)Keys.Down:
                paddle2.location.Y += paddle2.speed;
                break;

        }
    }

}
