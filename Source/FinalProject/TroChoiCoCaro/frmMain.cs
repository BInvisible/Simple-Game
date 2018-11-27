using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TroChoiCoCaro
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// Biến toàn cục
        /// </summary>
        CSanDau SanDau = new CSanDau();
        CNguoiChoi1 P1 = new CNguoiChoi1();
        CNguoiChoi2 P2 = new CNguoiChoi2();
        CBong Bong = new CBong();
        int diem_P1 = 0;
        int diem_P2 = 0;
        int level = 1;
        bool isNewGame = false;
        bool isMouseDown;
        int kcTren;
        int kcDuoi;

        #region "Function xử lý"

        /// <summary>
        /// Hàm chống giật màn hình khi thao tác lên form
        /// </summary>
        private void AntiFlicker()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        public void Replay()
        {
            Bong.GanToaDo(475, 505, 325, 355);
            timerBongDiChuyen.Start();
        }
        public void DieuKhienLable(bool x)
        {
            label_DiemP1.Enabled = x;
            label_DiemP2.Enabled = x;
            lable_TenP2.Enabled = x;
            lable_TenP1.Enabled = x;
            lb_Level.Enabled = x;
        }
        public void GanToaDoVe()
        {
            SanDau.GanToaDo(150, 800, 150, 500);
            P1.GanToaDo(130, 150, 380, 460);
            P2.GanToaDo(800, 820, 200, 280);
        }
        #endregion
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GanToaDoVe();
            Bong.GanToaDo(475, 505, 325, 355);
            panel1.Show();
            isMouseDown = false;
            DieuKhienLable(true);
            AntiFlicker();
        }
        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            SanDau.VeSanDau(e.Graphics);
            P1.VeNguoiChoi(e.Graphics);
            P2.VeNguoiChoi(e.Graphics);
            Bong.VeNguoiChoi(e.Graphics);
            VeDiemP1(diem_P1.ToString());
            VeDiemP2(diem_P2.ToString());
        }

        private void timerBongDiChuyen_Tick(object sender, EventArgs e)
        {
            Bong.DiChuyen(SanDau, P1, P2);
            if (Bong.DiChuyen(SanDau, P1, P2) == 1)
            {
                diem_P2++;
                Replay();
                if (diem_P2 == 10)
                {
                    label_DiemP2.Text = diem_P2.ToString();
                    timerBongDiChuyen.Stop();
                    DialogResult dr = MessageBox.Show("Điểm P2 = 10! P2 thắng, P1 thua.", "Ping Pong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    diem_P1 = 0;
                    diem_P2 = 0;
                }
            }
            if (Bong.DiChuyen(SanDau, P1, P2) == 2)
            {
                diem_P1++;
                Replay();
                if (diem_P1 == 10)
                {
                    label_DiemP1.Text = diem_P1.ToString();
                    timerBongDiChuyen.Stop();
                    DialogResult dr = MessageBox.Show("Điểm P1 = 10! P1 thắng, P2 thua.", "Ping Pong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    diem_P1 = 0;
                    diem_P2 = 0;
                }
            }
            this.Invalidate();
        }
        public void VeDiemP1(string diem_P1)
        {
            label_DiemP1.Text = diem_P1.ToString();
            if (level == 0)
                lb_Level.Text = "Level: Easy";
            if (level == 1)
                lb_Level.Text = "Level: Normal";
            if (level == 2)
                lb_Level.Text = "Level: Hard";
        }
        public void VeDiemP2(string diem_P2)
        {
            label_DiemP2.Text = diem_P2.ToString();
        }
        /// <summary>
        /// Di chuyển P1,P2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.W)
            {
                P1.DiChuyenLen(SanDau);
                this.Invalidate();
            }
            if (e.KeyData == Keys.S)
            {
                P1.DiChuyenXuong(SanDau);
                this.Invalidate();
            }
            if (e.KeyData == Keys.Up)
            {
                P2.DiChuyenLen(SanDau);
                this.Invalidate();
            }
            else if (e.KeyData == Keys.Down)
            {
                P2.DiChuyenXuong(SanDau);
                this.Invalidate();
            }
        }
        /// <summary>
        /// Xử lý trên ToolStripMenuItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            DieuKhienLable(true);
            GanToaDoVe();
            switch (level)
            {
                case 0:
                    Bong.vectorX = 1;
                    Bong.vectorY = 2;
                    timerBongDiChuyen.Interval = 30;
                    timerBongDiChuyen.Start();
                    break;
                case 1:
                    Bong.vectorX = 3;
                    Bong.vectorY = 4;
                    timerBongDiChuyen.Interval = 20;
                    timerBongDiChuyen.Start();
                    break;
                case 2:
                    Bong.vectorX = 5;
                    Bong.vectorY = 9;
                    timerBongDiChuyen.Interval = 10;
                    timerBongDiChuyen.Start();
                    break;
            }
        }

        private void stopToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            timerBongDiChuyen.Stop();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
            MainForm m = new MainForm();
            m.Show();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = 0;
            lb_Level.Text = "Level: Easy";
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = 1;
            lb_Level.Text = "Level: Normal";
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = 2;
            lb_Level.Text = "Level: Hard";
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("P1: Up-> W; Down-> S\n\rP2: Up-> Up; Down-> Down or use mouse\n\rIf point = 10->VICTORY", "Ping Pong");
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtP1.ResetText();
            txtP2.ResetText();
            panel1.Show();

            if (isNewGame == true)
            {
                DieuKhienLable(true);
                GanToaDoVe();
                diem_P1 = 0;
                diem_P2 = 0;
                Bong.GanToaDo(475, 505, 325, 355);
                timerBongDiChuyen.Stop();
                if (playToolStripMenuItem1.Checked == true)
                {
                    switch (level)
                    {
                        case 0:
                            Bong.vectorX = 1;
                            Bong.vectorY = 2;
                            timerBongDiChuyen.Interval = 30;
                            timerBongDiChuyen.Start();
                            break;
                        case 1:
                            Bong.vectorX = 3;
                            Bong.vectorY = 4;
                            timerBongDiChuyen.Interval = 20;
                            timerBongDiChuyen.Start();
                            break;
                        case 2:
                            Bong.vectorX = 4;
                            Bong.vectorY = 5;
                            timerBongDiChuyen.Interval = 10;
                            timerBongDiChuyen.Start();
                            break;
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            isNewGame = true;
            lable_TenP1.Text = txtP1.Text.ToString();
            if (lable_TenP1.Text == "")
                lable_TenP1.Text = "Player 1";
            lable_TenP2.Text = txtP2.Text.ToString();
            if (lable_TenP2.Text == "")
                lable_TenP2.Text = "Player 2";
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y > P2.Tren && e.Y < P2.Duoi && e.X > P2.Trai && e.X < P2.Phai)
            {
                kcTren = e.Y - P2.Tren;
                kcDuoi = P2.Duoi - e.Y;
                isMouseDown = true;
            }
        }

        private void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            kcTren = 0;
            kcDuoi = 0;
        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                if (P2.Tren == SanDau.Tren)
                {
                    if (e.Y - kcTren > P2.Tren)
                    {
                        P2.Tren = e.Y - kcTren;
                        P2.Duoi = e.Y + kcDuoi;
                    }
                }
                if (P2.Duoi == SanDau.Duoi)
                {
                    if (e.Y - kcTren < P2.Tren)
                    {
                        P2.Tren = e.Y - kcTren;
                        P2.Duoi = e.Y + kcDuoi;
                    }
                }

                if (P2.Tren > SanDau.Tren && P2.Duoi < SanDau.Duoi)
                {
                    int temp_Tren = e.Y - kcTren;
                    int temp_Duoi = e.Y + kcDuoi;

                    if (temp_Tren < SanDau.Tren)
                    {
                        int temp = P2.Tren;

                        P2.Tren = SanDau.Tren;

                        P2.Duoi -= (temp - P2.Tren);
                    }
                    else
                        if (temp_Duoi > SanDau.Duoi)
                    {
                        int temp = P2.Duoi;
                        P2.Duoi = SanDau.Duoi;
                        P2.Tren += (P2.Duoi - temp);
                    }
                    else
                    {
                        P2.Tren = (e.Y - kcTren);
                        P2.Duoi = (e.Y + kcDuoi);
                    }

                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtP1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lable_TenP2_Click(object sender, EventArgs e)
        {

        }

        private void lable_TenP1_Click(object sender, EventArgs e)
        {

        }

        private void label_DiemP1_Click(object sender, EventArgs e)
        {

        }

        private void label_DiemP2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtP2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtP1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lable_TenP11_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lb_Level_Click(object sender, EventArgs e)
        {

        }
    }

}
