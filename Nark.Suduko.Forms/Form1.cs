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
        Map m = new Map();
        List<TextBox> tbList = new List<TextBox>();
        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;

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

        private void InitSuduku(int level = 4)
        {
            m.Init();
            m.EraseRandomSquare(level * 10);
            List<TextBox> txtList = new List<TextBox>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Square s = m.GetSquare(i, j);
                    TextBox txtBox = new TextBox();
                    txtBox.Text = s.SquareValue;
                    if (s.SquareValue != "0")
                    {
                        txtBox.ReadOnly = true;
                        txtBox.BackColor = Color.Silver;
                        txtBox.ForeColor = Color.Black;
                    }
                    else
                    {
                        txtBox.Text = "";
                        txtBox.BackColor = Color.White;
                    }
                    txtBox.AutoSize = false;
                    txtBox.BorderStyle = BorderStyle.None;
                    txtBox.Font = new System.Drawing.Font("Console", 20);
                    txtBox.Size = new Size(50, 50);
                    txtBox.Location = new Point(i * 50 + 2 * i, j * 50 + 2 * j);
                    txtBox.Margin = new System.Windows.Forms.Padding(2);
                    Temp t = new Temp();
                    t.p = new Point(i, j);
                    t.c = txtBox.BackColor;
                    txtBox.Tag = t;
                    this.Controls.Add(txtBox);
                    txtBox.TextChanged += txtBox_TextChanged;
                    txtBox.MouseHover += txtBox_MouseHover;
                    txtBox.MouseLeave += txtBox_MouseLeave;
                    tbList.Add(txtBox);
                }
            }
        }

        void txtBox_MouseLeave(object sender, EventArgs e)
        {
            List<TextBox> list = new List<TextBox>();
            TextBox tb = (TextBox)sender;
            Point p = ((Temp)tb.Tag).p;
            Square s = m.GetSquare(p.X, p.Y);
            List<Square> sList = s.Peers;
            foreach (Square ss in sList)
            {
                Point pp = new Point(ss.Row, ss.Column);
                TextBox t = tbList.Find(obj => ((Temp)obj.Tag).p.X == pp.X && ((Temp)obj.Tag).p.Y == pp.Y);
                t.BackColor = ((Temp)t.Tag).c;
            }
        }

        void txtBox_MouseHover(object sender, EventArgs e)
        {
            List<TextBox> list = new List<TextBox>();
            TextBox tb = (TextBox)sender;
            Point p = ((Temp)tb.Tag).p;
            Square s = m.GetSquare(p.X, p.Y);
            List<Square> sList = s.Peers;
            foreach (Square ss in sList)
            {
                Point pp = new Point(ss.Row, ss.Column);
                TextBox t = tbList.Find(obj => ((Temp)obj.Tag).p.X == pp.X && ((Temp)obj.Tag).p.Y == pp.Y);
                t.BackColor = Color.AliceBlue;
            }
        }

        void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != "0" && !string.IsNullOrWhiteSpace(tb.Text))
            {
                Point p = ((Temp)tb.Tag).p;
                m.SetSquareValue(p.X, p.Y, tb.Text);
                if (!m.GetSquare(p.X, p.Y).IsValidate)
                {
                    tb.BackColor = Color.Red;
                    ((Temp)tb.Tag).c = tb.BackColor;
                }
                else
                {
                    tb.BackColor = Color.White;
                    ((Temp)tb.Tag).c = tb.BackColor;
                }
                if (m.MapStatus == GameEnum.MapStat.Completed)
                    MessageBox.Show("Win");
            }
            else if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = int.Parse(textBox1.Text);

            InitSuduku(i);
        }
    }

    class Temp
    {
        public Point p;
        public Color c;
    }
}
