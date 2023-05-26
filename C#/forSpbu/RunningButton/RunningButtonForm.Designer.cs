namespace RunningButton;

partial class RunningButtonForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Size = new Size(400, 400);
        this.MinimumSize = new Size(300, 400);
        this.Text = "RunningButton";
        this.Controls.Add(_runningButton);
        
        _runningButton.BackColor = Color.Gray;
        _runningButton.TextAlign = ContentAlignment.MiddleCenter;
        _runningButton.Text = "Button";
        _runningButton.ForeColor = Color.Azure;
        _runningButton.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
        _runningButton.MouseEnter += OnEnter;
        _runningButton.Click += OnClick;
        _runningButton.Location = new Point(0, 0);
        _runningButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        _runningButton.Size = new Size(100, 100);
    }

    #endregion

    private Button _runningButton = new Button();
    private Position _buttonPosition = Position.TopLeft;
}