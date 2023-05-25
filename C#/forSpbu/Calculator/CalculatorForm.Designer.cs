namespace Calculator;

partial class CalculatorForm
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
            mainLayout = new TableLayoutPanel();
            
            expression = new Label();
            numberLabel = new Label();
            buttonsTable = new TableLayoutPanel();

            for (int i = 0; i <= 10; i++)
            {
                numberButtons.Add(new Button());
            }
            for (int i = 0; i < 7; i++)
            {
                operationButtons.Add(new Button());
            }
            string[] operationsText = { "C", "+", "-", "mod", "*", "/", "=" };


            buttonsTable.SuspendLayout();
            mainLayout.SuspendLayout();
            SuspendLayout();
            
            
            /*
             * Calculator form
             */
            Size = new Size(400, 400);
            MinimumSize = new Size(300, 400);
            Text = "Calculator";
            Controls.Add(mainLayout);
            
            /*
             * Main label
             */
            mainLayout.Size = new Size(385, 362);
            mainLayout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            mainLayout.ColumnCount = 1;
            mainLayout.RowCount = 3;
            mainLayout.Controls.Add(expression, 0, 0);
            mainLayout.Controls.Add(numberLabel, 0, 1);
            mainLayout.Controls.Add(buttonsTable, 0, 2);
            mainLayout.RowStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            mainLayout.RowStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            mainLayout.RowStyles.Add(new ColumnStyle(SizeType.Percent, 75F));

            /*
             * Expression label
             */
            expression.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            expression.TextAlign = ContentAlignment.MiddleRight;
            expression.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            expression.Text = "";
            
            /*
             * Number label
             */
            numberLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            numberLabel.TextAlign = ContentAlignment.MiddleRight;
            numberLabel.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Pixel);
            numberLabel.Text = "0";
            
            /*
             * Buttons table
             */
            buttonsTable.BackColor = Color.Black;
            buttonsTable.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            buttonsTable.ColumnCount = 3;
            buttonsTable.RowCount = 6;
            buttonsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.ColumnCount));
            buttonsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.ColumnCount));
            buttonsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.ColumnCount));
            buttonsTable.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.RowCount));
            buttonsTable.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.RowCount));
            buttonsTable.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.RowCount));
            buttonsTable.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.RowCount));
            buttonsTable.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.RowCount));
            buttonsTable.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100/buttonsTable.RowCount));
            buttonsTable.Controls.Add(numberButtons[0], 1, 5);
            buttonsTable.Controls.Add(numberButtons[1], 0, 4);
            buttonsTable.Controls.Add(numberButtons[2], 1, 4);
            buttonsTable.Controls.Add(numberButtons[3], 2, 4);
            buttonsTable.Controls.Add(numberButtons[4], 0, 3);
            buttonsTable.Controls.Add(numberButtons[5], 1, 3);
            buttonsTable.Controls.Add(numberButtons[6], 2, 3);
            buttonsTable.Controls.Add(numberButtons[7], 0, 2);
            buttonsTable.Controls.Add(numberButtons[8], 1, 2);
            buttonsTable.Controls.Add(numberButtons[9], 2, 2);
            buttonsTable.Controls.Add(operationButtons[0], 0, 0);
            buttonsTable.Controls.Add(operationButtons[1], 1, 0);
            buttonsTable.Controls.Add(operationButtons[2], 2, 0);
            buttonsTable.Controls.Add(operationButtons[3], 0, 1);
            buttonsTable.Controls.Add(operationButtons[4], 1, 1);
            buttonsTable.Controls.Add(operationButtons[5], 2, 1);
            buttonsTable.Controls.Add(operationButtons[6], 2, 5);
            
            buttonsTable.Controls.Add(numberButtons[10], 0, 5);

            /*
             * Buttons
             */
            for (int i = 0; i <= 9; i++)
            {
                var button = numberButtons[i];
                button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                button.BackColor = Color.Gray;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Text = i.ToString();
                button.ForeColor = Color.Azure;
                button.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
                button.Click += NumberOnClick;
            }
            var commaButton = numberButtons[10];
            commaButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            commaButton.BackColor = Color.DimGray;
            commaButton.TextAlign = ContentAlignment.MiddleCenter;
            commaButton.Text = ",";
            commaButton.ForeColor = Color.Azure;
            commaButton.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
            commaButton.Click += NumberOnClick;
            
            /*
             * Operations
             */
            for (int i = 0; i < 7; i++)
            {
                var button = operationButtons[i];
                button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                button.BackColor = (operationsText[i] != "=") ? Color.DimGray : Color.Aqua;
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Text = operationsText[i];
                button.ForeColor = (operationsText[i] != "=") ? Color.Azure : Color.Black;
                button.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Pixel);
                button.Click += OperationOnClick;
            }

            
            buttonsTable.ResumeLayout(false);
            mainLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainLayout;
        
        private Label expression;
        private Label numberLabel;
        private TableLayoutPanel buttonsTable;

        private List<Button> numberButtons = new List<Button>();

        private List<Button> operationButtons = new List<Button>();
}