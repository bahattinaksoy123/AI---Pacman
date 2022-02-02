using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soru5_CEVAP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<int> cilekler = new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            map_matris_doldur();
            pacman = 350;
            cilekler.Add(29);
            cilekler.Add(54);
            cilekler.Add(838);
            cilekler.Add(813);
            flowlayoutpanel_doldur();
            timer.Start();
            list_olustur();

            cilek_sec();//diğer fonksiyonlar içerisindedir;
        }

        void cilek_sec()
        {
            try
            {
                if (cilekler.Count == 0) throw new Exception();
                int min_position = 999;
                double eucledian = 999.0;
                int pacmanx = pacman / 28;
                int pacmany = pacman - pacmanx * 28;
                foreach (int i in cilekler)
                {
                    int cilekx = i / 28;
                    int cileky = i - (cilekx * 28);
                    double kusbakisi_mesafe = Math.Sqrt((Math.Pow((pacmanx - cilekx), 2) + Math.Pow((pacmany - cileky), 2)));
                    kusbakisi_mesafe = kusbakisi_mesafe / 1.0;
                    if (eucledian > kusbakisi_mesafe)
                    {
                        eucledian = kusbakisi_mesafe;
                        min_position = i;
                    }
                }
                cilek = min_position;
                cilekler.Remove(cilek);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ziyaret_edildimi = false;
                }

                eucledian_hesapla();
                maliyet_hesapla();
                stack.Clear();
                a_yildiz_move(list[find_idx(pacman)]);
            }
            catch
            {
                timer.Stop();
                (flowLayoutPanel2.Controls[pacman] as PictureBox).Image = global::Soru5_CEVAP.Properties.Resources.pr;
                MessageBox.Show("A* algorithması  ve eucledian ile 4 köşedeki Çileklere şu kadar adımda çözüme ulaşıldı: " + adim.ToString());
                Application.Restart();
            }
        }

        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

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
            foreach (node n in list)
            {
                try
                {
                    if (n.komsular[0] != null)
                    {
                        komsu1.Clear();
                        hesapla(n.komsular[0], 0, 1);
                        min1 = min(komsu1);
                    }
                    if (n.komsular[1] != null)
                    {
                        komsu2.Clear();
                        hesapla(n.komsular[1], 0, 2);
                        min2 = min(komsu1);
                    }
                    if (n.komsular[2] != null)
                    {
                        komsu3.Clear();
                        hesapla(n.komsular[2], 0, 3);
                        min3 = min(komsu1);
                    }
                    if (n.komsular[3] != null)
                    {
                        komsu4.Clear();
                        hesapla(n.komsular[3], 0, 4);
                        min4 = min(komsu1);
                    }
                    n.maliyet = Math.Min(Math.Min(min1, min2), Math.Min(min3, min4));
                }
                catch (Exception hata)
                {

                }
            }
            for (int i = 0; i < list.Count; i++)
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
            ++maliyet;
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
            //cilekleri yerlestir//
            foreach(int i in cilekler)
            {
                (flowLayoutPanel2.Controls[i] as PictureBox).Image = global::Soru5_CEVAP.Properties.Resources.strawberry;
            }
        }

        void eucledian_hesapla()
        {
            int cilekx = cilek / 28;
            int cileky = cilek - cilekx * 28;
            foreach (node n in list)
            {
                double kusbakisi_mesafe = Math.Sqrt((Math.Pow((n.x - cilekx), 2) + Math.Pow((n.y - cileky), 2)));
                n.eucledian = kusbakisi_mesafe / 1.0;
            }
        }

        void list_olustur()
        {
            list.Clear() ;
            int a = 0;

            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (map_matris[i, j] == 1)
                    {
                        node x = new node();
                        x.idx = a;
                        x.x = i;
                        x.y = j;
                        list.Add(x);
                        a++;
                    }
                }
            }
            //komsularini ekle
            foreach (node n in list)
            {
                try
                {
                    int i = n.x;
                    int j = n.y;
                    a = i * 28 + j;
                    if (map_matris[(i), (j - 1)] == 1)
                    {
                        int b = a - 1;
                        n.komsular.Add(list[find_idx(b)]);
                    }
                    if (map_matris[(i), (j + 1)] == 1)
                    {
                        int b = a + 1;
                        n.komsular.Add(list[find_idx(b)]);

                    }
                    if (map_matris[(i - 1), (j)] == 1)
                    {
                        int b = a - 28;
                        n.komsular.Add(list[find_idx(b)]);

                    }
                    if (map_matris[(i + 1), (j)] == 1)
                    {
                        int b = a + 28;
                        n.komsular.Add(list[find_idx(b)]);
                    }
                }
                catch
                {
                }
            }
        }

        int find_idx(int position)
        {
            int a = 0;
            foreach(node n in list)
            {
                if ((n.x * 28 + n.y) == position)
                {
                    break;
                }
                a++;
            }
            return a;
        }

        List<int> stack = new List<int>();
        void a_yildiz_move(node current)
        {
            int position = current.x * 28 + current.y;
            if (position != cilek)
            {
                adim++;
                current.ziyaret_edildimi = true;
                (flowLayoutPanel2.Controls[position] as PictureBox).Image = global::Soru5_CEVAP.Properties.Resources.pr;
                label1.Text = timer.Elapsed.ToString();
                Update();
                System.Threading.Thread.Sleep(80);
                int best_idx = current.idx;
                bool komsuvarmi = false;
                foreach (node nd in current.komsular)
                {
                    if (nd.ziyaret_edildimi == false)
                    {
                        best_idx = nd.idx;
                        komsuvarmi = true;
                    }
                }
                if (komsuvarmi)
                {
                    foreach (node nd in current.komsular)
                    {
                        double f_maliyet = nd.eucledian + nd.maliyet/1.0;
                        if (nd.ziyaret_edildimi == false && f_maliyet < (list[best_idx].eucledian + list[best_idx].maliyet)/1.0)
                        {
                            best_idx = nd.idx;
                        }
                    }

                    (flowLayoutPanel2.Controls[position] as PictureBox).Image = global::Soru5_CEVAP.Properties.Resources.circle;
                    stack.Add(current.idx);
                    a_yildiz_move(list[best_idx]);
                }
                else
                {
                    
                    (flowLayoutPanel2.Controls[position] as PictureBox).Image = global::Soru5_CEVAP.Properties.Resources.circle;
                    int last_idx = stack[(stack.Count-1)];
                    while (!komsuvarmii(last_idx))
                    {
                        last_idx = stack[(stack.Count - 1)];
                        int position_last = list[last_idx].x * 28 + list[last_idx].y;
                        (flowLayoutPanel2.Controls[position_last] as PictureBox).Image = global::Soru5_CEVAP.Properties.Resources.pr;
                        label1.Text = timer.Elapsed.ToString();
                        Update();
                        System.Threading.Thread.Sleep(80);
                        (flowLayoutPanel2.Controls[position_last] as PictureBox).Image = global::Soru5_CEVAP.Properties.Resources.circle;
                        stack.Remove(stack[(stack.Count - 1)]);
                    }
                    a_yildiz_move(list[last_idx]);
                    
                }

            }
            else
            {
                pacman = cilek;
                (flowLayoutPanel2.Controls[pacman] as PictureBox).Image = global::Soru5_CEVAP.Properties.Resources.pr;
                Update();
                cilek_sec();
            }
        }

        bool komsuvarmii(int idx)
        {
            bool varmi = false;
            foreach (node nd in list[idx].komsular)
            {
                if (nd.ziyaret_edildimi == false)
                {
                    varmi = true;
                }
            }
            return varmi;
        }

        int pacman = 405;
        int[,] map_matris;
        int adim = 0;
        int cilek = 405;
        List<node> list=new List<node>();
        class node
        {
            public int idx=0;
            public int x = 0;
            public int y = 0;
            public double eucledian = 0.0;
            public int maliyet = 0;
            public bool ziyaret_edildimi = false;
            public List<node> komsular = new List<node>();
        }

    }
}
