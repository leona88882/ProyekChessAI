using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyekChess
{
    public partial class Form1 : Form
    {   int ctrgerak=0;
        int tempx=-1;
        int tempy=-1;
        board tempgerak = new board();
        public Form1()
        {
            InitializeComponent();
        }

        Button[,]  b = new Button[4,8];
        board[,]  boards  = new board[4,8];
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boards[i, j] = new board();
                    Button temp = new Button();

                    temp.Location = new Point(70*i, 70*j);
                    temp.Size = new Size(70, 70);
                    if ((i % 2 == 1 && j % 2 == 1)|| (i % 2 == 0 && j % 2 == 0))
                    {
                        temp.BackColor = Color.Black;
                    }
                    else
                    {
                        temp.BackColor = Color.White;
                    }
                   
                    b[i, j] = temp;
                    b[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    b[i, j].Click += new System.EventHandler(this.ClickedButton);
                    b[i, j].Tag = i + "," + j;
                    this.Controls.Add(temp);
                }
            }
            boards[0, 0].nama = "Rook";
            boards[3, 0].nama = "Rook";
            boards[0, 7].nama = "Rook";
            boards[3, 7].nama = "Rook";

            boards[1, 0].nama = "King";
            boards[2, 0].nama = "Queen";
            boards[1, 7].nama = "King";
            boards[2, 7].nama = "Queen";

            boards[0, 1].nama = "Knight";
            boards[3, 1].nama = "Knight";
            boards[0, 6].nama = "Knight";
            boards[3, 6].nama = "Knight";

            boards[1, 1].nama = "Bishop";
            boards[2, 1].nama = "Bishop";
            boards[1, 6].nama = "Bishop";
            boards[2, 6].nama = "Bishop";
            for (int i = 0; i < 4; i++)
            {
                boards[i, 0].warna_team = "Black";
                boards[i,1].warna_team = "Black";

                boards[i, 6].warna_team = "White";
                boards[i, 7].warna_team = "White";
            }

            refresh();



        }
        private void ClickedButton(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string[] indexes = btn.Tag.ToString().Split(',');

            //in indexes[0] you've got the i index and in indexes[1] the j index
            if (ctrgerak == 0)
            {
                tempy = Int32.Parse(indexes[1]);
                tempx = Int32.Parse(indexes[0]);
                ctrgerak = 1;

            }
            else if (ctrgerak == 1)
            {

                board temps = boards[tempx, tempy];
                bool legalmove = checkgerakan(tempx, tempy, Int32.Parse(indexes[0]), Int32.Parse(indexes[1]), temps.nama);
                if (legalmove == true)
                {
                    if (boards[Int32.Parse(indexes[0]), Int32.Parse(indexes[1])].warna_team == "kosong")
                    {
                        boards[tempx, tempy] = boards[Int32.Parse(indexes[0]), Int32.Parse(indexes[1])];
                        boards[Int32.Parse(indexes[0]), Int32.Parse(indexes[1])] = temps;
                    }
                    else
                    {
                        board kosong = new board();
                        boards[tempx, tempy] = kosong;
                        boards[Int32.Parse(indexes[0]), Int32.Parse(indexes[1])] = temps;
                    }
                   
                }
                else
                {
                    MessageBox.Show("Gerakan Tidak Legal");
                }
                ctrgerak = 0;
                refresh();
               
            }
        }
        public bool checkgerakan(int x_start, int y_start,int x_end,int y_end,string nama)
        {
            board temps1 = boards[tempx, tempy];
            if (temps1.warna_team != boards[x_end, y_end].warna_team)
            {
                if (nama == "Knight")
                {

                    if (x_start - x_end == 2 && y_start - y_end == 1)
                    {
                        return true;
                    }
                    if (x_start - x_end == -2 && y_start - y_end == 1)
                    {
                        return true;
                    }
                    if (x_start - x_end == 2 && y_start - y_end == -1)
                    {
                        return true;
                    }
                    if (x_start - x_end == -2 && y_start - y_end == -1)
                    {
                        return true;
                    }
                    if (x_start - x_end == 1 && y_start - y_end == 2)
                    {
                        return true;
                    }
                    if (x_start - x_end == -1 && y_start - y_end == 2)
                    {
                        return true;
                    }
                    if (x_start - x_end == 1 && y_start - y_end == -2)
                    {
                        return true;
                    }
                    if (x_start - x_end == -1 && y_start - y_end == -2)
                    {
                        return true;
                    }
                    else { return false; }

                }
                if (nama == "Rook")
                {
                    bool sementara = true;
                    if (x_start - x_end == 0 && y_start-y_end!=0)
                    {

                        if (y_end - y_start > 0)
                        {
                            for (int i = y_start + 1; i < y_end; i++)
                            {
                                if (boards[x_start,i].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }

                        }
                        else if (y_end - y_start < 0)
                        {
                            for (int i = y_end + 1; i < y_start; i++)
                            {
                                if (boards[x_start, i].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        if (sementara == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (x_start - x_end != 0 && y_start - y_end == 0)
                    {
                        ;
                        if (x_end - x_start > 0)
                        {
                            
                            for (int i = x_start+1; i < x_end; i++)
                            {
                                if (boards[i, y_start].nama != "kosong")
                                {
                                    sementara= false;
                                }
                            }
                        }
                        else if(x_end - x_start <0)
                        {
                            for (int i = x_end + 1; i < x_start; i++)
                            {
                                if (boards[i, y_start].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        if (sementara == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (nama == "Bishop")
                {
                    if (Math.Abs(x_start - x_end) == Math.Abs(y_start - y_end))
                    {
                        bool sementara = true;
                        if (y_end > y_start)
                        {
                            //bawah
                            int jarak = y_end - y_start;
                            if (x_end > x_start)
                            {   //bawah-kanan
                                for (int i = 1; i < jarak; i++)
                                {
                                   if( boards[i + x_start, i + y_start].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (x_end < x_start)
                            {   //bawah-kiri
                               
                                for (int i = 1; i < jarak; i++)
                                {
                                    if (boards[x_start-i, y_start+i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        if (y_end < y_start)
                        {
                            int jarak = y_start - y_end;
                            if (x_end > x_start)
                            {   //atas-kanan
                                for (int i = 1; i < jarak; i++)
                                {
                                    if (boards[i + x_start,  y_start-i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (x_end < x_start)
                            {   //atas-kiri

                                for (int i = 1; i < jarak; i++)
                                {
                                    if (boards[x_start - i, y_start -i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (nama == "Queen")
                {
                    if (Math.Abs(x_start - x_end) == Math.Abs(y_start - y_end))
                    {
                        bool sementara = true;
                        if (y_end > y_start)
                        {
                            //bawah
                            int jarak = y_end - y_start;
                            if (x_end > x_start)
                            {   //bawah-kanan
                                for (int i = 1; i < jarak; i++)
                                {
                                    if (boards[i + x_start, i + y_start].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (x_end < x_start)
                            {   //bawah-kiri

                                for (int i = 1; i < jarak; i++)
                                {
                                    if (boards[x_start - i, y_start + i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        if (y_end < y_start)
                        {
                            int jarak = y_start - y_end;
                            if (x_end > x_start)
                            {   //atas-kanan
                                for (int i = 1; i < jarak; i++)
                                {
                                    if (boards[i + x_start, y_start - i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (x_end < x_start)
                            {   //atas-kiri

                                for (int i = 1; i < jarak; i++)
                                {
                                    if (boards[x_start - i, y_start - i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                    else if (x_start - x_end == 0 && y_start - y_end != 0)
                    {
                        bool sementara = true;

                        if (y_end - y_start > 0)
                        {
                            for (int i = y_start + 1; i < y_end; i++)
                            {
                                if (boards[x_start, i].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }

                        }
                        else if (y_end - y_start < 0)
                        {
                            for (int i = y_end + 1; i < y_start; i++)
                            {
                                if (boards[x_start, i].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        if (sementara == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (x_start - x_end != 0 && y_start - y_end == 0)
                    {
                        bool sementara = true;
                        
                        if (x_end - x_start > 0)
                        {

                            for (int i = x_start + 1; i < x_end; i++)
                            {
                                if (boards[i, y_start].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        else if (x_end - x_start < 0)
                        {
                            for (int i = x_end + 1; i < x_start; i++)
                            {
                                if (boards[i, y_start].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        if (sementara == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (nama == "King")
                {
                  
                    if (x_start - x_end != 0 || y_start - y_end != 0)
                    {
                        
                        if ((Math.Abs(x_start - x_end) == 1&& Math.Abs(y_end - y_start) < 2) || (Math.Abs(y_end - y_start) == 1&& Math.Abs(x_start - x_end) <2 ))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
            

        }
        public void refresh()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(boards[i,j].nama=="Bishop"&& boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.bishop;
                    }
                    else if (boards[i,j].nama == "Bishop" && boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.bishop_2;
                    }
                    else if (boards[i, j].nama == "King" && boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.king_2;
                    }
                    else if (boards[i, j].nama == "King" && boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.king;
                    }
                    else if (boards[i, j].nama == "Knight" && boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.knight;
                    }
                    else if (boards[i, j].nama == "Knight" && boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.knight_2;
                    }
                    else if (boards[i, j].nama == "Queen" && boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.queen_2;
                    }
                    else if (boards[i, j].nama == "Queen" && boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.queen;
                    }
                    else if (boards[i, j].nama == "Rook" && boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.rook;
                    }
                    else if (boards[i, j].nama == "Rook" && boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.rook_2;
                    }
                    else
                    {
                        b[i, j].BackgroundImage = base.BackgroundImage;
                    }
                }
            }
        }
    }
}
