using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nark.Sudoku.Model;


namespace Nark.Suduko.Forms
{
    public partial class Form1 : Form
    {
        Map m;
        List<TextBox> tbList = new List<TextBox>();
        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            InitGUI();

            timer.Interval = 1000;
            timer.Tick += t_Tick;
        }

        DateTime start = new DateTime();
        TimeSpan duringTime = new TimeSpan();
        Timer timer = new Timer();
        void t_Tick(object sender, EventArgs e)
        {
            duringTime = DateTime.Now - start;
            lb_Time.Text = duringTime.ToString(@"hh\:mm\:ss");
        }


        void InitGUI()
        {
            Level l1 = new Level("Easy", 30);
            Level l2 = new Level("Normal", 40);
            Level l3 = new Level("Hard", 50);
            Level l4 = new Level("Hell", 60);
            List<Level> lvList = new List<Level>();
            lvList.AddRange(new Level[] { l1, l2, l3, l4 });
            cbb_Level.DataSource = lvList;
            cbb_Level.DisplayMember = "LvName";
            cbb_Level.ValueMember = "BlankCount";

            btn_Gen.Text = "Go";
            lb_Time.Text = "00:00:00";
            List<TextBox> txtList = new List<TextBox>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox txtBox = new TextBox();
                    txtBox.Text = "";
                    txtBox.AutoSize = false;
                    txtBox.BorderStyle = BorderStyle.None;
                    txtBox.Font = new System.Drawing.Font("Console", 20);
                    txtBox.Size = new Size(50, 50);
                    txtBox.Location = new Point(i * 50 + 2 * i, j * 50 + 2 * j);
                    txtBox.Margin = new System.Windows.Forms.Padding(2);
                    txtBox.MaxLength = 1;
                    txtBox.ReadOnly = true;
                    AdditionalInfo t = new AdditionalInfo();
                    t.Point = new Point(i, j);
                    t.Color = txtBox.BackColor;
                    txtBox.Tag = t;
                    this.Controls.Add(txtBox);
                    txtBox.TextChanged += txtBox_TextChanged;
                    txtBox.Click += txtBox_Click;
                    tbList.Add(txtBox);
                }
            }
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; //创建画板,这里的画板是由Form提供的.
            Pen p = new Pen(Color.Blue, 2);//定义了一个蓝色,宽度为的画笔
            //g.DrawLine(p, 10, 10, 100, 100);//在画板上画直线,起始坐标为(10,10),终点坐标为(100,100)
            g.DrawRectangle(p, 0, 0, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawRectangle(p, 0, 150 + 6, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawRectangle(p, 0, 300 + 12, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawRectangle(p, 150 + 6, 0, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawRectangle(p, 150 + 6, 150 + 6, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawRectangle(p, 150 + 6, 300 + 12, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawRectangle(p, 300 + 12, 0, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawRectangle(p, 300 + 12, 150 + 6, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为
            g.DrawRectangle(p, 300 + 12, 300 + 12, 150, 150);//在画板上画矩形,起始坐标为(10,10),宽为,高为



        }

        private void InitSuduku(int blankCount = 35)
        {
            m = new Map();
            m.FullInit();
            m.EraseRandomSquare(blankCount);
            List<TextBox> txtList = new List<TextBox>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox tb = tbList.Find(obj => ((AdditionalInfo)obj.Tag).Point == new Point(i, j));
                    AdditionalInfo temp = (AdditionalInfo)tb.Tag;
                    Square s = m.GetSquare(i, j);
                    bool isEmpty = (s.SquareValue == "0");
                    tb.Text = isEmpty ? "" : s.SquareValue;
                    tb.ReadOnly = !isEmpty;
                    tb.BackColor = tb.ReadOnly ? Color.Silver : Color.White;
                    temp.Color = tb.BackColor;
                }
            }
        }

        Point lastClick = new Point();
        void txtBox_Click(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            AdditionalInfo tbtag = (AdditionalInfo)tb.Tag;
            Point p = ((AdditionalInfo)tb.Tag).Point;
            if (p != lastClick)
            {
                foreach (TextBox txtB in tbList)
                {
                    txtB.BackColor = ((AdditionalInfo)txtB.Tag).Color;
                }
                if (m != null)
                {
                    Square s = m.GetSquare(p.X, p.Y);
                    List<Square> sList = s.Peers;

                    foreach (Square ss in sList)
                    {
                        Point pp = new Point(ss.Row, ss.Column);
                        TextBox t = tbList.Find(obj => ((AdditionalInfo)obj.Tag).Point.X == pp.X && ((AdditionalInfo)obj.Tag).Point.Y == pp.Y);
                        t.BackColor = t.BackColor == Color.AliceBlue ? ((AdditionalInfo)t.Tag).Color : Color.AliceBlue;
                    }
                }
                lastClick = p;
            }
            else
            {
                foreach (TextBox txtB in tbList)
                {
                    txtB.BackColor = ((AdditionalInfo)txtB.Tag).Color;
                }
                lastClick = new Point(-1, -1);
            }


        }

        void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != "0" && !string.IsNullOrWhiteSpace(tb.Text))
            {
                Point p = ((AdditionalInfo)tb.Tag).Point;
                m.SetSquareValue(p.X, p.Y, tb.Text);
                if (!m.GetSquare(p.X, p.Y).IsValidate)
                {
                    tb.BackColor = Color.Red;
                    ((AdditionalInfo)tb.Tag).Color = tb.BackColor;
                }
                else
                {
                    tb.BackColor = Color.White;
                    ((AdditionalInfo)tb.Tag).Color = tb.BackColor;
                }
                if (m.MapStatus == GameEnum.MapStat.Completed)
                {
                    timer.Stop();
                    MessageBox.Show("Win:{0}",duringTime.ToString(@"hh\:mm\:ss"));
                }
            }
            else if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.White;
                ((AdditionalInfo)tb.Tag).Color = tb.BackColor;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = int.Parse(cbb_Level.SelectedValue.ToString());

            InitSuduku(i);

            start = DateTime.Now;
            timer.Enabled = true;
            timer.Start();
        }
    }

    class AdditionalInfo
    {
        public Point Point;
        public Color Color;
    }

    class Level
    {
        private string lvName;

        public string LvName
        {
            get { return lvName; }
            set { lvName = value; }
        }
        private int blankCount;

        public int BlankCount
        {
            get { return blankCount; }
            set { blankCount = value; }
        }

        public Level(string name, int count)
        {
            lvName = name;
            blankCount = count;
        }
    }
}
