using System;
using System.Drawing;
using System.Windows.Forms;
 class UI : Form
{
    // Menu controls
    Panel Menu = new Panel();
    Button Play = new Button();
    Button Quit = new Button();
    Label title = new Label();

    // Control area setting
    Panel control = new Panel();
    public Button start = new Button();
    public Button reset = new Button();
    Button return_to_menu = new Button();

    // Game area setting
    public Panel game_area = new Panel();

    public struct Paddle
    {
        public int width;
        public int height;
        public Point location;
        public SolidBrush brush;
        public int speed;
    }

    public Paddle paddle1 = new Paddle();
    public Paddle paddle2 = new Paddle();

    public struct Pluck
    {
        public int diameter;
        public Point location;
        public SolidBrush brush;
        public int speed;
    }

    public Pluck pluck;

    public UI()
    {
        this.Width = 800;
        this.Height = 500;
        this.BackColor = Color.Beige;
        Menu.Dock = DockStyle.Fill;
        this.Controls.Add(Menu);
        MenuUI();
        
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        this.DoubleBuffered = true;
    }



    private void MenuUI()
    {
        title.AutoSize = true;
        title.Text = "Welcome to Pong game";
        title.Location = new Point(200, 5);
        title.Font = new Font(this.Font.Name, 30);
        Menu.Controls.Add(title);

        Play.AutoSize = true;
        Play.Text = "Play";
        Play.Font = new Font(this.Font.Name, 30);
        Play.Anchor = AnchorStyles.None;
        Play.Location = new Point(200, this.ClientSize.Height / 2 - Play.Height / 2);
        Play.Click += Play_Click;
        Menu.Controls.Add(Play);

        Quit.AutoSize = true;
        Quit.Text = "Quit";
        Quit.Font = new Font(this.Font.Name, 30);
        Quit.Anchor = AnchorStyles.None;
        Quit.Location = new Point(500, this.ClientSize.Height / 2 - Quit.Height / 2);
        Quit.Click += Quit_Click;
        Menu.Controls.Add(Quit);
    }

    private void Play_Click(object sender, EventArgs e)
    {
        Menu.Visible = false;
        control.Visible = true;
        GamePlayUI();
    }

    private void Quit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    public void GamePlayUI()
    {
        Int16 button_margin = 100;
        game_area.Dock = DockStyle.Fill;
        game_area.BackColor = Color.AliceBlue;
        this.Controls.Add(game_area);


        control.Dock = DockStyle.Bottom;
        control.Height = 50;
        control.BackColor = Color.CadetBlue;
        this.Controls.Add(control);

        game_area.Paint += draw;

        start.AutoSize = true;
        start.Height = 25;
        start.Text = "Start";
        start.Location = new Point(200, 10);
        control.Controls.Add(start);


        reset.AutoSize = true;
        reset.Text = "Reset";
        reset.Location = new Point(start.Location.X + start.Width + button_margin, 10);
        control.Controls.Add(reset);

        return_to_menu.AutoSize = true;
        return_to_menu.Text = "Return to menu";
        return_to_menu.Location = new Point(reset.Location.X + reset.Width + button_margin, 10);
        return_to_menu.Click += return_to_menu_Click;
        control.Controls.Add(return_to_menu);

        paddle1.width = 20;
        paddle1.height = 80;
        paddle1.location = new Point(0, game_area.Height / 2 - paddle1.height / 2);
        paddle1.brush = new SolidBrush(Color.DarkSeaGreen);
        paddle1.speed = 10;

        paddle2.width = 20;
        paddle2.height = 80;
        paddle2.location = new Point(game_area.Width - paddle2.width, game_area.Height / 2 - paddle1.height / 2);
        paddle2.brush = new SolidBrush(Color.DarkSeaGreen);
        paddle2.speed = 10;

        pluck.diameter = 25;
        pluck.location = new Point(game_area.Width / 2 - pluck.diameter / 2, game_area.Height / 2 - pluck.diameter / 2);
        pluck.brush = new SolidBrush(Color.Red);
        pluck.speed = 1;

    }

    private void return_to_menu_Click(object sender, EventArgs e)
    {
        Menu.Visible = true;
        control.Visible = false;
    }

    public void draw(object sender, PaintEventArgs e)
    {
        e.Graphics.FillEllipse(pluck.brush, pluck.location.X, pluck.location.Y, pluck.diameter, pluck.diameter);
        e.Graphics.FillRectangle(paddle1.brush, paddle1.location.X, paddle1.location.Y, paddle1.width, paddle1.height);
        e.Graphics.FillRectangle(paddle2.brush, paddle2.location.X, paddle2.location.Y, paddle2.width, paddle2.height);
    }


}
