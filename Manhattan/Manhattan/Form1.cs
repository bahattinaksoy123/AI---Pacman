using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manhattan
{
    public partial class Form1 : Form
    {

        int[,] map_matris;
        int adim = 0;
        int cilek = 405;
        public Form1()
        {
            InitializeComponent();
        }
        node[] list;
        class node
        {
            public int x = 0;
            public int y = 0;
            public double manhattan = 0.0;
            public int maliyet = 0;
            public bool ziyaret_edildimi = false;
            public List<node> komsular = new List<node>();
        }



        void map_matris_doldur()
        {
            map_matris = new int[31, 28]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },

                {0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0 },
                {0,1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0 },
                {0,1,1,1,1,1,1,0,0,1,1,1,1,0,0,1,1,1,1,0,0,1,1,1,1,1,1,0 },
                {0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,0,0,0,1,1,0,0,0,1,0,0,1,0,0,0,0,0,0 },/*kutu girişi*/
                {0,0,0,0,0,0,1,0,0,1,0,1,1,1,1,1,1,0,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,1,1,1,0,1,1,1,1,1,1,0,1,1,1,1,0,0,0,0,0,0 },

                {0,0,0,0,0,0,1,0,0,1,0,1,1,1,1,1,1,0,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,0,0,0,0,1,0 },
                {0,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,0 },
                {0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0 },
                {0,0,0,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,0 },
                {0,1,1,1,1,1,1,0,0,1,1,1,1,0,0,1,1,1,1,0,0,1,1,1,1,1,1,0 },
                {0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0 },
                {0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },

                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }

            };

        }
        void flowlayoutpanel_doldur()
        {
            flowLayoutPanel2.Controls.Clear();
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    PictureBox p = new PictureBox();
                    p.Size = new System.Drawing.Size(20, 20);
                    p.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    p.BackColor = System.Drawing.Color.Transparent;
                    p.Margin = new System.Windows.Forms.Padding(0);
                    p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    // p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    flowLayoutPanel2.Controls.Add(p);
                }
            }
          //ilk pacmani yerleştir//
          (flowLayoutPanel2.Controls[pacman] as PictureBox).Image = global::Manhattan.Properties.Resources.pd;
            //ilk yiyecegi yerlestir//
            (flowLayoutPanel2.Controls[cilek] as PictureBox).Image = global::Manhattan.Properties.Resources.strawberry;
        }

        void list_olustur()
        {
            list = new node[868];
            int a = 0;
            int cilekx = cilek / 28;
            int cileky = cilek - cilekx * 28;

            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    list[a] = new node();
                    list[a].x = i;
                    list[a].y = j;
                    double manhattan_mesafe = Math.Abs(i - cilekx) + Math.Abs(j - cileky);
                    manhattan_mesafe = manhattan_mesafe / 1.0;
                    list[a].manhattan = manhattan_mesafe;
                    list[a].maliyet = 1;
                    a++;
                }
            }
            //komsularini ekle
            a = 0;
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    try
                    {
                      
                        if (map_matris[(i - 1), (j)] == 1)
                        {
                            int b = a - 28;
                            list[a].komsular.Add(list[b]);

                        }
                        if (map_matris[(i), (j - 1)] == 1)
                        {
                            int b = a - 1;
                            list[a].komsular.Add(list[b]);
                        }
                        if (map_matris[(i + 1), (j)] == 1)
                        {
                            int b = a + 28;
                            list[a].komsular.Add(list[b]);
                        }
                        if (map_matris[(i), (j + 1)] == 1)
                        {
                            int b = a + 1;
                            list[a].komsular.Add(list[b]);

                        }
                    }
                    catch
                    {
                    }
                    a++;
                }
            }
        }

        List<int> stack;
        void a_yildiz_move(int current_position)
        {
            if (current_position != cilek)
            {
                adim++;
                list[current_position].ziyaret_edildimi = true;
                (flowLayoutPanel2.Controls[current_position] as PictureBox).Image = global::Manhattan.Properties.Resources.pd;
                Update();
                System.Threading.Thread.Sleep(80);
                int best_position = current_position;

                bool komsuvarmi = false;
                foreach (node nd in list[current_position].komsular)
                {
                    if (nd.ziyaret_edildimi == false)
                    {
                        best_position = nd.x * 28 + nd.y;
                        komsuvarmi = true;
                    }
                }
                if (komsuvarmi)
                {
                    foreach (node nd in list[current_position].komsular)
                    {
                        double f_maliyet = nd.manhattan+ nd.maliyet;
                        if (nd.ziyaret_edildimi == false && f_maliyet <= (list[best_position].manhattan +list[best_position].maliyet))
                        {
                            best_position = nd.x * 28 + nd.y;
                        }
                    }

                    (flowLayoutPanel2.Controls[current_position] as PictureBox).Image = global::Manhattan.Properties.Resources.circle;
                    stack.Add(best_position);
                    a_yildiz_move(best_position);
                }
                else
                {
                    
                    (flowLayoutPanel2.Controls[current_position] as PictureBox).Image = global::Manhattan.Properties.Resources.circle;
                    int position_last = stack[(stack.Count - 1)];
                    while (!komsuvarmii(position_last))
                    {
                        position_last = stack[(stack.Count - 1)];
                        (flowLayoutPanel2.Controls[position_last] as PictureBox).Image = global::Manhattan.Properties.Resources.pd;
                        this.Refresh();
                        (flowLayoutPanel2.Controls[position_last] as PictureBox).Image = global::Manhattan.Properties.Resources.circle;
                        stack.Remove(stack[(stack.Count - 1)]);
                    }
                    a_yildiz_move(position_last);

                }

            }
            else
            {
                timer.Stop();
                (flowLayoutPanel2.Controls[pacman] as PictureBox).Image = global::Manhattan.Properties.Resources.pd;
                MessageBox.Show("A* algorithması  ve manhattan ile şu kadar adımda çözüme ulaşıldı: " + adim.ToString());
                Application.Restart();
            }
        }

        bool komsuvarmii(int position)
        {
            bool varmi = false;
            foreach (node nd in list[position].komsular)
            {
                if (nd.ziyaret_edildimi == false)
                {
                    varmi = true;
                }
            }
            return varmi;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int pacman = 29;

        private void button1_Click(object sender, EventArgs e)
        {

            stack = new List<int>();
            button1.Visible = false;
            map_matris_doldur();
            cilek_random();
            cilek =position_1[rd.Next(position_1.Count)];
            flowlayoutpanel_doldur();
            list_olustur();
            maliyet_hesapla();
            timer.Start();
            a_yildiz_move(pacman);
        }

        List<int> position_1 = new List<int>();
        Random rd;

        void cilek_random()
        {
            rd = new Random();
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (map_matris[i, j] == 1)
                    {
                        int p = i * 28 + j;
                        if (p != 431) position_1.Add(p);
                    }
                }
            }
        }

        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = timer.Elapsed.ToString();
            Update();
        }



        List<int> komsu1 = new List<int>();
        int min1 = 99999;
        List<int> komsu2 = new List<int>();
        int min2 = 99999;

        List<int> komsu3 = new List<int>();
        int min3 = 99999;

        List<int> komsu4 = new List<int>();
        int min4 = 99999;


        void maliyet_hesapla()
        {
            int a = 0;
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    try
                    {
                        if (list[a].komsular[0] != null)
                        {
                            komsu1.Clear();
                            hesapla(list[a].komsular[0], 0, 1);
                            min1 = min(komsu1);
                        }
                        if (list[a].komsular[1] != null)
                        {
                            komsu2.Clear();
                            hesapla(list[a].komsular[1], 0, 2);
                            min2 = min(komsu1);
                        }
                        if (list[a].komsular[2] != null)
                        {
                            komsu3.Clear();
                            hesapla(list[a].komsular[2], 0, 3);
                            min3 = min(komsu1);
                        }
                        if (list[a].komsular[3] != null)
                        {
                            komsu4.Clear();
                            hesapla(list[a].komsular[3], 0, 4);
                            min4 = min(komsu1);
                        }
                        list[a].maliyet = Math.Min(Math.Min(min1, min2), Math.Min(min3, min4));
                    }
                    catch (Exception hata)
                    {

                    }
                    a++;
                }
            }

            for (int i = 0; i < list.Length; i++)
            {
                list[i].ziyaret_edildimi = false;
            }
        }


        int min(List<int> l)
        {
            int m = 999999;
            foreach (int i in l)
            {
                m = Math.Min(m, i);
            }
            return m;
        }

        void hesapla(node current, int maliyet, int komsu_number)
        {
            maliyet++;
            current.ziyaret_edildimi = true;
            int current_position = current.x * 28 + current.y;
            if (current_position != pacman)
            {
                if (komsu_number == 1)
                {
                    komsu1.Add(maliyet);
                }
                else if (komsu_number == 2)
                {
                    komsu2.Add(maliyet);
                }
                else if (komsu_number == 3)
                {
                    komsu3.Add(maliyet);
                }
                else
                {
                    komsu4.Add(maliyet);
                }
            }
            else
            {
                foreach (node k in current.komsular)
                {
                    if (k.ziyaret_edildimi == false)
                    {
                        hesapla(k, maliyet, komsu_number);
                    }
                }
            }

        }





    }
}
