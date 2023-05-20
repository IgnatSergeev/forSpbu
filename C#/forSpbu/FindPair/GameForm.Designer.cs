namespace FindPare;

partial class GameForm
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
    private void InitializeComponent(int fieldSize)
    {
        this.mainLayout = new TableLayoutPanel();
        this.fieldSize = fieldSize;
        
        this.Size = new Size(400, 400);
        this.MinimumSize = new Size(300, 400);
        this.Text = "FindPair";
        this.Controls.Add(this.mainLayout);

        this.mainLayout.Size = new Size(385, 362);
        this.mainLayout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        this.mainLayout.ColumnCount = fieldSize;
        this.mainLayout.RowCount = fieldSize;
        for (int i = 0; i < fieldSize; i++)
        {
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/fieldSize));
            mainLayout.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100/fieldSize));
            for (int j = 0; j < fieldSize; j++)
            {
                var button = new Button();
                button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                button.BackColor = Color.Gray;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Tag = (i * fieldSize + j).ToString();
                button.ForeColor = Color.Azure;
                button.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
                button.Click += CellOnClick;
                this.mainLayout.Controls.Add(button, i, j);
            }
        }
    }

    #endregion

    private TableLayoutPanel mainLayout;
    private int fieldSize;
}